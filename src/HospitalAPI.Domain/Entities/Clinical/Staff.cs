using HospitalAPI.Domain.Entities.Common;

namespace HospitalAPI.Domain.Entities.Clinical;

public class Staff : BaseEntity
{
    public Guid BranchId { get; set; }
    public Guid? UserId { get; set; } // Links to iam.users
    public string StaffType { get; set; } = string.Empty; // DOCTOR, NURSE, etc.
}
