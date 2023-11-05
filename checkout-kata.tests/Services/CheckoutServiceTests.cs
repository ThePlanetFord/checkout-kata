using checkout_kata.Services;
using FluentAssertions;

namespace checkout_kata.tests.Services;

public class CheckoutServiceTests
{
    [Fact]
    public void GivenANullDiscountService_WhenConstructingCheckout_ThenThrowArgumentNullException()
    {
        FluentActions.Invoking(() => new CheckoutService(null))
            .Should()
            .ThrowExactly<ArgumentNullException>()
            .WithParameterName("discountService");
    }
}