using System.ComponentModel.DataAnnotations;

namespace Sample_CleanArchitecture_CQRS.Application.CQRS.Users.Dtos;

public record UserWithRolesDto
{
    public UserWithRolesDto(string? firstName, string? lastName, string userName, string email, string? phoneNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        UserName = userName;
        Email = email;
        PhoneNumber = phoneNumber;
    }
    public string? FirstName { get; }

    public string? LastName { get; }

    public string UserName { get; }

    public string Email { get; }

    [DataType(DataType.PhoneNumber)]
    public string? PhoneNumber { get; }

    public List<string> Roles { get; } = new List<string>();


}