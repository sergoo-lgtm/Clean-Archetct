using Microsoft.AspNetCore.Identity;

namespace eTickets.Models.IdentityEntities
{
    public class ApplicationUser :IdentityUser<Guid>
    {
        public string? PersonName { get; set; }
    }
}
