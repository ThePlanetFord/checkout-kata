namespace checkout_kata.Models;

public class Discount : IDiscount
{
    public char ItemSku { get; set; }
    public int Quantity { get; set; }
    public decimal Value { get; set; }
}