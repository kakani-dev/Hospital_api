using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HospitalAPI.Domain.Entities.Billing;

namespace HospitalAPI.Infrastructure.Data.Configurations.Billing;

public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.ToTable("invoices", "billing");
        
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id).HasColumnName("id").HasDefaultValueSql("gen_random_uuid()");
        
        builder.Property(i => i.TenantId).HasColumnName("tenant_id").IsRequired();
        builder.Property(i => i.BranchId).HasColumnName("branch_id").IsRequired();
        builder.Property(i => i.PatientId).HasColumnName("patient_id").IsRequired();
        builder.Property(i => i.TotalAmount).HasColumnName("total_amount").HasColumnType("numeric(12,2)").HasDefaultValue(0);
        builder.Property(i => i.Status).HasColumnName("status").HasDefaultValue("draft");
        builder.Property(i => i.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false);
        builder.Property(i => i.CreatedBy).HasColumnName("created_by").IsRequired();
        builder.Property(i => i.CreatedOn).HasColumnName("created_on").HasDefaultValueSql("now()");
        builder.Property(i => i.UpdatedBy).HasColumnName("updated_by");
        builder.Property(i => i.UpdatedOn).HasColumnName("updated_on");

        builder.HasIndex(i => new { i.TenantId, i.PatientId })
            .HasDatabaseName("idx_billing_invoice_patient");

        builder.HasMany(i => i.InvoiceItems)
            .WithOne(ii => ii.Invoice)
            .HasForeignKey(ii => ii.InvoiceId);

        builder.HasMany(i => i.Payments)
            .WithOne(p => p.Invoice)
            .HasForeignKey(p => p.InvoiceId);
    }
}
