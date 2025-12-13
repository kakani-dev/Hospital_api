using HospitalAPI.Domain.Entities.Common;

namespace HospitalAPI.Domain.Entities.Organization;

public class Tenant : BaseEntity
{
    public string TenantCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Status { get; set; } = "active";
    
    // Navigation properties
    public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();
}
