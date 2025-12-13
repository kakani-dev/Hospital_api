using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HospitalAPI.Domain.Entities.IAM;

namespace HospitalAPI.Infrastructure.Data.Configurations.IAM;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("roles", "iam");
        
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).HasColumnName("id").HasDefaultValueSql("gen_random_uuid()");
        
        builder.Property(r => r.TenantId).HasColumnName("tenant_id").IsRequired();
        builder.Property(r => r.Name).HasColumnName("name").IsRequired();
        builder.Property(r => r.Description).HasColumnName("description");
        builder.Property(r => r.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false);
        builder.Property(r => r.CreatedBy).HasColumnName("created_by").IsRequired();
        builder.Property(r => r.CreatedOn).HasColumnName("created_on").HasDefaultValueSql("now()");
        builder.Property(r => r.UpdatedBy).HasColumnName("updated_by");
        builder.Property(r => r.UpdatedOn).HasColumnName("updated_on");

        builder.HasIndex(r => new { r.TenantId, r.Name })
            .IsUnique()
            .HasDatabaseName("ux_iam_roles_name");
    }
}
