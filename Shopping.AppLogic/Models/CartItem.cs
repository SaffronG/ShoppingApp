namespace Shopping.AppLogic.Models;

public class CartItem(string name, decimal price, int quantity) : IEquatable<CartItem>
{
    public string Name = name;
    public decimal Price = price;
    public int Quantity = quantity;
    public bool Equals(CartItem? other) => other is not null && other.Name == Name;
    public override bool Equals(object? obj) => Equals(obj as CartItem);
    public override int GetHashCode() => HashCode.Combine(Name, Price, Quantity);
    public static bool operator ==(CartItem leftItem, CartItem rightItem) => leftItem is null ? rightItem is null : leftItem.Equals(rightItem);
    public static bool operator !=(CartItem leftItem, CartItem rightItem) => leftItem is null ? rightItem is not null : !leftItem.Equals(rightItem);
    public static CartItem operator +(CartItem? left, CartItem? right)
        => left == right
        ? new(left.Name, left.Price + right.Price, left.Quantity + right.Quantity)
        : throw new InvalidOperationException($"Cannot add type CartItem.{left.Name} to CartItem.{right.Name}.");
}
