using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;
using Sample_CleanArchitecture_CQRS.Application.Common.Interfaces;
using Sample_CleanArchitecture_CQRS.Application.Common.Models.Results;
using Sample_CleanArchitecture_CQRS.Application.CQRS.v1.Authentication.Dtos;

namespace Sample_CleanArchitecture_CQRS.Application.CQRS.v1.Authentication.Commands.Login;


public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, ApiResult<TokenDto>>
{
    private readonly IAuthenticationService _authenticationService;

    public LoginCommandHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }
    public async Task<ApiResult<TokenDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var loginResult = await _authenticationService.LoginAsync(request.Email, request.Password);

        if (!loginResult.IsSuccess)
        {
            return ApiResult<TokenDto>.Failed(loginResult.Errors.ToList());
        }

        return ApiResult<TokenDto>.Success(loginResult.Result!);
    }
}

