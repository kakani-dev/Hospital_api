using HospitalAPI.Domain.Entities.Common;

namespace HospitalAPI.Domain.Entities.Organization;

public class Department : BaseEntity
{
    public Guid FacilityId { get; set; }
    public string Name { get; set; } = string.Empty;
    
    // Navigation properties
    public virtual Facility Facility { get; set; } = null!;
}
