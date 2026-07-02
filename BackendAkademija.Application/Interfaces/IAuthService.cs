using BackendAkademija.Application.Dto.AuthDto;

namespace BackendAkademija.Application.Interfaces;


public interface IAuthService
{
    Task<LoginResult> LoginAsync(string username, string password, CancellationToken cancellationToken);
    Task<RefreshResult> RefreshAsync(string refreshToken, CancellationToken cancellationToken);

}