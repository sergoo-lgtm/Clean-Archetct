using eTickets.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers.Api;

[ApiController]
[Route("api/cinemas")]
public class CinemasApiController : ControllerBase
{
    private readonly CinemaService _service;

    public CinemasApiController(CinemaService service)
    {
        _service = service;
    }

   [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var cinemas = await _service.GetAllCinemasAsync();
        return Ok(cinemas);

    }
}