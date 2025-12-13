namespace HospitalAPI.Domain.Entities.IAM;

public class Permission
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty; // patient.read, billing.write
    public string? Description { get; set; }
    
    // Navigation properties
    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}
