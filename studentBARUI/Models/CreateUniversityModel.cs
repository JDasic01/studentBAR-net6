using System.ComponentModel.DataAnnotations;

namespace studentBARUI.Models;

public class CreateUniversityModel
{
    [Required]
    [MaxLength(75)]
    public string University { get; set; }
}
