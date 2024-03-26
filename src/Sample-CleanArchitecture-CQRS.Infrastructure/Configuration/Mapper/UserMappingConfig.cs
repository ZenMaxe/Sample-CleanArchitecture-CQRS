using Mapster;
using Sample_CleanArchitecture_CQRS.Application.CQRS.v1.Users.Dtos;
using Sample_CleanArchitecture_CQRS.Infrastructure.Models.Identity;

namespace Sample_CleanArchitecture_CQRS.Infrastructure.Configuration.Mapper;

public class UserMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ApplicationUser, UserDto>()
              .ConstructUsing(
                   src => new UserDto(
                       src.UserName!,
                       src.Email!,
                       src.PhoneNumber,
                       src.FirstName,
                       src.LastName));

        config.NewConfig<ApplicationUser, UserWithRolesDto>()
              .ConstructUsing(
                   src => new UserWithRolesDto(
                       src.UserName!,
                       src.Email!,
                       src.PhoneNumber,
                       src.FirstName,
                       src.LastName));

        config.NewConfig<EditUserDto, ApplicationUser>()
              .Map(dest => dest.FirstName, src => !string.IsNullOrEmpty(src.FirstName))
              .Map(dest => dest.LastName, src => !string.IsNullOrEmpty(src.LastName))
              .Map(dest => dest.Email, src => !string.IsNullOrEmpty(src.Email))
              .Map(dest => dest.UserName, src => !string.IsNullOrEmpty(src.UserName))
              .Map(dest => dest.PhoneNumber, src => !string.IsNullOrEmpty(src.PhoneNumber));

    }
}