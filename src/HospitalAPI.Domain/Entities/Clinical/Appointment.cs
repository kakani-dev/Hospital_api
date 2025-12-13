using HospitalAPI.Domain.Entities.Common;

namespace HospitalAPI.Domain.Entities.Clinical;

public class Appointment : BaseEntity
{
    public Guid BranchId { get; set; }
    public Guid PatientId { get; set; }
    public Guid StaffId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Status { get; set; } = "booked"; // booked, confirmed, cancelled, completed
}
