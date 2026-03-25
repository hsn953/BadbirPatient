namespace BADBIR.Shared.DTOs;

/// <summary>
/// Data Transfer Object for patient login request.
/// </summary>
public class LoginRequestDto
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

/// <summary>
/// Data Transfer Object for a successful login response.
/// </summary>
public class LoginResponseDto
{
    public string Token { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public DateTime Expiry { get; set; }
}
