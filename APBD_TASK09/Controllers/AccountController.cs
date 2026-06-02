using System.Security.Claims;
using APBD_TASK09.Data;
using APBD_TASK09.Models;
using APBD_TASK09.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APBD_TASK09.Controllers;

public class AccountController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly PasswordHasher<AppUser> _passwordHasher;

    public AccountController(ApplicationDbContext context)
    {
        _context = context;
        _passwordHasher = new PasswordHasher<AppUser>();
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(
        RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var exists = await _context.AppUsers
            .AnyAsync(x => x.Email == model.Email);

        if (exists)
        {
            ModelState.AddModelError(
                "",
                "Email already exists");

            return View(model);
        }

        var user = new AppUser
        {
            Email = model.Email,
            Role = "User"
        };

        user.PasswordHash =
            _passwordHasher.HashPassword(
                user,
                model.Password);

        _context.AppUsers.Add(user);

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Login));
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(
        LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = await _context.AppUsers
            .FirstOrDefaultAsync(x =>
                x.Email == model.Email);

        if (user == null)
        {
            ModelState.AddModelError(
                "",
                "Invalid credentials");

            return View(model);
        }

        var result =
            _passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                model.Password);

        if (result ==
            PasswordVerificationResult.Failed)
        {
            ModelState.AddModelError(
                "",
                "Invalid credentials");

            return View(model);
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier,
                user.Id.ToString()),

            new(ClaimTypes.Name,
                user.Email),

            new(ClaimTypes.Role,
                user.Role)
        };

        var identity = new ClaimsIdentity(
            claims,
            CookieAuthenticationDefaults.AuthenticationScheme);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity));

        return RedirectToAction(
            "Index",
            "Dashboard");
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction(nameof(Login));
    }
}