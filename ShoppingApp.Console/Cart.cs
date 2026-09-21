public class Cart
{
    public List<object[]> items = new List<object[]>();
    public double total = 0;

    public void add(string name, double price, int qty)
    {
        items.Add(new object[] { name, price, qty });
        total = total + price * qty;
    }

    public void remove(string name)
    {
        foreach (var i in items)
        {
            if ((string)i[0] == name)
            {
                total = total - (double)i[1] * (int)i[2];
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
            total = total - total * 0.1;
        }
        else if (code == "SAVE20")
        {
            total = total - total * 0.2;
        }
        else
        {
            Console.WriteLine("invalid code");
        }
    }

    public void checkout(double cash)
    {
        if (cash < total)
        {
            Console.WriteLine("not enough money");
        }
        else
        {
            double change = cash - total;
            Console.WriteLine("Change: " + change);
            items.Clear();
            total = 0;
        }
    }
}