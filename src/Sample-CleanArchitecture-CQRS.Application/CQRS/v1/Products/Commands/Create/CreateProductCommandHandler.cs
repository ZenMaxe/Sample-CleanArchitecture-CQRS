﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mapster;

using MapsterMapper;

using MediatR;
using Sample_CleanArchitecture_CQRS.Application.Common.Abstractions.Messaging.Commands;
using Sample_CleanArchitecture_CQRS.Application.Common.Models.Results;
using Sample_CleanArchitecture_CQRS.Application.CQRS.v1.Products.Common;
using Sample_CleanArchitecture_CQRS.Application.Resources.Products;
using Sample_CleanArchitecture_CQRS.Domain.Common.Interfaces;
using Sample_CleanArchitecture_CQRS.Domain.Entities.Products;

namespace Sample_CleanArchitecture_CQRS.Application.CQRS.v1.Products.Commands.Create;

internal sealed class CreateProductCommandHandler(IProductRepository productRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateProductCommand, ProductDetailsDto>
{

    private readonly IProductRepository _productRepository = productRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ApiResult<ProductDetailsDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        Product product = Product.CreateNew(request.Name, request.Price);

        await _productRepository.Add(product);

        int result = await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (result < 1)
        {
            return ApiResult<ProductDetailsDto>.Failed(ProductsErrors.SomethingIsWrong);
        }


        return ApiResult<ProductDetailsDto>.Success(_mapper.Map<ProductDetailsDto>(product));

    }
}
