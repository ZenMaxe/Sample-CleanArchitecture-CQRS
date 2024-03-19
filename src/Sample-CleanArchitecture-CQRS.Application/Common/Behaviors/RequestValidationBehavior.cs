using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;

using MediatR;

using Sample_CleanArchitecture_CQRS.Application.Common.Interfaces.Models;
using Sample_CleanArchitecture_CQRS.Application.Common.Models.Results;


namespace Sample_CleanArchitecture_CQRS.Application.Common.Behaviors;
internal sealed class RequestValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IResult

{
    private readonly IEnumerable<IValidator<TRequest>>? _validators;

    public RequestValidationBehavior(IEnumerable<IValidator<TRequest>>? validators)
        => this._validators = validators;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (_validators is null)
        {
            return await next();
        }
        var context = new ValidationContext<TRequest>(request);

        var validationFailures = await Task.WhenAll(
            _validators.Select(validator => validator.ValidateAsync(context)));

        var errors = validationFailures
            .Where(validationResult => !validationResult.IsValid)
            .SelectMany(validationResult => validationResult.Errors)
            .Select(validationFailure => validationFailure.ErrorMessage)
            .Distinct()
            .ToList();
        if (errors.Count != 0)
        {
            // TResponse is ApiResult in This Behavior

            return createApiResult<TResponse>(errors);
        }

        return await next();
    }

    private static TResult createApiResult<TResult>(List<string> errors)
        where TResult : IResult
    {
        Type type = typeof(TResult).GetGenericArguments()[0];
        object data = typeof(ApiResult<>)
                            .GetGenericTypeDefinition()
                            .MakeGenericType(type)
                            .GetMethod(nameof(ApiResult<object>.Failed), [typeof(List<string>), typeof(int)])!
                            .Invoke(null, [errors, 400])!;

        return (TResult) data;

    }
}