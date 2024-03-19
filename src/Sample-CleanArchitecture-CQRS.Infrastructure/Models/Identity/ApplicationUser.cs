using Microsoft.AspNetCore.Identity;

namespace Sample_CleanArchitecture_CQRS.Infrastructure.Models.Identity;



public class ApplicationUser : IdentityUser<Guid>
{
    public string? FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;


    public void SetPersonalName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;

    }
}