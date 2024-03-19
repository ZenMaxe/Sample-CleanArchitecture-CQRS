using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mapster;

using MediatR;

using Sample_CleanArchitecture_CQRS.Application.Common.Models.Results;
using Sample_CleanArchitecture_CQRS.Application.CQRS.Products.Common;

namespace Sample_CleanArchitecture_CQRS.Application.CQRS.Products.Commands.Create;


public record CreateProductCommand(
    string Name,
    decimal Price
 ) : IRequest<ApiResult<ProductDetailsDto>>, IMapFrom<CreateProductDto>;

public record CreateProductDto(
    string Name,
    decimal Price
 );

