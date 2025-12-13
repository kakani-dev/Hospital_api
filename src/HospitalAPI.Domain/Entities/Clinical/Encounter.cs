using HospitalAPI.Domain.Entities.Common;

namespace HospitalAPI.Domain.Entities.Clinical;

public class Encounter : BaseEntity
{
    public Guid BranchId { get; set; }
    public Guid PatientId { get; set; }
    public string EncounterType { get; set; } = string.Empty; // OPD, IPD, Emergency
    public string Status { get; set; } = "open"; // open, closed
}
