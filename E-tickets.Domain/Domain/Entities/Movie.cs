using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using E_tickets.Core.Domain; // أو حسب اسم المجلد الفعلي عندك
using E_tickets.Core.Domain;
namespace eTickets.Models;

public class Movie
{
    private Movie() { }

    public Movie(string name, string description, DateTime startDate, DateTime endDate, double price, string imageUrl, MovieCategory movieCategory, int cinemaId, int producerId)
    {
        Name = name;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        Price = price;
        ImageURL = imageUrl;
        MovieCategory = movieCategory;
        CinemaId = cinemaId;
        ProducerId = producerId;
        Actors_Movies = new List<Actor_Movie>();
    }

    [Key]
    public int Id { get; private set; }

    [Required]
    public string Name { get; private set; }

    [Required]
    public string Description { get; private set; }

    public DateTime StartDate { get; private set; }

    public DateTime EndDate { get; private set; }

    public double Price { get; private set; }

    [Required]
    public string ImageURL { get; private set; }

    public MovieCategory MovieCategory { get; private set; }

    public List<Actor_Movie> Actors_Movies { get; private set; }

    [ForeignKey("CinemaId")]
    public int CinemaId { get; private set; }
    public Cinema Cinema { get; private set; }

    [ForeignKey("ProducerId")]
    public int ProducerId { get; private set; }
    public Producer Producer { get; private set; }

    public void Update(string name, string description, DateTime startDate, DateTime endDate, double price, string imageUrl, MovieCategory movieCategory, int cinemaId, int producerId)
    {
        Name = name;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        Price = price;
        ImageURL = imageUrl;
        MovieCategory = movieCategory;
        CinemaId = cinemaId;
        ProducerId = producerId;
    }
}