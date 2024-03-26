namespace Sample_CleanArchitecture_CQRS.Application.CQRS.v1.Products.Common;


// For This Project Nothing Different Between details and list
public class ProductListDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }
}