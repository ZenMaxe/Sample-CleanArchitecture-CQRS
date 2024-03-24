using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;

using Sample_CleanArchitecture_CQRS.Application.Resources.Products;
using Sample_CleanArchitecture_CQRS.Domain.Common.Resources.Products;

namespace Sample_CleanArchitecture_CQRS.Application.CQRS.Products.Queries.Details;
public sealed class ProductDetailsQueryValidator : AbstractValidator<ProductDetailsQuery>
{
    public ProductDetailsQueryValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage(ProductErrors.ProductIdMustBeProvided);
    }
}

