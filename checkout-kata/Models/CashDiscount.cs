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
        var amount = Math.Floor((decimal)products.Count() / Quantity);
        
        if (amount == 0)
            return 0;

        return originalAmountWithoutDeal - (amount * Value);
    }
}