using BADBIR.Shared.Enums;

namespace BADBIR.Api.Data.Entities;

/// <summary>
/// Records a patient's informed consent submission.
/// One row per consent event (initial + any future re-consents).
/// </summary>
public class ConsentRecord
{
    public int ConsentId { get; set; }

    /// <summary>FK to AspNetUsers.Id.</summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>Version of the consent form accepted (from AppConstants.ConsentFormVersion).</summary>
    public string ConsentFormVersion { get; set; } = string.Empty;

    /// <summary>UTC timestamp when consent was submitted.</summary>
    public DateTime ConsentTimestamp { get; set; }

    /// <summary>IP address of the request (may be null if unavailable).</summary>
    public string? IPAddress { get; set; }

    /// <summary>Browser user-agent string (used to determine device type).</summary>
    public string? UserAgent { get; set; }

    /// <summary>Electronic (typed name + checkbox) or Drawn (canvas).</summary>
    public SignatureType SignatureType { get; set; }

    /// <summary>
    /// For electronic signatures: typed full name.
    /// For drawn signatures: base64-encoded PNG image.
    /// </summary>
    public string SignatureData { get; set; } = string.Empty;

    /// <summary>Navigation to the owning user.</summary>
    public ApplicationUser? User { get; set; }
}
