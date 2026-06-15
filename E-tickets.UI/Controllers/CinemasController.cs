using eTickets.DTO.CinemaDTOS;
using eTickets.Middlewares;
using eTickets.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers;

public class CinemasController : Controller
{
    private readonly CinemaService _service;

    public CinemasController(CinemaService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var allCinemas = await _service.GetAllCinemasAsync();
        return View(allCinemas);
    }

    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var cinema = await _service.GetCinemaAsync(id);
            return View(cinema);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [Authorize(Roles = "Admin,Editor")]
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var cinema = await _service.GetCinemaForEditAsync(id);
            ViewBag.CinemaId = id;
            return View(cinema);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin,Editor")]
    public async Task<IActionResult> Edit(int id, CinemaInputDto cinemaDto)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.CinemaId = id;
            return View(cinemaDto);
        }

        try
        {
            await _service.UpdateCinemaAsync(id, cinemaDto);
            return RedirectToAction(nameof(Index));
        }
        catch (BusinessException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            ViewBag.CinemaId = id;
            return View(cinemaDto);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var cinema = await _service.GetCinemaAsync(id);
            return View(cinema);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            await _service.DeleteCinemaAsync(id);
            return RedirectToAction(nameof(Index));
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
