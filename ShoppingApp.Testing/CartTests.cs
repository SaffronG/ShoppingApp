using Shopping.AppLogic.Models;
using Shopping.AppLogic.Modules;
using Shouldly;

namespace ShoppingApp.Testing
{
    public class CartTests
    {
        public static List<CartItem> FactTestCartItems = [
            new CartItem("Ramen", 6.85M, 2),
            new CartItem("Milk", 3.89M, 3),
        ];
        public static TheoryData<CartItem> TheoryTestCartItems => new()
        {
            { new("Ramen", 6.85M, 2) },
            { new("Milk", 3.89M, 3) },
        };
        public static List<string> ValidDiscountCodes = [
            "Buy4Free",
            "2+1",
            "FallCase26"
        ];
        [Theory]
        [MemberData(nameof(TheoryTestCartItems))]
        public void AddSingleItem(CartItem item)
        {
            Cart cart = new();
            cart.Add(item);
            cart.items.Count.ShouldBe(1);
        }
        [Fact]
        public void AddMultipleItems()
        {
            Cart cart = new();
            cart.Add(FactTestCartItems[0]);
            cart.Add(FactTestCartItems[1]);
            cart.items.Count.ShouldBe(2);
        }
        [Fact]
        public void RemoveExistingItem()
        {
            Cart cart = new();
            cart.Add(FactTestCartItems[0]);
            cart.Add(FactTestCartItems[1]);
            cart.Remove(FactTestCartItems[0].Name);
            cart.items.ShouldHaveSingleItem();
            cart.items.First().Name.ShouldBe(FactTestCartItems[1].Name);
        }
        [Theory]
        [InlineData("FallCase26")]
        [InlineData("2+1")]
        [InlineData("Buy4Free")]
        public void ApplyValidDiscountCode(string code)
        {
            Cart cart = new();
            cart.Add(FactTestCartItems[0]);
            cart.Add(FactTestCartItems[1]);
            cart.ApplyDiscount(code);
            cart.total.Equals(20.296);
        }
        [Fact]
        public void CheckoutWithExactAmount()
        {
            Cart cart = new();
            cart.Add(FactTestCartItems[0]);
            cart.Add(FactTestCartItems[1]);
            decimal exactCashAmount = cart.items.Sum(item => item.Price * item.Quantity);
            cart.Checkout(exactCashAmount);
            cart.items.ShouldBeEmpty();
            cart.total.Equals(0);
        }
        [Fact]
        public void CheckoutWithExcessAmount()
        {
            Cart cart = new();
            cart.Add(FactTestCartItems[0]);
            cart.Add(FactTestCartItems[1]);
            decimal excessCashAmount = cart.items.Sum(item => item.Price * item.Quantity) + 20;
            cart.Checkout(excessCashAmount);
            cart.items.ShouldBeEmpty();
            cart.total.ShouldBe(0);
        }
        [Theory]
        [InlineData("FallCase26", "FallCase26")]
        [InlineData("2+1", "Buy4Free")]
        [InlineData("Buy4Free", "2+1")]
        public void ApplyMultipleDiscountCodes(string code1, string code2)
        {
            Cart cart = new();
            cart.Add(FactTestCartItems[0]);
            cart.Add(FactTestCartItems[1]);
            cart.ApplyDiscount(code1);
            Should.Throw<InvalidOperationException>(() => cart.ApplyDiscount(code2));
            cart.total.Equals(20.296);
        }
        [Fact]
        public void CannotCheckoutWithBalance()
        {
            Cart cart = new();
            cart.Add(FactTestCartItems[0]);
            cart.Add(FactTestCartItems[1]);
            var ex = Should.Throw<InsufficientFundsException>(() => cart.Checkout(0));
            cart.items.ShouldNotBeEmpty();
            cart.total.ShouldNotBe(0);
        }
    }
}
