using BackendAkademija.Application.Interfaces;

namespace BackendAkademija.Infrastructure;

public class DummyJsonAuthService : IAuthService
{
    public Task<LoginResult> LoginAsync(string username, string password, CancellationToken cancellationToken)
    {
        // TODO: Dovristi
        throw new NotImplementedException();
    }
}