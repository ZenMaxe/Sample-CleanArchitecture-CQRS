using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;
using Sample_CleanArchitecture_CQRS.Application.Common.Models.Results;
using Sample_CleanArchitecture_CQRS.Application.CQRS.v1.Products.Common;

namespace Sample_CleanArchitecture_CQRS.Application.CQRS.v1.Products.Queries.GetAll;


public sealed record GetAllProductsQuery : IRequest<ApiResult<List<ProductListDto>>>;
