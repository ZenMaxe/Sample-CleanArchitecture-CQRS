namespace Sample_CleanArchitecture_CQRS.Infrastructure.Services.Interafaces;

public interface IJwtTokenGenerator
{
    string GenerateToken((string id, string username, string email) user);
}