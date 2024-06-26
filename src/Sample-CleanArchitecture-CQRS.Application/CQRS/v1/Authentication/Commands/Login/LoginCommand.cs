﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mapster;

using MediatR;
using Sample_CleanArchitecture_CQRS.Application.Common.Abstractions.Messaging.Commands;
using Sample_CleanArchitecture_CQRS.Application.Common.Models.Results;
using Sample_CleanArchitecture_CQRS.Application.CQRS.v1.Authentication.Dtos;

namespace Sample_CleanArchitecture_CQRS.Application.CQRS.v1.Authentication.Commands.Login;


public sealed record LoginCommand(string Email, string Password) :
    ICommand<TokenDto>, IMapFrom<LoginDto>;




public sealed record LoginDto(string Email, string Password);