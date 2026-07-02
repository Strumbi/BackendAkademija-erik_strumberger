using BackendAkademija.Application.Models;

namespace BackendAkademija.Application.Interfaces;

public interface IRefreshTokenStore
{
    void Save(RefreshToken refreshToken);
    RefreshToken? Get(string token);
    void Revoke(string token);
}