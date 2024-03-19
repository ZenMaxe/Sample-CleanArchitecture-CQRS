using Sample_CleanArchitecture_CQRS.Application.Common.Models.Results;
using Sample_CleanArchitecture_CQRS.Application.CQRS.Role.Dtos;

namespace Sample_CleanArchitecture_CQRS.Application.Common.Interfaces;

public interface IRoleService
{
    Task<InfraResult<RoleDto>> CreateRoleAsync((string roleName, string displayName) role);
    Task<InfraResult<string>> DeleteRoleAsync(string roleId);
    Task<InfraResult<List<RoleDto>>> GetDefaultRolesAsync();
    Task<InfraResult<RoleDto>> UpdateRole(string id, string? roleName, string? displayName);

    Task<InfraResult<RoleDto>> GetRoleDetailsAsync(string id);
    Task<InfraResult<List<RoleDto>>> GetRolesAsync();

}