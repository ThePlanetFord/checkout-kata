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

        var valueOfProductOriginally = products.FirstOrDefault().UnitPrice;
        var originalAmountWithoutDeal = products.Sum(x => x.UnitPrice);
        var amountOfDeals = Math.Floor((decimal)products.Count() / Quantity);
        var nonDealProducts = products.Count() % Quantity;

        return originalAmountWithoutDeal - ((amountOfDeals * Value) + (nonDealProducts * valueOfProductOriginally));
    }
}