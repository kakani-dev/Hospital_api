using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HospitalAPI.Domain.Entities.Patient;

namespace HospitalAPI.Infrastructure.Data.Configurations.Patient;

public class PatientConfiguration : IEntityTypeConfiguration<Domain.Entities.Patient.Patient>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Patient.Patient> builder)
    {
        builder.ToTable("patients", "patient");
        
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("id").HasDefaultValueSql("gen_random_uuid()");
        
        builder.Property(p => p.TenantId).HasColumnName("tenant_id").IsRequired();
        builder.Property(p => p.BranchId).HasColumnName("branch_id");
        builder.Property(p => p.Mrn).HasColumnName("mrn").IsRequired();
        builder.Property(p => p.FirstName).HasColumnName("first_name").IsRequired();
        builder.Property(p => p.LastName).HasColumnName("last_name");
        builder.Property(p => p.Phone).HasColumnName("phone");
        builder.Property(p => p.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false);
        builder.Property(p => p.CreatedBy).HasColumnName("created_by").IsRequired();
        builder.Property(p => p.CreatedOn).HasColumnName("created_on").HasDefaultValueSql("now()");
        builder.Property(p => p.UpdatedBy).HasColumnName("updated_by");
        builder.Property(p => p.UpdatedOn).HasColumnName("updated_on");

        builder.HasIndex(p => new { p.TenantId, p.Mrn })
            .IsUnique()
            .HasDatabaseName("ux_patient_mrn");
    }
}
