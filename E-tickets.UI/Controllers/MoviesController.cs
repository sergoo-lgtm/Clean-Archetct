using eTickets.DTO.MovieDTOS;
using eTickets.Service;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers;

public class MoviesController : Controller
{
    private readonly MovieService _service;

    public MoviesController(MovieService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index(string? searchString)
    {
        IEnumerable<MovieDto> movies;

        if (!string.IsNullOrWhiteSpace(searchString))
        {
            movies = await _service.SearchMoviesAsync(searchString);
            ViewData["SearchString"] = searchString;
        }
        else
        {
            movies = await _service.GetAllMoviesAsync();
        }

        return View(movies);
    }

    public async Task<IActionResult> Filter(string? searchString)
    {
        return RedirectToAction(nameof(Index), new { searchString });
    }

    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var movie = await _service.GetMovieDetailsAsync(id);
            return View(movie);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
