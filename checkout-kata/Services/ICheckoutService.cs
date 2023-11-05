namespace checkout_kata.Services;

public interface ICheckoutService
{
    int Total();
    void Add(string product);
}