using checkout_kata.Models;

namespace checkout_kata.Services;

public interface IDiscountService
{
    IEnumerable<IDiscount> GetDiscounts();
}