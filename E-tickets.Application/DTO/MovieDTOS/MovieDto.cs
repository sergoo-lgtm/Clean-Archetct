using E_tickets.Core.Domain; 

namespace eTickets.DTO.MovieDTOS;

public class MovieDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public double Price { get; set; }
    public string ImageURL { get; set; } = string.Empty;
    public MovieCategory MovieCategory { get; set; }
    public string CinemaName { get; set; } = string.Empty;
    public string ProducerName { get; set; } = string.Empty;
    public List<string> ActorNames { get; set; } = new List<string>();
}
