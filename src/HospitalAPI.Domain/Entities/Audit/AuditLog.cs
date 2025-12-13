using HospitalAPI.Domain.Entities.Common;

namespace HospitalAPI.Domain.Entities.Audit;

public class AuditLog : ITenantEntity, IAuditableEntity
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public Guid ActorUserId { get; set; }
    public string Action { get; set; } = string.Empty; // CREATE, UPDATE, DELETE
    public string Entity { get; set; } = string.Empty; // Table/Entity name
    public Guid? EntityId { get; set; }
    public string? BeforeData { get; set; } // JSON
    public string? AfterData { get; set; } // JSON
    public Guid CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public Guid? UpdatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
}
