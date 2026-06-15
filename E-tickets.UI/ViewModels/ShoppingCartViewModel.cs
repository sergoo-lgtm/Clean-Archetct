using eTickets.Models;

namespace eTickets.ViewModels;

public class ShoppingCartViewModel
{
    public List<ShoppingCartItem> Items { get; set; } = new();
    public decimal Total { get; set; }
}
