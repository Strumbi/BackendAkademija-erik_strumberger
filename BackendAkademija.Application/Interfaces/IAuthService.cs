namespace BackendAkademija.Application.Interfaces;

public record LoginResult(bool Succeeded, string? Token, string? Error);

public interface IAuthService
{
    Task<LoginResult> LoginAsync(string username, string password, CancellationToken cancellationToken);
}