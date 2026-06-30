namespace BackendAkademija.Application.Helper;

public static class StringHelper
{
    public static string Truncate(string? value, int maxLength) => 
    string.IsNullOrEmpty(value) || value.Length <= maxLength 
        ? value ?? string.Empty 
        : value[..maxLength] + "...";
}