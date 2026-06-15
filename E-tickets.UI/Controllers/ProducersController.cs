using eTickets.DTO.ProducerDTOS;
using eTickets.Middlewares;
using eTickets.Service;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers;

public class ProducersController : Controller
{
    private readonly ProducerService _service;

    public ProducersController(ProducerService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var data = await _service.GetAllProducersAsync();
        return View(data);
    }

    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var producer = await _service.GetProducerAsync(id);
            return View(producer);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var producer = await _service.GetProducerForEditAsync(id);
            ViewBag.ProducerId = id;
            return View(producer);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ProducerInputDto producerDto)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.ProducerId = id;
            return View(producerDto);
        }

        try
        {
            await _service.UpdateProducerAsync(id, producerDto);
            return RedirectToAction(nameof(Index));
        }
        catch (BusinessException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            ViewBag.ProducerId = id;
            return View(producerDto);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var producer = await _service.GetProducerAsync(id);
            return View(producer);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            await _service.DeleteProducerAsync(id);
            return RedirectToAction(nameof(Index));
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
