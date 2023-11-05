namespace checkout_kata.Models;

public class PercentDiscount : IDiscount
{
    public char ItemSku { get; set; }
    public int Quantity { get; set; }
    public decimal Value { get; set; }
    public decimal Calculate(IEnumerable<IProduct> products)
    {
        if (products is null)
            throw new ArgumentNullException(nameof(products));
        
        throw new NotImplementedException();
    }
}