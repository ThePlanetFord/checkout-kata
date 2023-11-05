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
        decimal total = 0;
        foreach (var item in _basket)
        {
            total = item.UnitPrice + total;
        }
        return total;
    }

    public void Add(IProduct product)
    {
        if (product is null)
            throw new ArgumentNullException(nameof(product));
        
        _basket.Add(product);
    }

    public IEnumerable<IProduct> Basket => _basket;
}