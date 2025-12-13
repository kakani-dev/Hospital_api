using Microsoft.EntityFrameworkCore;
using HospitalAPI.Domain.Entities.Billing;
using HospitalAPI.Domain.Interfaces.Repositories.Billing;
using HospitalAPI.Infrastructure.Data;

namespace HospitalAPI.Infrastructure.Repositories.Billing;

public class InvoiceRepository : GenericRepository<Invoice>, IInvoiceRepository
{
    public InvoiceRepository(HospitalDbContext context) : base(context)
    {
    }

    public async Task<Invoice?> GetWithItemsAsync(Guid id)
    {
        return await _dbSet
            .Include(i => i.InvoiceItems)
                .ThenInclude(ii => ii.Service)
            .Include(i => i.Payments)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<IEnumerable<Invoice>> GetByPatientAsync(Guid tenantId, Guid patientId)
    {
        return await _dbSet
            .Where(i => i.TenantId == tenantId && i.PatientId == patientId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Invoice>> GetByStatusAsync(Guid tenantId, string status)
    {
        return await _dbSet
            .Where(i => i.TenantId == tenantId && i.Status == status)
            .ToListAsync();
    }
}
