using Microsoft.AspNetCore.Identity;

namespace Sample_CleanArchitecture_CQRS.Infrastructure.Models.Identity;

public class ApplicationRole : IdentityRole<Guid>
{

    public string DisplayName { get; set; }


    /// <summary>
    /// Determine Role Is Default Or Not
    /// </summary>
    public bool IsDefault { get; init; }


    public ApplicationRole(string roleName, string displayName)
    {
        base.Name = roleName;
        DisplayName = displayName;
        IsDefault = false;
    }
    public ApplicationRole()
    {
        // Parameterless constructor
    }

    public ApplicationRole(string roleName, string displayName, bool isDefault)
    {
        base.Name = roleName;
        DisplayName = displayName;
        IsDefault = isDefault;
    }
}