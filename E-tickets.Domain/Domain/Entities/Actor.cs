using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace eTickets.Models;

public class Actor
{
    private Actor() { }

    public Actor(string fullName, string? profilePicture, string bio)
    {
        FullName = fullName;
        ProfilePivture = profilePicture; 
        Bio = bio;
        Actors_Movies = new List<Actor_Movie>();
    }
    [Key]
    public int Id { get; private set; }
    [Display(Name = "Full Name")]
    [Required] 
    public string FullName { get; private set; }
    [Display(Name = "Profile Picture URL")]
    public string? ProfilePivture { get; private set; }
    [Display(Name = "Biography")]
    [Required]
    public string Bio { get; private set; }
    [ValidateNever]
    public List<Actor_Movie> Actors_Movies { get; private set; }
    public void Update(string fullName, string? profilePicture, string bio)
    {
        if (string.IsNullOrWhiteSpace(fullName)) 
            throw new ArgumentException("Name cannot be empty");

        FullName = fullName;
        ProfilePivture = profilePicture;
        Bio = bio;
    }
}