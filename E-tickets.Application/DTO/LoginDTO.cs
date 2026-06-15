using System.ComponentModel.DataAnnotations;

namespace eTickets.DTO;

public class LoginDTO
{
   

    [Required(ErrorMessage = "Email is required")]

    [EmailAddress(ErrorMessage = "Invalid email format")]
    
    public string? Email { get; set; } 

    [Required(ErrorMessage = "Password is required")]

    [DataType(DataType.Password)]


    public string? Password { get; set; } 
}