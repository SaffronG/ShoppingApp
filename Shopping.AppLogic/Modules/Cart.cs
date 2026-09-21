using Shopping.AppLogic.Models;

namespace Shopping.AppLogic.Modules
{
    public class Cart
    {
        public static List<(string code, decimal discount)> ValidDiscountCodes = [
            ("Buy4Free", 0.2M),
            ("2+1", 0.1M),
            ("FallCase26", 0.25M),
        ];
        public List<CartItem> items = [];
        public decimal total = 0;
        public bool discountApplied = false;
        public void Add(CartItem newItem)
        {
            items.Add(newItem);
            total += newItem.Price * newItem.Quantity;
        }

        public void Remove(string name)
        {
            CartItem currentItem = items.FirstOrDefault(item => item.Name == name)
                ?? throw new ItemNotFoundException(name);
            total -= currentItem.Price * currentItem.Quantity;
            items.Remove(currentItem);
        }

        public void ApplyDiscount(string discountCode)
        {
            if (discountApplied)
                throw new InvalidOperationException("Cannot apply discount code twice");
            var currentDiscount = ValidDiscountCodes.FirstOrDefault(codeTuple => codeTuple.code == discountCode);
            if (currentDiscount.code is null)
                throw new InvalidDiscountCodeException(discountCode);
            total *= currentDiscount.discount;
            discountApplied = true;
        }

        public decimal Checkout(decimal cash)
        {
            if (cash < total)
                throw new InsufficientFundsException(total, cash);

            decimal change = cash - total;
            items.Clear();
            total = 0;
            discountApplied = false;   // if you added the one-discount-per-cart field

            return change;
        }
    }
}