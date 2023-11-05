namespace checkout_kata.Models;

public interface IProduct
{
    char Sku { get; set; }
    decimal UnitPrice { get; set; }
}