using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MapsterMapper;

using MediatR;

using Microsoft.AspNetCore.Identity;
using Sample_CleanArchitecture_CQRS.Application.Common.Abstractions.Messaging.Commands;
using Sample_CleanArchitecture_CQRS.Application.Common.Interfaces;
using Sample_CleanArchitecture_CQRS.Application.Common.Models.Results;
using Sample_CleanArchitecture_CQRS.Application.CQRS.v1.Users.Dtos;

namespace Sample_CleanArchitecture_CQRS.Application.CQRS.v1.Users.Commands.Create;
internal sealed class CreateUserCommandHandler :
    ICommandHandler<CreateUserCommand, UserDto>
{
    private readonly IUserService _userService;

    public CreateUserCommandHandler(
        IUserService userService)
    {
        _userService = userService;
    }

    public async Task<ApiResult<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.CreateUserAsync(request.UserName, request.Password,
                                              request.Email, request.FirstName,
                                            request.LastName, request.PhoneNumber);

        if (!user.IsSuccess)
        {
            return ApiResult<UserDto>.Failed(user.Errors.ToList());
        }


        return ApiResult<UserDto>.Success(user.Result!);
    }
}

