using Microsoft.AspNetCore.Identity;

using Sample_CleanArchitecture_CQRS.Application.Common.Interfaces;
using Sample_CleanArchitecture_CQRS.Application.Common.Models.Results;
using Sample_CleanArchitecture_CQRS.Application.CQRS.v1.Authentication.Dtos;
using Sample_CleanArchitecture_CQRS.Infrastructure.Models.Identity;
using Sample_CleanArchitecture_CQRS.Infrastructure.Resources.Services.AuthenticationService;
using Sample_CleanArchitecture_CQRS.Infrastructure.Resources.Services.UserService;

using IJwtTokenGenerator = Sample_CleanArchitecture_CQRS.Infrastructure.Services.Interafaces.IJwtTokenGenerator;

namespace Sample_CleanArchitecture_CQRS.Infrastructure.Services.Identity;



public sealed class AuthenticationService : IAuthenticationService
{

    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthenticationService(SignInManager<ApplicationUser> signInManager,
                                 UserManager<ApplicationUser> userManager,
                                 IJwtTokenGenerator jwtTokenGenerator)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<InfraResult<TokenDto>> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
        {
            return InfraResult<TokenDto>.Failed(UserServiceErrors.UserNotFound);
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

        if (!result.Succeeded)
        {
            return InfraResult<TokenDto>.Failed(errorSelector(result));
        }

        var token = _jwtTokenGenerator.GenerateToken((user.Id.ToString(), user.UserName!, user.Email!));

        // No Refresh Token For Now!

        return InfraResult<TokenDto>.Success(new TokenDto(token));
    }



    // No LogOut For JWT, Beacuse it's sample project not a norma project, i need to implement UserTokenBlackList + Refresh Token
    // For Logout system and refresh token functionallity

    private string errorSelector(SignInResult result)
    {
        string error;
        if (result.IsLockedOut)
        {
            error = AuthenticationServiceErrors.AccountIsLocked;
        }
        else if (result.IsNotAllowed)
        {
            error = AuthenticationServiceErrors.AccountNotAllowed;
        }
        else
        {
            error = AuthenticationServiceErrors.WrongUsernameOrPassword;
        }

        return error;
    }



}