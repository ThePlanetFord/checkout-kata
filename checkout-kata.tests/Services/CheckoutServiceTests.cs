using checkout_kata.Models;
using checkout_kata.Services;
using FluentAssertions;
using Moq;

namespace checkout_kata.tests.Services;

public class CheckoutServiceTests
{
    private Mock<IDiscountService> DiscountService;
    private CheckoutService CheckoutService;
    
    public CheckoutServiceTests()
    {
        DiscountService = new Mock<IDiscountService>();
        CheckoutService = new CheckoutService(DiscountService.Object);
    }
    
    
    [Fact]
    public void GivenANullDiscountService_WhenConstructingCheckout_ThenThrowArgumentNullException()
    {
        FluentActions.Invoking(() => new CheckoutService(null))
            .Should()
            .ThrowExactly<ArgumentNullException>()
            .WithParameterName("discountService");
    }

    [Fact]
    public void GivenANullProduct_WhenAddingToBasket_ThenThrowArgumentNullException()
    {
        FluentActions.Invoking(() => CheckoutService.Add(null))
            .Should()
            .ThrowExactly<ArgumentNullException>()
            .WithParameterName("product");
    }

    [Fact]
    public void GivenIAddAProductToTheCheckout_ThenThisShouldShowInsideTheBasket()
    {
        var product = new Product()
        {
            UnitPrice = 10,
            Sku = 'A'
        };
        CheckoutService.Add(product);
        CheckoutService.Basket.Should().BeEquivalentTo(new List<IProduct> { product });
    }

    [Fact]
    public void GivenAProductIsAddedToCheckout_ThenTheTotalShouldBeCalculated()
    {
        var product = new Product()
        {
            UnitPrice = 10,
            Sku = 'A'
        };
        CheckoutService.Add(product);
        CheckoutService.Total().Should().Be(10);
    }
}