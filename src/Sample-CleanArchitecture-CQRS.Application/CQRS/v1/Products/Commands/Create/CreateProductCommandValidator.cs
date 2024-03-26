using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;
using Sample_CleanArchitecture_CQRS.Domain.Common.Resources.Products;

namespace Sample_CleanArchitecture_CQRS.Application.CQRS.v1.Products.Commands.Create;
public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {

        RuleFor(x => x.Name).NotEmpty()
            .WithMessage(ProductErrors.NameCannotBeEmpty);


        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage(ProductErrors.PriceCannotBeNegative);
    }
}

