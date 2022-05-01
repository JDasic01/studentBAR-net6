using System.ComponentModel.DataAnnotations;

namespace studentBARUI.Models;

public class CreateFacultyModel
{
    [Required]
    [MaxLength(75)]
    public string FacultyName { get; set; }
    [Required]
    [MinLength(1)]
    [Display(Name = "University")]
    public string UniversityId { get; set; }
    [MaxLength(500)]
}
