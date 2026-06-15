using System.ComponentModel.DataAnnotations;

namespace eTickets.Models;

public class Producer
{
    private Producer() { }

    public Producer(string fullName, string profilePictureUrl, string bio)
    {
        FullName = fullName;
        ProfilePictureURL = profilePictureUrl;
        Bio = bio;
        Movies = new List<Movie>();
    }

    [Key]
    public int Id { get; private set; }

    [Display(Name = "Full Name")]
    [Required]
    public string FullName { get; private set; }

    [Display(Name = "Profile Picture URL")]
    [Required]
    public string ProfilePictureURL { get; private set; }

    [Display(Name = "Biography")]
    [Required]
    public string Bio { get; private set; }

    public List<Movie> Movies { get; private set; }

    public void Update(string fullName, string profilePictureUrl, string bio)
    {
        FullName = fullName;
        ProfilePictureURL = profilePictureUrl;
        Bio = bio;
    }
}