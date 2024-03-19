using System.Runtime.CompilerServices;

using Sample_CleanArchitecture_CQRS.Application.Common.Interfaces.Models;

namespace Sample_CleanArchitecture_CQRS.Application.Common.Models.Results;

public class InfraResult<TEntity> : IResult where TEntity : class
{
    public bool IsSuccess { get; }


    /// <summary>
    /// Gets the name of the method that generated the result.
    /// </summary>
    /// <remarks>
    /// Useful for debugging and logging to trace the source of the result.
    /// </remarks>
    public string MethodName { get; }

    public string[] Errors { get; }

    public TEntity? Result { get; }

    private InfraResult(bool isSuccess, string[] errors,
                        TEntity? result, string methodName)
    {
        IsSuccess = isSuccess;
        Errors = errors;
        Result = result;
        MethodName = methodName;
    }


    public static InfraResult<TEntity> Success(
                                               TEntity result,
                                               [CallerMemberName] string method = "Self")
    {

        return new(true, [], result, method);
    }


    public static InfraResult<TEntity> Failed(string[]? errors, [CallerMemberName] string method = "Self")
    {
        return new(false, errors ?? [], null, method);
    }

    public static InfraResult<TEntity> Failed(string error, [CallerMemberName] string method = "Self")
    {
        if (string.IsNullOrEmpty(error))
        {
            return new(false, [], null, method);
        }
        return new(false, [error], null, method);
    }

}