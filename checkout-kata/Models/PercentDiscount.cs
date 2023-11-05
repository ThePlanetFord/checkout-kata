namespace checkout_kata.Models;

public class PercentDiscount : IDiscount
{
    public char ItemSku { get; set; }
    public int Quantity { get; set; }
    public decimal Value { get; set; }
    public decimal Calculate(IEnumerable<IProduct> products)
    {
        throw new NotImplementedException();
    }
}