namespace BackendAkademija.Application.Dto.AuthDto;

public record RefreshResult(
    bool Success,
    string? AccessToken,
    string? RefreshToken,
    string? Error);