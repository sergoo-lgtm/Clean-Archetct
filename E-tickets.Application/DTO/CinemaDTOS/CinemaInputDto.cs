using System.ComponentModel.DataAnnotations;

namespace eTickets.DTO.CinemaDTOS;

public class CinemaInputDto
{
    [Required]
    [Display(Name = "Cinema Name")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Url]
    [Display(Name = "Cinema Logo")]
    public string Logo { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Cinema Description")]
    public string Description { get; set; } = string.Empty;
}
