using BackendAkademija.Application.Interfaces;
using BackendAkademija.Application.Models;
using BackendAkademija.Infrastructure.Persistence;
using BackendAkademija.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendAkademija.Infrastructure.Auth;

public class RefreshTokenStore(AppDbContext context) : IRefreshTokenStore
{

    public void Save(RefreshToken refreshToken)
    {
        var entity = new RefreshTokenEntity
        {
            Token = refreshToken.Token,
            UserId = refreshToken.UserId,
            Username = refreshToken.UserName,
            ExpiresAt = refreshToken.ExpiresAt,
            OriginalLoginAt = refreshToken.OriginalLoginAt,
            IsRevoked = false,
            CreatedAt = DateTime.UtcNow
        };

        context.RefreshTokens.Add(entity);
        context.SaveChanges();
    }

    public RefreshToken? Get(string token)
    {
        var entity = context.RefreshTokens
            .AsNoTracking()
            .FirstOrDefault(t => t.Token == token);

        if (entity is null)
            return null;

        return new RefreshToken
        {
            Token = entity.Token,
            UserId = entity.UserId,
            UserName = entity.Username,
            ExpiresAt = entity.ExpiresAt,
            OriginalLoginAt = entity.OriginalLoginAt,
            IsRevoked = entity.IsRevoked
        };
    }

    public void Revoke(string token)
    {
        var entity = context.RefreshTokens
            .FirstOrDefault(t => t.Token == token);

        if (entity is null)
            return;

        entity.IsRevoked = true;
        context.SaveChanges();
    }
}