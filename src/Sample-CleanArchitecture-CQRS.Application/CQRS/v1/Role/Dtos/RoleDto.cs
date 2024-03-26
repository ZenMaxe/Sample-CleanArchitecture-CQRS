namespace Sample_CleanArchitecture_CQRS.Application.CQRS.v1.Role.Dtos;

public record RoleDto
{
    public Guid Id { get; }

    public string Name { get; }

    public string DisplayName { get; }


    public RoleDto(Guid id, string name,
                   string displayName)
    {
        Id = id;
        Name = name;
        DisplayName = displayName;
    }
}