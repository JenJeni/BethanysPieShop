using System.Reflection.Metadata.Ecma335;

namespace BethanysPieShop.Models
{
    public class ShoppingCartItem
    {
        public int ShoppingCartItemId { get; set; } //Will store all items that are associated with the GUID
        public Pie Pie { get; set; } = default!;
        public int Amount { get; set; }
        public string? ShoppingCartId { get; set; } //A string that represents a GUID
    }
}
