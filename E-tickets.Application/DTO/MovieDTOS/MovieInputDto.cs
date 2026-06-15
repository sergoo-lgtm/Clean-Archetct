using E_tickets.Core.Domain; 

namespace eTickets.DTO.MovieDTOS;

public class MovieInputDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public double Price { get; set; }
    public string ImageURL { get; set; }
    public MovieCategory MovieCategory { get; set; }
    public int CinemaId { get; set; }
    public int ProducerId { get; set; }
    public List<int> ActorIds { get; set; } = new List<int>();
}
