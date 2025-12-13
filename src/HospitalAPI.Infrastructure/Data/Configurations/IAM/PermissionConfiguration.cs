using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HospitalAPI.Domain.Entities.IAM;

namespace HospitalAPI.Infrastructure.Data.Configurations.IAM;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("permissions", "iam");
        
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("id").HasDefaultValueSql("gen_random_uuid()");
        
        builder.Property(p => p.Code).HasColumnName("code").IsRequired();
        builder.Property(p => p.Description).HasColumnName("description");

        builder.HasIndex(p => p.Code)
            .IsUnique();
    }
}
