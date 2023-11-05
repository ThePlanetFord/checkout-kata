using checkout_kata.Models;

namespace checkout_kata.Services;

public interface ICheckoutService
{
    int Total();
    void Add(IProduct product);
    IEnumerable<IProduct> Basket { get; }
}