using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mapster;

using MediatR;
using Sample_CleanArchitecture_CQRS.Application.Common.Models.Results;
using Sample_CleanArchitecture_CQRS.Application.CQRS.v1.Products.Common;

namespace Sample_CleanArchitecture_CQRS.Application.CQRS.v1.Products.Commands.Create;


public sealed record CreateProductCommand(
    string Name,
    decimal Price
 ) : IRequest<ApiResult<ProductDetailsDto>>, IMapFrom<CreateProductDto>;

public sealed record CreateProductDto(
    string Name,
    decimal Price
 );

