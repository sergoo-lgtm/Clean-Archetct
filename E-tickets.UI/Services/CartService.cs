using eTickets.Extensions;
using eTickets.Models;
using eTickets.ViewModels;

namespace eTickets.Services;

public class CartService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private const string CartSessionKey = "Cart";

    public CartService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private ISession Session => _httpContextAccessor.HttpContext!.Session;

    private List<ShoppingCartItem> GetCartItems()
    {
        return Session.GetObject<List<ShoppingCartItem>>(CartSessionKey) ?? [];
    }

    private void SaveCartItems(List<ShoppingCartItem> items)
    {
        Session.SetObject(CartSessionKey, items);
    }

    public ShoppingCartViewModel GetCart()
    {
        var items = GetCartItems();
        return new ShoppingCartViewModel
        {
            Items = items,
            Total = items.Sum(i => i.LineTotal)
        };
    }

    public void AddItem(int movieId, string movieName, string imageUrl, double price)
    {
        var items = GetCartItems();
        var existing = items.FirstOrDefault(i => i.MovieId == movieId);

        if (existing != null)
        {
            existing.Quantity++;
        }
        else
        {
            items.Add(new ShoppingCartItem
            {
                MovieId = movieId,
                MovieName = movieName,
                ImageUrl = imageUrl,
                Price = (decimal)price,
                Quantity = 1
            });
        }

        SaveCartItems(items);
    }

    public void RemoveItem(int movieId)
    {
        var items = GetCartItems();
        items.RemoveAll(i => i.MovieId == movieId);
        SaveCartItems(items);
    }

    public void UpdateQuantity(int movieId, int quantity)
    {
        var items = GetCartItems();
        var item = items.FirstOrDefault(i => i.MovieId == movieId);
        if (item != null)
        {
            if (quantity <= 0)
                items.Remove(item);
            else
                item.Quantity = quantity;
        }
        SaveCartItems(items);
    }

    public int GetCartCount()
    {
        return GetCartItems().Sum(i => i.Quantity);
    }

    public void ClearCart()
    {
        Session.Remove(CartSessionKey);
    }
}
