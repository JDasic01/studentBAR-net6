using System.ComponentModel.DataAnnotations;

namespace studentBARUI.Models;

public class CreatePostModel
{
    [Required]
    [MaxLength(75)]
    public string Suggestion { get; set; }
    [Required]
    [MinLength(1)]
    [Display(Name = "Faculty")]
    public string FacultyId { get; set; }
    [MaxLength(500)]
    public string Description { get; set; }
}
