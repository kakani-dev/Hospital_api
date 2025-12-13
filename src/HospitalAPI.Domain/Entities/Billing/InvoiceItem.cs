namespace HospitalAPI.Domain.Entities.Billing;

public class InvoiceItem
{
    public Guid Id { get; set; }
    public Guid InvoiceId { get; set; }
    public Guid ServiceId { get; set; }
    public int Quantity { get; set; } = 1;
    public decimal Price { get; set; }
    public decimal Amount { get; set; }
    
    // Navigation properties
    public virtual Invoice Invoice { get; set; } = null!;
    public virtual ServiceCatalog Service { get; set; } = null!;
}
