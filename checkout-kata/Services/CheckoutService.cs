using checkout_kata.Models;

namespace checkout_kata.Services;

public class CheckoutService : ICheckoutService
{
    private readonly IDiscountService _discountService;
    private readonly IList<IProduct> _basket = new List<IProduct>();
    
    public CheckoutService(IDiscountService discountService)
    {
        _discountService = discountService ?? throw new ArgumentNullException(nameof(discountService));
    }

    public decimal Total()
    {
        var discounts = _discountService.GetDiscounts();
        var discount = discounts.Sum(x => CalculateDiscount(x, _basket));
        var total = _basket.Sum(x => x.UnitPrice);

        return total - discount;
    }

    public void Add(IProduct product)
    {
        if (product is null)
            throw new ArgumentNullException(nameof(product));
        
        _basket.Add(product);
    }
    
    private static decimal CalculateDiscount(IDiscount discount, IEnumerable<IProduct> cart)
    {
        var applicableProducts = cart.Where(product => product.Sku == discount.ItemSku);
        return discount.Calculate(applicableProducts);
    }

    public IEnumerable<IProduct> Basket => _basket;
}