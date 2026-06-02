using System.Security.Claims;
using APBD_TASK09.Data;
using APBD_TASK09.Models;
using APBD_TASK09.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APBD_TASK09.Controllers;

[Authorize]
public class DashboardController : Controller
{
    private readonly ApplicationDbContext _context;

    public DashboardController(
        ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var userId = int.Parse(
            User.FindFirstValue(
                ClaimTypes.NameIdentifier)!);

        var notes = await _context.UserNotes
            .Where(x => x.AppUserId == userId)
            .ToListAsync();

        return View(notes);
    }

    [HttpPost]
    public async Task<IActionResult> AddNote(
        CreateNoteViewModel model)
    {
        if (!ModelState.IsValid)
            return RedirectToAction(nameof(Index));

        var userId = int.Parse(
            User.FindFirstValue(
                ClaimTypes.NameIdentifier)!);

        var note = new UserNote
        {
            AppUserId = userId,
            Title = model.Title,
            Content = model.Content
        };

        _context.UserNotes.Add(note);

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}