using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Sample_CleanArchitecture_CQRS.Application.Common.Models.Results;

namespace Sample_CleanArchitecture_CQRS.Application.Common.Abstractions.Messaging.Commands;
internal interface ICommandHandler<in TCommand> :
    IRequestHandler<TCommand> where TCommand : ICommand
{
}

internal interface ICommandHandler<in TCommand, TResponse> :
    IRequestHandler<TCommand, ApiResult<TResponse>> where TCommand : ICommand<TResponse>
{
}
