namespace APBD_TASK09.Models;

using System.ComponentModel.DataAnnotations;


public class AppUser
{
    public int Id { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public string PasswordHash { get; set; } = null!;

    [Required]
    public string Role { get; set; } = "User";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<UserNote> Notes { get; set; } =
        new List<UserNote>();
}