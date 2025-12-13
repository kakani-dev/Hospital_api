using HospitalAPI.Domain.Entities.Common;

namespace HospitalAPI.Domain.Entities.Billing;

public class Payment : BaseEntity
{
    public Guid BranchId { get; set; }
    public Guid InvoiceId { get; set; }
    public decimal Amount { get; set; }
    public string Method { get; set; } = string.Empty; // cash, card, upi, insurance
    
    // Navigation properties
    public virtual Invoice Invoice { get; set; } = null!;
}
