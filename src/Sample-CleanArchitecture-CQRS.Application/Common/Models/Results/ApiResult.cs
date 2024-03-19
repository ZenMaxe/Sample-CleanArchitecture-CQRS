using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Sample_CleanArchitecture_CQRS.Application.Common.Interfaces.Models;
using Sample_CleanArchitecture_CQRS.Domain.Common.Interfaces;

namespace Sample_CleanArchitecture_CQRS.Application.Common.Models.Results;
public class ApiResult<TResult> : IResult
{
    public bool IsSuccess { get; }
    public TResult? Data { get; }
    public List<string> Errors { get; } = [];

    public int StatusCode { get; }

    public ApiResult()
    {
        // For Producer and Behavior
    }

    private ApiResult(TResult data, int statusCode)
    {
        Data = data;
        StatusCode = statusCode;
        IsSuccess = true;
    }

    private ApiResult(List<string> errors, int statusCode)
    {
        IsSuccess = false;
        StatusCode = statusCode;
        Errors = errors;
    }

    public static ApiResult<TResult> Success(TResult data, int statusCode = 200)
    {
        return new ApiResult<TResult>(data, statusCode);
    }

    public static ApiResult<TResult> Failed(List<string> errors, int statusCode = 400)
    {
        return new(errors, statusCode);
    }

    public static ApiResult<TResult> Failed(string error, int statusCode = 400)
    {
        if (string.IsNullOrEmpty(error))
        {
            return new([], statusCode);
        }
        return new([error], statusCode);
    }

}

