

using Mapster;

using MapsterMapper;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Sample_CleanArchitecture_CQRS.Application.Common.Interfaces;
using Sample_CleanArchitecture_CQRS.Application.Common.Models.Results;
using Sample_CleanArchitecture_CQRS.Application.CQRS.Role.Dtos;
using Sample_CleanArchitecture_CQRS.Infrastructure.Models.Identity;
using Sample_CleanArchitecture_CQRS.Infrastructure.Resources.Services.RoleService;

namespace Sample_CleanArchitecture_CQRS.Infrastructure.Services.Identity;


public class RoleService : IRoleService
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IMapper _mapper;


    public RoleService(RoleManager<ApplicationRole> roleManager,
        IMapper mapper )
    {
        _roleManager = roleManager;
        _mapper = mapper;
    }


    public async Task<InfraResult<RoleDto>> CreateRoleAsync((string roleName, string displayName) role)
    {
        var roleObject = new ApplicationRole(role.roleName, role.displayName);

        var result = await _roleManager.CreateAsync(roleObject);

        if (!result.Succeeded)
        {
            return InfraResult<RoleDto>.Failed(result.Errors.Select(x => x.Description).ToArray());
        }

        return InfraResult<RoleDto>.Success(_mapper.Map<RoleDto>(roleObject));
    }


    public async Task<InfraResult<string>> DeleteRoleAsync(string roleId)
    {
        var role = await _roleManager.FindByIdAsync(roleId);

        if (role is null)
        {
            return InfraResult<string>.Failed(RoleServiceErrors.RoleNotFound);
        }

        if (role.IsDefault)
        {
            return InfraResult<string>.Failed(RoleServiceErrors.CannotDeleteDefaultRole);
        }

        var result = await _roleManager.DeleteAsync(role);

        if (!result.Succeeded)
        {
            return InfraResult<string>.Failed(result.Errors.Select(x => x.Description).ToArray());
        }

        return InfraResult<string>.Success(result: RoleServiceResults.RoleDeletedSuccessfully);
    }

    public async Task<InfraResult<List<RoleDto>>> GetDefaultRolesAsync()
    {
        var roles = await _roleManager.Roles.Where(x => x.IsDefault)
                                      .ToListAsync();

        List<RoleDto> roleDtos = new();

        foreach (var role in roles)
        {
            roleDtos.Add(_mapper.Map<RoleDto>(role));
        }

        return InfraResult<List<RoleDto>>.Success(roleDtos);
    }


    public async Task<InfraResult<RoleDto>> UpdateRole(string id, string? roleName, string? displayName)
    {
        var role = await _roleManager.FindByIdAsync(id);

        if (role is null)
        {
            return InfraResult<RoleDto>.Failed(RoleServiceErrors.RoleNotFound);
        }

        if (!string.IsNullOrEmpty(roleName))
        {
            role.Name = roleName;

        }

        if (!string.IsNullOrEmpty(displayName))
        {
            role.DisplayName = displayName;

        }

        var result = await _roleManager.UpdateAsync(role);

        if (!result.Succeeded)
        {
            return InfraResult<RoleDto>.Failed(result.Errors.Select(x => x.Description).ToArray());
        }

        return InfraResult<RoleDto>.Success(_mapper.Map<RoleDto>(role));
    }

    public async Task<InfraResult<RoleDto>> GetRoleDetailsAsync(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);

        if (role is null)
        {
            return InfraResult<RoleDto>.Failed(RoleServiceErrors.RoleNotFound);
        }


        return InfraResult<RoleDto>.Success(_mapper.Map<RoleDto>(role));
    }


    public async Task<InfraResult<List<RoleDto>>> GetRolesAsync()
    {

        var roles = await _roleManager.Roles
                                      .ProjectToType<RoleDto>()
                                      .ToListAsync();

        return InfraResult<List<RoleDto>>.Success(roles);
    }
}