using HospitalAPI.Domain.Entities.Billing;
using HospitalAPI.Domain.Interfaces.Repositories;

namespace HospitalAPI.Domain.Interfaces.Repositories.Billing;

public interface IInvoiceRepository : IGenericRepository<Invoice>
{
    Task<Invoice?> GetWithItemsAsync(Guid id);
    Task<IEnumerable<Invoice>> GetByPatientAsync(Guid tenantId, Guid patientId);
    Task<IEnumerable<Invoice>> GetByStatusAsync(Guid tenantId, string status);
}
