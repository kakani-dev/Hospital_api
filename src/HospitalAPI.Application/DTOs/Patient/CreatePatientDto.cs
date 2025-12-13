namespace HospitalAPI.Application.DTOs.Patient;

public class CreatePatientDto
{
    public Guid? BranchId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; }
    public string? Phone { get; set; }
}
