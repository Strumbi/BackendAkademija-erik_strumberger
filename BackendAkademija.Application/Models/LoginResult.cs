namespace BackendAkademija.Application.Dto.AuthDto;

public record LoginResult(
    bool Success,
    string? AccessToken,
    string? RefreshToken,
    string? Error);