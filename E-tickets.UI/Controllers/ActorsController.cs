using eTickets.Models;
using eTickets.Service;
using eTickets.DTO.ActorDTOS; 
using Microsoft.AspNetCore.Mvc;
using eTickets.Middlewares; 

namespace eTickets.Controllers;

public class ActorsController : Controller
{
    private readonly ActorService _service;
    public ActorsController(ActorService service)
    {
        _service = service; 
    }

    public IActionResult Index()
    {
        var data = _service.GetAllActors();
        return View(data);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ActorInputDto actorDto) 
    {
        if (!ModelState.IsValid) return View(actorDto);

        try
        {
            await _service.AddActorAsync(actorDto);
            return RedirectToAction(nameof(Index));
        }
        catch (BusinessException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
        
            return View(actorDto);
        }
    }
    public async Task<IActionResult> Details(int id)
    {
        var actor = await _service.GetActorAsync(id); 
        return View(actor);
    }
    public async Task<IActionResult> Edit(int id)
    {
        var actor = await _service.GetActorAsync(id);
        return View(actor);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, ActorInputDto actorDto)
    {
        if (!ModelState.IsValid) return View(actorDto);

        await _service.UpdateAsync(id, actorDto);
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(int id)
    {
        var actor = await _service.GetActorAsync(id);
        return View(actor);
    }
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _service.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}