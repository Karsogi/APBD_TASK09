namespace APBD_TASK09.Models;
using System.ComponentModel.DataAnnotations;

public class UserNote
{
    public int Id { get; set; }

    public int AppUserId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = null!;

    [Required]
    [MaxLength(1000)]
    public string Content { get; set; } = null!;

    public DateTime CreatedAt { get; set; } =
        DateTime.UtcNow;

    public AppUser AppUser { get; set; } = null!;
}