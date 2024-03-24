using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;

using Sample_CleanArchitecture_CQRS.Domain.Common.Resources.Products;

namespace Sample_CleanArchitecture_CQRS.Application.CQRS.Products.Commands.Delete;
public sealed class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(command => command.ProductId)
            .NotEmpty()
            .WithMessage(ProductErrors.ProductIdMustBeProvided);
    }
}

