using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sample_CleanArchitecture_CQRS.Domain.Common.Resources.Products;


namespace Sample_CleanArchitecture_CQRS.Domain.Entities.Products;
public class Product
{
    public Guid Id { get; } = Guid.NewGuid();

    public string Name { get; private set; } = string.Empty;

    public decimal Price { get; private set; }

    public Product()
    {
        
    }
    private Product(string name, decimal price)
    {
        if (price < 0)
            throw new ArgumentException(ProductErrors.PriceCannotBeNegative);

        Name = name;

        Price = price;
    }

    public static Product CreateNew(string name, decimal price)
    {
        return new Product(name, price);
    }

    public void UpdateProduct(string? name, decimal? price)
    {

        if (!string.IsNullOrWhiteSpace(name))
        {
            Name = name;
        }

        if (price.HasValue)
        {
            UpdatePrice(price.Value);
        }
    }

    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice < 0)
            throw new ArgumentException(ProductErrors.PriceCannotBeNegative);
        Price = newPrice;
    }

}

