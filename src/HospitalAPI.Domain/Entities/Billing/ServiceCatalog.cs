using HospitalAPI.Domain.Entities.Common;

namespace HospitalAPI.Domain.Entities.Billing;

public class ServiceCatalog : ITenantEntity, IAuditableEntity
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Category { get; set; }
    public decimal? Price { get; set; }
    public bool IsDeleted { get; set; } = false;
    public Guid CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public Guid? UpdatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
}
