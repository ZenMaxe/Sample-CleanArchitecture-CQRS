﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mapster;

using MediatR;
using Sample_CleanArchitecture_CQRS.Application.Common.Abstractions.Messaging.Commands;
using Sample_CleanArchitecture_CQRS.Application.Common.Models.Results;
using Sample_CleanArchitecture_CQRS.Application.CQRS.v1.Products.Common;

namespace Sample_CleanArchitecture_CQRS.Application.CQRS.v1.Products.Commands.Delete;


public sealed record DeleteProductCommand(Guid ProductId) : ICommand<string>, IMapFrom<DeleteProductDto>;

public sealed record DeleteProductDto(Guid ProductId);