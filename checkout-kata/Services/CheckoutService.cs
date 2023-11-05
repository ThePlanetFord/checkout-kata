namespace checkout_kata.Services;

public class CheckoutService : ICheckoutService
{
    private readonly IDiscountService _discountService;


    public CheckoutService(IDiscountService discountService)
    {
        _discountService = discountService;
    }

    public int Total()
    {
        throw new NotImplementedException();
    }

    public void Add(string product)
    {
        throw new NotImplementedException();
    }
}