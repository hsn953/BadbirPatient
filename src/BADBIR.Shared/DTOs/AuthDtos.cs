namespace BADBIR.Shared.DTOs;

/// <summary>Patient registration request.</summary>
public class RegisterRequestDto
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string NhsNumber { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
}

/// <summary>Login request payload.</summary>
public class LoginRequestDto
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

/// <summary>Successful authentication response carrying a JWT.</summary>
public class LoginResponseDto
{
    public string AccessToken { get; set; } = string.Empty;
    public DateTime Expiry { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public IEnumerable<string> Roles { get; set; } = [];
}
