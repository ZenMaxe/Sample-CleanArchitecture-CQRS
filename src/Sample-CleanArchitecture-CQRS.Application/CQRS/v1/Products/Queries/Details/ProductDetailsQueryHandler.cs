using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MapsterMapper;

using MediatR;
using Sample_CleanArchitecture_CQRS.Application.Common.Models.Results;
using Sample_CleanArchitecture_CQRS.Application.CQRS.v1.Products.Common;
using Sample_CleanArchitecture_CQRS.Application.Resources.Products;
using Sample_CleanArchitecture_CQRS.Domain.Entities.Products;

namespace Sample_CleanArchitecture_CQRS.Application.CQRS.v1.Products.Queries.Details;


public sealed class ProductDetailsQueryHandler : IRequestHandler<ProductDetailsQuery, ApiResult<ProductDetailsDto>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;


    public ProductDetailsQueryHandler(IMapper mapper,
        IProductRepository productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }
    public async Task<ApiResult<ProductDetailsDto>> Handle(ProductDetailsQuery request, CancellationToken cancellationToken)
    {
        Product? product = await _productRepository.GetByIdAsync(request.ProductId, track: false);
        if (product is null)
        {
            return ApiResult<ProductDetailsDto>.Failed(ProductsErrors.ProductNotFind);
        }

        var mapped = _mapper.Map<ProductDetailsDto>(product);
        return ApiResult<ProductDetailsDto>.Success(mapped);
    }
}

