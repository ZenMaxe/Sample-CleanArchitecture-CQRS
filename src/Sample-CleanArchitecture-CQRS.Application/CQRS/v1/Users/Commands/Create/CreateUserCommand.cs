﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mapster;

using MediatR;
using Sample_CleanArchitecture_CQRS.Application.Common.Models.Results;
using Sample_CleanArchitecture_CQRS.Application.CQRS.v1.Users.Dtos;

namespace Sample_CleanArchitecture_CQRS.Application.CQRS.v1.Users.Commands.Create;
public sealed record CreateUserCommand(
    string UserName,
    string Email,
    string Password,
    string? PhoneNumber,
    string? FirstName,
    string? LastName) : IRequest<ApiResult<UserDto>>, IMapFrom<CreateUserDto>;


public sealed record CreateUserDto(
    string UserName,
    string Email,
    string Password,
    string? PhoneNumber,
    string? FirstName,
    string? LastName);