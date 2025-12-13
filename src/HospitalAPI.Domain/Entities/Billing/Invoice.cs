using HospitalAPI.Domain.Entities.Common;

namespace HospitalAPI.Domain.Entities.Billing;

public class Invoice : BaseEntity
{
    public Guid BranchId { get; set; }
    public Guid PatientId { get; set; }
    public decimal TotalAmount { get; set; } = 0;
    public string Status { get; set; } = "draft"; // draft, finalized, paid, cancelled
    
    // Navigation properties
    public virtual ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
