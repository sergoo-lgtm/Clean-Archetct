using System.ComponentModel.DataAnnotations;

namespace eTickets.Models;

public class Cinema
{
    private Cinema() { }

    public Cinema(string name, string logo, string description)
    {
        Name = name;
        Logo = logo;
        Description = description;
        Movies = new List<Movie>();
    }

    [Key]
    public int Id { get; private set; }

    [Display(Name = "Cinema Name")]
    [Required]
    public string Name { get; private set; }

    [Display(Name = "Cinema Logo")]
    [Required]
    public string Logo { get; private set; }

    [Display(Name = "Cinema Description")]
    [Required]
    public string Description { get; private set; }

    public List<Movie> Movies { get; private set; }

    public void Update(string name, string logo, string description)
    {
        Name = name;
        Logo = logo;
        Description = description;
    }
}