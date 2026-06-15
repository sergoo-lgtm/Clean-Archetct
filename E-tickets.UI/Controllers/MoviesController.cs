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

    public async Task<IActionResult> Index()
    {
        var movies = await _service.GetAllMoviesAsync();
        return View(movies);
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
