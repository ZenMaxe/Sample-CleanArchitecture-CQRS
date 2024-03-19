using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;

using Sample_CleanArchitecture_CQRS.Domain.Common.Resources.Products;

namespace Sample_CleanArchitecture_CQRS.Application.CQRS.Products.Commands.Edit;


public class EditProductCommandValidator : AbstractValidator<EditProductCommand>
{

    public EditProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .When(x => x.Name is not null)
            .WithMessage(ProductErrors.NameCannotBeEmpty);

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .When(x => x.Price.HasValue)
            .WithMessage(ProductErrors.PriceCannotBeNegative);
    }
}

