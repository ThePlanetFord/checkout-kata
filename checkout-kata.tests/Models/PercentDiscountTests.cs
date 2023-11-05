using checkout_kata.Models;
using FluentAssertions;

namespace checkout_kata.tests.Models;

public class PercentDiscountTests
{
    [Fact]
    public void GivenANullEnumerableOfProducts_WhenCalculating_ThenThrowArgumentNullException()
    {
        FluentActions.Invoking(() => new PercentDiscount().Calculate(null))
            .Should()
            .ThrowExactly<ArgumentNullException>()
            .WithParameterName("products");
    }

    [Theory]
    [MemberData(nameof(PercentDiscountScenarios))]
    public void GivenASetOfProductsWhenCalculatingDiscount_ReturnExpectedDiscount(IEnumerable<IProduct> products,
        decimal expectedDiscount)
    {
        var percentDiscount = new PercentDiscount()
        {
            ItemSku = 'D',
            Quantity = 2,
            Value = 0.75M
        };

        percentDiscount.Calculate(products).Should().Be(expectedDiscount);
    }
    
    public static IEnumerable<object[]> PercentDiscountScenarios()
    {
        yield return new object[]
        {
            new List<IProduct>()
            {
                new Product()
                {
                    Sku = 'D',
                    UnitPrice = 55
                },
                new Product()
                {
                    Sku = 'D',
                    UnitPrice = 55
                }
            }, 96.25M
        };
        
        yield return new object[]
        {
            new List<IProduct>()
            {
                new Product()
                {
                    Sku = 'D',
                    UnitPrice = 55
                },
                new Product()
                {
                    Sku = 'D',
                    UnitPrice = 55
                },
                new Product()
                {
                    Sku = 'D',
                    UnitPrice = 55
                }
            }, 151.25
        };
        
        yield return new object[]
        {
            new List<IProduct>()
            {
                new Product()
                {
                    Sku = 'D',
                    UnitPrice = 55
                },
                new Product()
                {
                    Sku = 'D',
                    UnitPrice = 55
                },
                new Product()
                {
                    Sku = 'D',
                    UnitPrice = 55
                },
                new Product()
                {
                    Sku = 'D',
                    UnitPrice = 55
                }
            }, 192.5M
        };
        
        yield return new object[]
        {
            new List<IProduct>()
            {
                new Product()
                {
                    Sku = 'B',
                    UnitPrice = 15
                },
                new Product()
                {
                    Sku = 'B',
                    UnitPrice = 15
                },
                new Product()
                {
                    Sku = 'B',
                    UnitPrice = 15
                },
                new Product()
                {
                    Sku = 'B',
                    UnitPrice = 15
                },
                new Product()
                {
                    Sku = 'B',
                    UnitPrice = 15
                },
                new Product()
                {
                    Sku = 'B',
                    UnitPrice = 15
                    }
            }, 10
        };
    }
}