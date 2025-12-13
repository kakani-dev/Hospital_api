using HospitalAPI.Domain.Entities.Common;

namespace HospitalAPI.Domain.Entities.IAM;

public class Role : BaseEntity
{
    public string Name { get; set; } = string.Empty; // ADMIN, DOCTOR, NURSE, CASHIER
    public string? Description { get; set; }
    
    // Navigation properties
    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}
