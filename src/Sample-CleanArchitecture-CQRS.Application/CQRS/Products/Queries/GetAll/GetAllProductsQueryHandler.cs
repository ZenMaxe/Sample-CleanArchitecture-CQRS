using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MapsterMapper;

using MediatR;

using Sample_CleanArchitecture_CQRS.Application.Common.Models.Results;
using Sample_CleanArchitecture_CQRS.Application.CQRS.Products.Common;
using Sample_CleanArchitecture_CQRS.Domain.Entities.Products;

namespace Sample_CleanArchitecture_CQRS.Application.CQRS.Products.Queries.GetAll;

public sealed class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, ApiResult<List<ProductListDto>>>
{

    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public GetAllProductsQueryHandler(IProductRepository productRepository,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }
    public async Task<ApiResult<List<ProductListDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllAsync(cancellationToken);
        var data = _mapper.Map<IEnumerable<ProductListDto>>(products);
        return ApiResult<List<ProductListDto>>.Success(data.ToList());
    }
}

