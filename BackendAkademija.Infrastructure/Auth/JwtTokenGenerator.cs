using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BackendAkademija.Application.Interfaces;
using BackendAkademija.Application.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace BackendAkademija.Infrastructure.Auth;

public class JwtTokenGenerator(IConfiguration configuration) : IJwtTokenGenerator
{
    public string GenerateAccessToken(int userId, string username)
    {
        var jwtSection = configuration.GetSection("Jwt");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection["Secret"] ?? string.Empty));
        var credientals = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new Claim[]
        {
            new (JwtRegisteredClaimNames.Sub, userId.ToString()),
            new (JwtRegisteredClaimNames.UniqueName, username),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: jwtSection["Issuer"],
            audience: jwtSection["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(double.Parse(jwtSection["ExpiryMinutes"] ?? "60")),
            signingCredentials: credientals
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    

    public RefreshToken GenerateRefreshToken(int userId, string username, DateTime? originalLoginAt = null)
    {
        var jwtSection = configuration.GetSection("Jwt");
        var tokenBytes = RandomNumberGenerator.GetBytes(64);
        var token = Convert.ToBase64String(tokenBytes);

        var loginAt = originalLoginAt ?? DateTime.UtcNow;

        return new RefreshToken
        {
            Token = token,
            UserId = userId,
            UserName = username,
            OriginalLoginAt = loginAt,
            ExpiresAt = loginAt.AddDays(double.Parse(jwtSection["RefreshTokenExpiryDays"] ?? "7")),
            IsRevoked = false
        };
    }
}