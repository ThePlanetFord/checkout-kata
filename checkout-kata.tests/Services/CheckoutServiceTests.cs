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

    [Theory]
    [MemberData(nameof(GetScenarioThreeCheckOutData))]
    public void GivenDiscountsExistInBasket_ThenApplyDiscountToTotal(IEnumerable<Product> products,
        IEnumerable<IDiscount> discounts, decimal expectedPrice)
    {
        DiscountService.Setup(x => x.GetDiscounts())
            .Returns(discounts);
        foreach (var product in products)
        {
            CheckoutService.Add(product);
        }

        CheckoutService.Basket.Should().BeEquivalentTo(products);
        CheckoutService.Total().Should().Be(expectedPrice);
    }

    [Fact]
    public void GivenThereIsNoDiscountsAvailable_WhenCalculatingCheckout_ThenReturnExpected()
    {
        DiscountService.Setup(x => x.GetDiscounts())
            .Returns(Enumerable.Empty<IDiscount>());

        var products = new List<IProduct>
        {
            new Product() { Sku = 'A', UnitPrice = 10 },
            new Product() { Sku = 'B', UnitPrice = 15 },
            new Product() { Sku = 'C', UnitPrice = 40 },
            new Product() { Sku = 'D', UnitPrice = 55 }
        };

        foreach (var product in products)
        {
            CheckoutService.Add(product);
        }

        CheckoutService.Basket.Should().BeEquivalentTo(products);
        CheckoutService.Total().Should().Be(120);
    }

    [Theory]
    [MemberData(nameof(MultipleDiscountsPresent))]
    public void GivenMultipleDiscountsArePresent_ThenCalculateExpectedBasketTotal(IEnumerable<IDiscount> discounts,
        IEnumerable<Product> products, decimal expectedPrice)
    {
        DiscountService.Setup(x => x.GetDiscounts())
            .Returns(discounts);
        foreach (var product in products)
        {
            CheckoutService.Add(product);
        }

        CheckoutService.Basket.Should().BeEquivalentTo(products);
        CheckoutService.Total().Should().Be(expectedPrice);
    }

    public static IEnumerable<object[]> GetScenarioThreeCheckOutData()
    {
        yield return new object[]
        {
            new List<Product>
            {
                new() { Sku = 'A', UnitPrice = 10 },
                new() { Sku = 'B', UnitPrice = 15 },
                new() { Sku = 'B', UnitPrice = 15 },
                new() { Sku = 'B', UnitPrice = 15 }
            },
            new List<IDiscount>
            {
                new CashDiscount()
                {
                    ItemSku = 'B',
                    Quantity = 3,
                    Value = 40
                }
            },
            50
        };

        yield return new object[]
        {
            new List<Product>
            {
                new() { Sku = 'A', UnitPrice = 10 },
                new() { Sku = 'B', UnitPrice = 15 },
                new() { Sku = 'B', UnitPrice = 15 },
                new() { Sku = 'C', UnitPrice = 40 }
            },
            new List<IDiscount>
            {
                new CashDiscount()
                {
                    ItemSku = 'B',
                    Quantity = 3,
                    Value = 40
                }
            },
            80
        };

        yield return new object[]
        {
            new List<Product>
            {
                new() { Sku = 'A', UnitPrice = 10 },
                new() { Sku = 'B', UnitPrice = 15 },
                new() { Sku = 'B', UnitPrice = 15 },
                new() { Sku = 'B', UnitPrice = 15 },
                new() { Sku = 'B', UnitPrice = 15 },
                new() { Sku = 'C', UnitPrice = 40 }
            },
            new List<IDiscount>
            {
                new CashDiscount()
                {
                    ItemSku = 'B',
                    Quantity = 3,
                    Value = 40
                }
            },
            105
        };
    }

    public static IEnumerable<object[]> MultipleDiscountsPresent()
    {
        yield return new object[]
        {
            new List<IDiscount>()
            {
                new CashDiscount()
                {
                    ItemSku = 'B',
                    Quantity = 3,
                    Value = 40
                },
                new PercentDiscount()
                {
                    ItemSku = 'D',
                    Quantity = 2,
                    Value = 0.25M
                }
            },
            new List<Product>
            {
                new() { Sku = 'A', UnitPrice = 10 },
                new() { Sku = 'B', UnitPrice = 15 },
                new() { Sku = 'B', UnitPrice = 15 },
                new() { Sku = 'B', UnitPrice = 15 },
                new() { Sku = 'B', UnitPrice = 15 },
                new() { Sku = 'C', UnitPrice = 40 },
                new() { Sku = 'D', UnitPrice = 55 },
                new() { Sku = 'D', UnitPrice = 55 },
                new() { Sku = 'D', UnitPrice = 55 }
            },
            151.25M
        };
    }
}