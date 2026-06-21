using eTickets.DTO.MovieDTOS;
using eTickets.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Service;

public class MovieService
{
    private readonly IUnitOfWork _unitOfWork;

    public MovieService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<MovieDto>> GetAllMoviesAsync()
    {
        var movies = await _unitOfWork.Movies.GetAll
            .AsNoTracking()
            .Include(m => m.Cinema)
            .Include(m => m.Producer)
            .OrderBy(m => m.Name)
            .ToListAsync();

        return movies.Select(m => new MovieDto
        {
            Id = m.Id,
            Name = m.Name,
            Description = m.Description,
            StartDate = m.StartDate,
            EndDate = m.EndDate,
            Price = m.Price,
            ImageURL = m.ImageURL,
            MovieCategory = m.MovieCategory,
            CinemaName = m.Cinema.Name,
            ProducerName = m.Producer.FullName
        });
    }

    public async Task<IEnumerable<MovieDto>> SearchMoviesAsync(string searchString)
    {
        var movies = await _unitOfWork.Movies.GetAll
            .AsNoTracking()
            .Include(m => m.Cinema)
            .Include(m => m.Producer)
            .Where(m => m.Name.Contains(searchString))
            .OrderBy(m => m.Name)
            .ToListAsync();

        return movies.Select(m => new MovieDto
        {
            Id = m.Id,
            Name = m.Name,
            Description = m.Description,
            StartDate = m.StartDate,
            EndDate = m.EndDate,
            Price = m.Price,
            ImageURL = m.ImageURL,
            MovieCategory = m.MovieCategory,
            CinemaName = m.Cinema.Name,
            ProducerName = m.Producer.FullName
        });
    }
    public async Task<MovieDto> GetMovieDetailsAsync(int id)
    {
        var movie = await _unitOfWork.Movies.GetAll
            .AsNoTracking()
            .Include(m => m.Cinema)
            .Include(m => m.Producer)
            .Include(m => m.Actors_Movies)
            .ThenInclude(am => am.Actor)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie == null)
            throw new KeyNotFoundException($"Movie with ID {id} not found.");

        return new MovieDto
        {
            Id = movie.Id,
            Name = movie.Name,
            Description = movie.Description,
            StartDate = movie.StartDate,
            EndDate = movie.EndDate,
            Price = movie.Price,
            ImageURL = movie.ImageURL,
            MovieCategory = movie.MovieCategory,
            CinemaName = movie.Cinema.Name,
            ProducerName = movie.Producer.FullName,
            ActorNames = movie.Actors_Movies
                .Select(am => am.Actor.FullName)
                .OrderBy(name => name)
                .ToList()
        };
    }
}