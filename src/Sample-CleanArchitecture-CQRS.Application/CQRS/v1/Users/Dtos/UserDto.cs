using System.ComponentModel.DataAnnotations;

namespace Sample_CleanArchitecture_CQRS.Application.CQRS.v1.Users.Dtos;

public record UserDto
{

    public string? FirstName { get; }

    public string? LastName { get; }

    public string UserName { get; }

    public string Email { get; }

    [DataType(DataType.PhoneNumber)]
    public string? PhoneNumber { get; }

    public UserDto(string userName, string email,
                   string? phoneNumber, string? firstName,
                   string? lastName)
    {
        UserName = userName;
        Email = email;
        PhoneNumber = phoneNumber;
        FirstName = firstName;
        LastName = lastName;
    }

}