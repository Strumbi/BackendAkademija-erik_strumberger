using BackendAkademija.Application.Models;

namespace BackendAkademija.Application.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateAccessToken(int userId, string username);
    RefreshToken GenerateRefreshToken(int userId, string username, DateTime? originalLoginAt = null);
}