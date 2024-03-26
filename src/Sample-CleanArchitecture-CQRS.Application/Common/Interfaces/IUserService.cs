using Sample_CleanArchitecture_CQRS.Application.Common.Models.Results;
using Sample_CleanArchitecture_CQRS.Application.CQRS.v1.Users.Dtos;

namespace Sample_CleanArchitecture_CQRS.Application.Common.Interfaces;

public interface IUserService
{
    Task<InfraResult<string>> AssignUserToRole(string userName, ICollection<string> roles);

    Task<InfraResult<UserDto>> CreateUserAsync(string userName,
                                               string password,
                                               string email,
                                               string? firstName,
                                               string? lastName,
                                               string? phoneNumber,
                                               ICollection<string>? roles = null);

    Task<InfraResult<string>> DeleteUserAsync(string userId);

    Task<InfraResult<UserDto>> UpdateUserDetailsAsync(string userId, EditUserDto model);
}