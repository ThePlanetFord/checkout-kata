namespace checkout_kata.Models;

public class CashDiscount : IDiscount
{
    public char ItemSku { get; set; }
    public int Quantity { get; set; }
    public decimal Value { get; set; }
    
    public decimal Calculate(IEnumerable<IProduct> products)
    {
        if (products is null)
            throw new ArgumentNullException(nameof(products));

        var originalAmountWithoutDeal = products.Sum(x => x.UnitPrice);
        var amount = (decimal)products.Count() / Quantity;
        return originalAmountWithoutDeal - (Math.Floor(amount) * Value);
    }
}