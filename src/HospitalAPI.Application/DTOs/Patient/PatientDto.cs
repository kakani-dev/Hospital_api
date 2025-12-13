namespace HospitalAPI.Application.DTOs.Patient;

public class PatientDto
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public Guid? BranchId { get; set; }
    public string Mrn { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; }
    public string? Phone { get; set; }
    public DateTime CreatedOn { get; set; }
}
