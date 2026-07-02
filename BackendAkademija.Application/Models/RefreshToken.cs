namespace BackendAkademija.Application.Models;

public class RefreshToken
{
    public string Token { get; set; } = string.Empty;
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public DateTime OriginalLoginAt { get; set; }
    public bool IsRevoked { get; set; }
}