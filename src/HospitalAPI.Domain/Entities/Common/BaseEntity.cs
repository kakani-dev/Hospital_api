namespace HospitalAPI.Domain.Entities.Common;

public abstract class BaseEntity : ITenantEntity, IAuditableEntity
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public bool IsDeleted { get; set; } = false;
    public Guid CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public Guid? UpdatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
}
