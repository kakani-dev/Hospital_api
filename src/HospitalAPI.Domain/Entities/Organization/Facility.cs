using HospitalAPI.Domain.Entities.Common;

namespace HospitalAPI.Domain.Entities.Organization;

public class Facility : BaseEntity
{
    public Guid BranchId { get; set; }
    public string Name { get; set; } = string.Empty;
    
    // Navigation properties
    public virtual Branch Branch { get; set; } = null!;
    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();
}
