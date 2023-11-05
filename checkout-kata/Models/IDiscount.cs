namespace checkout_kata.Models;

public interface IDiscount
{
    char ItemSku { get; set; }
    int Quantity { get; set; }
    decimal Value { get; set; }
}