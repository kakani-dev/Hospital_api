using HospitalAPI.Domain.Entities.Common;

namespace HospitalAPI.Domain.Entities.Patient;

public class Patient : BaseEntity
{
    public Guid? BranchId { get; set; }
    public string Mrn { get; set; } = string.Empty; // Medical Record Number
    public string FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; }
    public string? Phone { get; set; }
}
