using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Sample_CleanArchitecture_CQRS.Infrastructure.Configuration.Settings;
using Sample_CleanArchitecture_CQRS.Infrastructure.Services.Interafaces;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Sample_CleanArchitecture_CQRS.Infrastructure.Generators;

internal sealed class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IOptions<JwtConfig> _jwtSettings;


    public JwtTokenGenerator(IOptions<JwtConfig> jwtSettings)
    {
        _jwtSettings = jwtSettings;
    }


    public string GenerateToken((string id, string username, string email) user)
    {
        var signInKey = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Value.Secret)),
            SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.id),
            new Claim(JwtRegisteredClaimNames.Name, user.username),
            new Claim(JwtRegisteredClaimNames.Email, user.email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var securityKey = new JwtSecurityToken(
            issuer: _jwtSettings.Value.Issuer,
            audience: _jwtSettings.Value.Audience,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.Value.ExpiryMinutes),
            claims: claims,
            signingCredentials: signInKey
        );

        return new JwtSecurityTokenHandler().WriteToken(securityKey);
    }
}