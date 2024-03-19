using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using FluentValidation;

using Sample_CleanArchitecture_CQRS.Domain.Common.Resources.Users;

namespace Sample_CleanArchitecture_CQRS.Application.CQRS.Users.Commands.Create;
public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    private const string _phoneNumberRegex = @"^\+[1-9]\d{1,14}$";
    private static readonly string[] _sourceArray = new[] {"[A-Z]", "[a-z]", "[0-9]", "[^a-zA-Z0-9]" };

    public CreateUserCommandValidator()
    {
        RuleFor(c => c.UserName)
            .NotEmpty().WithMessage(UserErrors.UsernameIsRequired)
            .MinimumLength(3).WithMessage(UserErrors.UsernameMinLength);

        RuleFor(c => c.Email)
            .NotEmpty().WithMessage(UserErrors.EmailAddressRequired)
            .EmailAddress().WithMessage(UserErrors.EmailAddressWrongFormat);

        RuleFor(c => c.Password)
            .NotEmpty().WithMessage(UserErrors.PasswordIsRequired)
            .MinimumLength(8).WithMessage(UserErrors.PasswordMinLength)
            .Matches("[A-Z]")
            .Matches("[a-z]")
            .Matches("[0-9]")
            .Matches("[^a-zA-Z0-9]")
            .Must(p => Array.TrueForAll(_sourceArray, pattern => Regex.IsMatch(p, pattern)))
            .WithMessage(UserErrors.PasswordMustBeComplex);

        // Optional Fields
        When(c => !string.IsNullOrWhiteSpace(c.PhoneNumber), () =>
        {
            RuleFor(c => c.PhoneNumber)
                .Matches(_phoneNumberRegex).WithMessage(UserErrors.PhoneNumberWrongFormat);
        });

        When(c => !string.IsNullOrWhiteSpace(c.FirstName), () =>
        {
            RuleFor(c => c.FirstName)
                .MaximumLength(50).WithMessage(UserErrors.FirstNameMaxLength);
        });

        When(c => !string.IsNullOrWhiteSpace(c.LastName), () =>
        {
            RuleFor(c => c.LastName)
                .MaximumLength(50).WithMessage(UserErrors.LastNameMaxLength);
        });
    }
}

