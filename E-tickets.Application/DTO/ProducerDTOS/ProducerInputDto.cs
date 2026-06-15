using System.ComponentModel.DataAnnotations;

namespace eTickets.DTO.ProducerDTOS;

public class ProducerInputDto
{
    [Required]
    [Display(Name = "Full Name")]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [Url]
    [Display(Name = "Profile Picture URL")]
    public string ProfilePictureURL { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Biography")]
    public string Bio { get; set; } = string.Empty;
}
