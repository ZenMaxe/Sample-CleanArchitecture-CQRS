using System.ComponentModel.DataAnnotations;

namespace Sample_CleanArchitecture_CQRS.Application.CQRS.v1.Users.Dtos;

public record EditUserDto
{
    public string? UserName { get; }

    public string? FirstName { get; }
    public string? LastName { get; }

    [EmailAddress]
    public string? Email { get; }


    [DataType(DataType.Password)]
    public string? CurrentPassword { get; }

    [DataType(DataType.Password)]
    public string? NewPassword { get; }

    [DataType(DataType.Password)]
    public string? ConfirmationPassword { get; }

    public string? PhoneNumber { get; }


    public EditUserDto(string? userName, string? email,
                       string? phoneNumber, string? password,
                       string? currentPassword,
                       string? confirmationPassword,
                       string? firstName, string? lastName
                       )
    {
        UserName = userName;
        Email = email;
        PhoneNumber = phoneNumber;
        NewPassword = password;
        ConfirmationPassword = confirmationPassword;
        FirstName = firstName;
        LastName = lastName;
        CurrentPassword = currentPassword;
    }
}