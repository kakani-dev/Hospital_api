using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HospitalAPI.Domain.Entities.IAM;

namespace HospitalAPI.Infrastructure.Data.Configurations.IAM;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users", "iam");
        
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnName("id");
        
        builder.Property(u => u.TenantId).HasColumnName("tenant_id").IsRequired();
        builder.Property(u => u.Email).HasColumnName("email").IsRequired();
        builder.Property(u => u.FullName).HasColumnName("full_name").IsRequired();
        builder.Property(u => u.Status).HasColumnName("status").HasDefaultValue("active");
        builder.Property(u => u.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false);
        builder.Property(u => u.CreatedBy).HasColumnName("created_by").IsRequired();
        builder.Property(u => u.CreatedOn).HasColumnName("created_on").HasDefaultValueSql("now()");
        builder.Property(u => u.UpdatedBy).HasColumnName("updated_by");
        builder.Property(u => u.UpdatedOn).HasColumnName("updated_on");

        builder.HasIndex(u => new { u.TenantId, u.Email })
            .IsUnique()
            .HasDatabaseName("ux_iam_users_email");
    }
}
