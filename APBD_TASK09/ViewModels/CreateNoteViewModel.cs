using System.ComponentModel.DataAnnotations;

namespace APBD_TASK09.ViewModels;

public class CreateNoteViewModel
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = null!;

    [Required]
    [MaxLength(1000)]
    public string Content { get; set; } = null!;
}