using MapsterMapper;

using Microsoft.AspNetCore.Identity;

using Sample_CleanArchitecture_CQRS.Application.Common.Interfaces;
using Sample_CleanArchitecture_CQRS.Application.Common.Models.Results;
using Sample_CleanArchitecture_CQRS.Application.CQRS.v1.Users.Dtos;
using Sample_CleanArchitecture_CQRS.Domain.Common.Interfaces;
using Sample_CleanArchitecture_CQRS.Infrastructure.Models.Identity;
using Sample_CleanArchitecture_CQRS.Infrastructure.Resources.Services.UserService;

namespace Sample_CleanArchitecture_CQRS.Infrastructure.Services.Identity;

public sealed class UserService : IUserService
{

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;



    public UserService(UserManager<ApplicationUser> userManager,
                       IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }


    public async Task<InfraResult<string>> AssignUserToRole(string userName, ICollection<string> roles)
    {
        var user = await _userManager.FindByNameAsync(userName);

        if (user == null)
        {
            return InfraResult<string>.Failed(UserServiceErrors.UserNotFound);
        }


        var result = await _userManager.AddToRolesAsync(user, roles);

        if (!result.Succeeded)
        {
            return InfraResult<string>.Failed(result.Errors.Select(x => x.Description).ToArray());
        }

        return InfraResult<string>.Success(UserServiceResults.RequstedRolesSuccessfullyAssigned);
    }


    public async Task<InfraResult<UserDto>> CreateUserAsync(string userName,
                                            string password,
                                            string email,
                                            string? firstName,
                                            string? lastName,
                                            string? phoneNumber,
                                            ICollection<string>? roles = null)
    {

        var user = new ApplicationUser()
        {
            UserName = userName,
            Email = email,
        };

        if (!string.IsNullOrEmpty(firstName) &&
            !string.IsNullOrEmpty(lastName))
        {
            user.SetPersonalName(firstName, lastName);
        }

        if (!string.IsNullOrEmpty(phoneNumber))
        {
            user.PhoneNumber = phoneNumber;

        }


        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            return InfraResult<UserDto>.Failed(result.Errors.Select(X => X.Description).ToArray());
        }

        if (roles is not null && roles.Count > 0)
        {
            var roleResult = await _userManager.AddToRolesAsync(user, roles);

            if (!roleResult.Succeeded)
            {
                return InfraResult<UserDto>.Failed(roleResult.Errors.Select(X => X.Description).ToArray());
            }
        }

        return InfraResult<UserDto>.Success(_mapper.Map<UserDto>(user));
    }




    public async Task<InfraResult<UserDto>> UpdateUserDetailsAsync(string userId, EditUserDto model)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
        {
            return InfraResult<UserDto>.Failed(UserServiceErrors.UserNotFound);
        }

        _mapper.Map(model, user);

        if (!string.IsNullOrEmpty(model.NewPassword))
        {
            var changePasswordResult = await _userManager.ChangePasswordAsync(user,
                model.CurrentPassword!, model.NewPassword);

            if (!changePasswordResult.Succeeded)
            {
                return InfraResult<UserDto>.Failed(changePasswordResult.Errors.Select(x => x.Description).ToArray());
            }
        }

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            return InfraResult<UserDto>.Failed(result.Errors.Select(x => x.Description).ToArray());
        }

        return InfraResult<UserDto>.Success(_mapper.Map<UserDto>(user));
    }

    public async Task<InfraResult<string>> DeleteUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
        {
            return InfraResult<string>.Failed(UserServiceErrors.UserNotFound);
        }


        var result = await _userManager.DeleteAsync(user);

        if (!result.Succeeded)
        {
            return InfraResult<string>.Failed(result.Errors.Select(x => x.Description).ToArray());
        }

        return InfraResult<string>.Success(UserServiceResults.UserDeletedSuccessfully);

    }

    public async Task<InfraResult<UserWithRolesDto>> GetUserDetailsAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
        {
            return InfraResult<UserWithRolesDto>.Failed(UserServiceErrors.UserNotFound);
        }

        var roles = await _userManager.GetRolesAsync(user);


        var mapped = _mapper.Map<UserWithRolesDto>(user);
        mapped.Roles.AddRange(roles);

        return InfraResult<UserWithRolesDto>.Success(mapped);

    }



}