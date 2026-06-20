using eTickets.Service;
using eTickets.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers;

[Authorize]
public class ShoppingCartController : Controller
{
    private readonly CartService _cartService;
    private readonly MovieService _movieService;

    public ShoppingCartController(CartService cartService, MovieService movieService)
    {
        _cartService = cartService;
        _movieService = movieService;
    }

    public IActionResult Index()
    {
        var cart = _cartService.GetCart();
        return View(cart);
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart(int id, string? returnUrl)
    {
        if (!User.Identity?.IsAuthenticated ?? true)
        {
            return RedirectToAction("Login", "Account");
        }

        try
        {
            var movie = await _movieService.GetMovieDetailsAsync(id);
            _cartService.AddItem(movie.Id, movie.Name, movie.ImageURL, movie.Price);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }

        if (!string.IsNullOrEmpty(returnUrl))
            return Redirect(returnUrl);

        return RedirectToAction(nameof(Index));
    }
    [HttpPost]
    public IActionResult RemoveFromCart(int id)
    {
        _cartService.RemoveItem(id);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult UpdateQuantity(int id, int quantity)
    {
        _cartService.UpdateQuantity(id, quantity);
        return RedirectToAction(nameof(Index));
    }
}
