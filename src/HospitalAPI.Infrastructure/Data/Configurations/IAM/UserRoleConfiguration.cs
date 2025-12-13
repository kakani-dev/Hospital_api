using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HospitalAPI.Domain.Entities.IAM;

namespace HospitalAPI.Infrastructure.Data.Configurations.IAM;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("user_roles", "iam");
        
        builder.HasKey(ur => new { ur.UserId, ur.RoleId });
        
        builder.Property(ur => ur.UserId).HasColumnName("user_id").IsRequired();
        builder.Property(ur => ur.RoleId).HasColumnName("role_id").IsRequired();
        builder.Property(ur => ur.TenantId).HasColumnName("tenant_id").IsRequired();
        builder.Property(ur => ur.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false);
        builder.Property(ur => ur.CreatedBy).HasColumnName("created_by").IsRequired();
        builder.Property(ur => ur.CreatedOn).HasColumnName("created_on").HasDefaultValueSql("now()");
        builder.Property(ur => ur.UpdatedBy).HasColumnName("updated_by");
        builder.Property(ur => ur.UpdatedOn).HasColumnName("updated_on");

        builder.HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId);

        builder.HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId);
    }
}
