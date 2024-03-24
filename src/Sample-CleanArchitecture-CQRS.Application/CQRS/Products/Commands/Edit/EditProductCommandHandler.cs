using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mapster;

using MapsterMapper;

using MediatR;

using Sample_CleanArchitecture_CQRS.Application.Common.Models.Results;
using Sample_CleanArchitecture_CQRS.Application.CQRS.Products.Common;
using Sample_CleanArchitecture_CQRS.Application.Resources.Products;
using Sample_CleanArchitecture_CQRS.Domain.Common.Interfaces;
using Sample_CleanArchitecture_CQRS.Domain.Entities.Products;

namespace Sample_CleanArchitecture_CQRS.Application.CQRS.Products.Commands.Edit;
public sealed class EditProductCommandHandler : IRequestHandler<EditProductCommand, ApiResult<ProductDetailsDto>>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;


    public EditProductCommandHandler(IUnitOfWork unitOfWork,
        IProductRepository productRepository,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
        _mapper = mapper;
    }
    public async Task<ApiResult<ProductDetailsDto>> Handle(EditProductCommand request, CancellationToken cancellationToken)
    {
        Product? product = await _productRepository.GetByIdAsync(request.ProductId);
        if (product is null)
        {
            return ApiResult<ProductDetailsDto>.Failed(ProductsErrors.ProductNotFind);
        }
        await _unitOfWork.BeginTransaction();

        product.UpdateProduct(request.Name, request.Price);

        int result = await _unitOfWork.CommitTransaction();

        if (result < 1)
        {
            return ApiResult<ProductDetailsDto>.Failed(ProductsErrors.SomethingIsWrong);
        }

        var dto = _mapper.Map<ProductDetailsDto>(product);
        return ApiResult<ProductDetailsDto>.Success(dto);
    }
}

