namespace BADBIR.Api.Data.Entities;

/// <summary>
/// Maps to [dbo].[bbPatient] — the main patient demographic record.
/// PII fields (title, forenames, surname, countryresidence, phrn, pnhs) are
/// AES-encrypted using PasswordDeriveBytes (see EncryptionService).
/// This is a shared table owned by the Clinician System; the Patient App reads
/// and updates specific columns only (Portal_IsRegistered, Portal_DateRegistered,
/// statusid).
/// </summary>
public class BbPatient
{
    public int Patientid { get; set; }

    public int? Firststudyno { get; set; }
    public int? Studyidlastfive { get; set; }
    public DateTime? Dateconsented { get; set; }
    public DateTime? Consentformreceived { get; set; }

    /// <summary>CHI / Health &amp; Care number (Scotland) — AES encrypted.</summary>
    public string? Phrn { get; set; }

    /// <summary>NHS number (England/Wales) — AES encrypted.</summary>
    public string? Pnhs { get; set; }

    public int? Genderid { get; set; }
    public DateTime? Dateofbirth { get; set; }

    /// <summary>Title — AES encrypted.</summary>
    public string? Title { get; set; }

    /// <summary>Surname — AES encrypted.</summary>
    public string? Surname { get; set; }

    /// <summary>Forenames — AES encrypted.</summary>
    public string? Forenames { get; set; }

    public string? Address1 { get; set; }
    public string? Address2 { get; set; }
    public string? Address3 { get; set; }
    public string? AddressTown { get; set; }
    public string? AddressCounty { get; set; }
    public string? AddressPostcode { get; set; }
    public string? Phone { get; set; }
    public string? Emailaddress { get; set; }

    /// <summary>Country of residence — AES encrypted.</summary>
    public string? Countryresidence { get; set; }

    /// <summary>FK to bbPatientStatuslkp.pstatusid. 1=Current, 6=Registered awaiting consent, 7=Awaiting drug details.</summary>
    public int? Statusid { get; set; }

    public bool? LosttoFUP { get; set; }
    public int? Registrationcohortid { get; set; }

    // ── Audit columns ────────────────────────────────────────────────────────
    public int Createdbyid { get; set; }
    public string Createdbyname { get; set; } = string.Empty;
    public DateTime Createddate { get; set; }
    public int Lastupdatedbyid { get; set; }
    public string Lastupdatedbyname { get; set; } = string.Empty;
    public DateTime Lastupdateddate { get; set; }

    public int? Tempstudyno { get; set; }
    public float? Consentversion { get; set; }
    public int Statusdetailid { get; set; }
    public string? Consentfileaddress { get; set; }
    public DateOnly? Deathdate { get; set; }
    public DateOnly? AltDeathDate { get; set; }
    public int? AltStudyID234Digits { get; set; }
    public int? ConsentedByBadbirUserID { get; set; }
    public int? RemoteConsent { get; set; }

    /// <summary>Set to 1 when the patient registers via the Patient Portal.</summary>
    public int PortalIsRegistered { get; set; }

    /// <summary>Timestamp when the patient registered via the Patient Portal.</summary>
    public DateTime? PortalDateRegistered { get; set; }

    // ── Navigation ────────────────────────────────────────────────────────────
    public ApplicationUser? PortalUser { get; set; }
    public ICollection<BbPatientCohortHistory> CohortHistories { get; set; } = [];
}
