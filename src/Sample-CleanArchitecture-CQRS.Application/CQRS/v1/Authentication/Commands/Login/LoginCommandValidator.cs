using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using FluentValidation;
using Sample_CleanArchitecture_CQRS.Domain.Common.Resources.Users;

namespace Sample_CleanArchitecture_CQRS.Application.CQRS.v1.Authentication.Commands.Login;

public sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(c => c.Email)
            .NotEmpty().WithMessage(UserErrors.EmailAddressRequired)
            .EmailAddress().WithMessage(UserErrors.EmailAddressWrongFormat);

        RuleFor(c => c.Password)
            .NotEmpty().WithMessage(UserErrors.PasswordIsRequired)
            .MinimumLength(8).WithMessage(UserErrors.PasswordMinLength);

    }

}

