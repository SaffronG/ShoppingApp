namespace Shopping.AppLogic.Models;

/// <summary>
/// Base type for every error raised by the shopping cart domain.
/// </summary>
public abstract class CartException : Exception
{
    protected CartException() { }

    protected CartException(string message) : base(message) { }

    protected CartException(string message, Exception innerException)
        : base(message, innerException) { }
}

public sealed class ItemNotFoundException : CartException
{
    public string ItemName { get; }

    public ItemNotFoundException(string itemName)
        : base($"Item {itemName} could not be found.")
        => ItemName = itemName;

    public ItemNotFoundException(string itemName, Exception innerException)
        : base($"Item {itemName} could not be found.", innerException)
        => ItemName = itemName;
}

public sealed class InvalidQuantityException : CartException
{
    public int Quantity { get; }

    public InvalidQuantityException(int quantity)
        : base($"{quantity} is not a valid item quantity.")
        => Quantity = quantity;

    public InvalidQuantityException(int quantity, Exception innerException)
        : base($"{quantity} is not a valid item quantity.", innerException)
        => Quantity = quantity;
}

public sealed class InvalidPriceException : CartException
{
    public decimal Price { get; }

    public InvalidPriceException(decimal price)
        : base($"Price ${price} is not a valid value.")
        => Price = price;

    public InvalidPriceException(decimal price, Exception innerException)
        : base($"Price ${price} is not a valid value.", innerException)
        => Price = price;
}

public sealed class InvalidDiscountCodeException : CartException
{
    public string Code { get; }

    public InvalidDiscountCodeException(string code)
        : base($"The provided discount code {code} is not valid.")
        => Code = code;

    public InvalidDiscountCodeException(string code, Exception innerException)
        : base($"The provided discount code {code} is not valid.", innerException)
        => Code = code;
}

public sealed class InsufficientFundsException : CartException
{
    public decimal Required { get; }
    public decimal Available { get; }

    public InsufficientFundsException(decimal required, decimal available)
        : base($"Insufficient funds: the order requires ${required} but only ${available} is available.")
    {
        Required = required;
        Available = available;
    }

    public InsufficientFundsException(string message) : base(message) { }

    public InsufficientFundsException(string message, Exception innerException)
        : base(message, innerException) { }
}
