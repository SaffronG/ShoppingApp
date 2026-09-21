using Shopping.AppLogic.Models;

namespace Shopping.AppLogic.Modules
{
    public class Cart
    {
        public List<CartItem> items = [];
        public decimal total = 0;

        public void add(string name, decimal price, int qty)
        {
            items.Add(new(name, price, qty));
            total = total + price * qty;
        }

        public void remove(string name)
        {
            foreach (var i in items)
            {
                if (i.Name == name)
                {
                    total = total - i.Price * i.Price;
                    items.Remove(i);
                    return;
                }
            }
            Console.WriteLine("not found");
        }

        public void applyDiscount(string code)
        {
            if (code == "SAVE10")
            {
                total = total - total * 0.1M;
            }
            else if (code == "SAVE20")
            {
                total = total - total * 0.2M;
            }
            else
            {
                Console.WriteLine("invalid code");
            }
        }

        public void checkout(decimal cash)
        {
            if (cash < total)
            {
                Console.WriteLine("not enough money");
            }
            else
            {
                decimal change = cash - total;
                Console.WriteLine("Change: " + change);
                items.Clear();
                total = 0;
            }
        }
    }
}