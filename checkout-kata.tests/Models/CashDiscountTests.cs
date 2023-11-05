using checkout_kata.Models;
using FluentAssertions;

namespace checkout_kata.tests.Models;

public class CashDiscountTests
{
    [Fact]
    public void GivenANullEnumerableOfProducts_WhenCalculating_ThenThrowArgumentNullException()
    {
        FluentActions.Invoking(() => new CashDiscount().Calculate(null))
            .Should()
            .ThrowExactly<ArgumentNullException>()
            .WithParameterName("products");
    }

    [Theory]
    [MemberData(nameof(CashDiscountScenarios))]
    public void GivenASetOfProductsWhenCalculatingDiscount_ReturnExpectedDiscount(IEnumerable<IProduct> products,
        decimal expectedDiscount)
    {
        var cashDiscount = new CashDiscount()
        {
            ItemSku = 'B',
            Quantity = 3,
            Value = 40
        };

        cashDiscount.Calculate(products).Should().Be(expectedDiscount);
    }
    
    public static IEnumerable<object[]> CashDiscountScenarios()
    {
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
                }
            }, 0
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
                }
            }, 5
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
                }
            }, 5
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