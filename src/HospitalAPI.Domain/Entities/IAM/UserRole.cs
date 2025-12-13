using HospitalAPI.Domain.Entities.Common;

namespace HospitalAPI.Domain.Entities.IAM;

public class UserRole : ITenantEntity, IAuditableEntity
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
    public Guid TenantId { get; set; }
    public bool IsDeleted { get; set; } = false;
    public Guid CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public Guid? UpdatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    
    // Navigation properties
    public virtual User User { get; set; } = null!;
    public virtual Role Role { get; set; } = null!;
}
