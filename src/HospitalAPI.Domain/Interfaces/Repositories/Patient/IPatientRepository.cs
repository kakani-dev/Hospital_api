using HospitalAPI.Domain.Entities.Patient;
using HospitalAPI.Domain.Interfaces.Repositories;

namespace HospitalAPI.Domain.Interfaces.Repositories.Patient;

public interface IPatientRepository : IGenericRepository<Entities.Patient.Patient>
{
    Task<Entities.Patient.Patient?> GetByMrnAsync(Guid tenantId, string mrn);
    Task<IEnumerable<Entities.Patient.Patient>> SearchAsync(Guid tenantId, string searchTerm);
    Task<IEnumerable<Entities.Patient.Patient>> GetByBranchAsync(Guid tenantId, Guid branchId);
}
