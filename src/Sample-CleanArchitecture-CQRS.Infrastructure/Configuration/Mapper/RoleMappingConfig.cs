using Mapster;

using Sample_CleanArchitecture_CQRS.Application.CQRS.Role.Dtos;
using Sample_CleanArchitecture_CQRS.Infrastructure.Models.Identity;

namespace Sample_CleanArchitecture_CQRS.Infrastructure.Configuration.Mapper;

public class RoleMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {

        config.NewConfig<ApplicationRole, RoleDto>()
              .ConstructUsing(src => new RoleDto(src.Id, src.Name!, src.DisplayName));


    }
}