using HospitalAPI.Domain.Entities.Common;

namespace HospitalAPI.Domain.Entities.Organization;

public class Branch : BaseEntity
{
    public string BranchCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? City { get; set; }
    public string Status { get; set; } = "active";
    
    // Navigation properties
    public virtual Tenant Tenant { get; set; } = null!;
    public virtual ICollection<Facility> Facilities { get; set; } = new List<Facility>();
}
