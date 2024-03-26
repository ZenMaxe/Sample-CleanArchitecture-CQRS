using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using MediatR;
using Sample_CleanArchitecture_CQRS.Application.Common.Models.Results;
using Sample_CleanArchitecture_CQRS.Application.CQRS.Products.Common;
using Sample_CleanArchitecture_CQRS.Application.Resources.Products;
using Sample_CleanArchitecture_CQRS.Domain.Common.Interfaces;
using Sample_CleanArchitecture_CQRS.Domain.Entities.Products;

namespace Sample_CleanArchitecture_CQRS.Application.CQRS.v1.Products.Commands.Delete;


public sealed class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ApiResult<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductRepository _productRepository;

    public DeleteProductCommandHandler(IUnitOfWork unitOfWork,
        IProductRepository productRepository)
    {
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
    }

    public async Task<ApiResult<string>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.ProductId);
        if (product is null)
        {
            return ApiResult<string>.Failed(ProductsErrors.ProductNotFind);
        }



        await _unitOfWork.BeginTransaction();

        _productRepository.Delete(product);

        int result = await _unitOfWork.CommitTransaction();

        if (result < 1)
        {
            return ApiResult<string>.Failed(ProductsErrors.SomethingIsWrong);
        }

        return ApiResult<string>.Success(ProductsResult.ProductHasBeenDeleted);
    }
}

