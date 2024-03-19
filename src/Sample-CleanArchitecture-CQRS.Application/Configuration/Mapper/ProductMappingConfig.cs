using Mapster;

using Sample_CleanArchitecture_CQRS.Application.CQRS.Products.Commands.Edit;
using Sample_CleanArchitecture_CQRS.Application.CQRS.Products.Common;
using Sample_CleanArchitecture_CQRS.Domain.Entities.Products;

namespace Sample_CleanArchitecture_CQRS.Application.Configuration.Mapper;

public class ProductMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Product, ProductDetailsDto>();

        config.NewConfig<Product, ProductListDto>();

        config.NewConfig<(Guid, EditProductDto), EditProductCommand>()
            .ConstructUsing(x =>
                    new EditProductCommand(x.Item1, x.Item2.Name, x.Item2.Price));
    }
}
