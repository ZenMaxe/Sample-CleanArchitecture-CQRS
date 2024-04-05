using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Sample_CleanArchitecture_CQRS.Application.Common.Models.Results;

namespace Sample_CleanArchitecture_CQRS.Application.Common.Abstractions.Messaging.Queries;
internal interface IQuery<TResponse> : IRequest<ApiResult<TResponse>>
{
}
