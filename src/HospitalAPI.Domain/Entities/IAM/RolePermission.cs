using HospitalAPI.Domain.Entities.Common;

namespace HospitalAPI.Domain.Entities.IAM;

public class RolePermission : ITenantEntity, IAuditableEntity
{
    public Guid RoleId { get; set; }
    public Guid PermissionId { get; set; }
    public Guid TenantId { get; set; }
    public bool IsDeleted { get; set; } = false;
    public Guid CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public Guid? UpdatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    
    // Navigation properties
    public virtual Role Role { get; set; } = null!;
    public virtual Permission Permission { get; set; } = null!;
}
