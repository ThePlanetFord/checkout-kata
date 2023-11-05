namespace checkout_kata.Models;

public class Product : IProduct
{
    public char Sku { get; set; }
    public decimal UnitPrice { get; set; }
}