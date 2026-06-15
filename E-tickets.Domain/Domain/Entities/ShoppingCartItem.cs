namespace eTickets.Models;

public class ShoppingCartItem
{
    public int MovieId { get; set; }
    public string MovieName { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal LineTotal => Price * Quantity;
}
