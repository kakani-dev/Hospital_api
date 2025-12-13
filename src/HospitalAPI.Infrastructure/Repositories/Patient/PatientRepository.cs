using Microsoft.EntityFrameworkCore;
using HospitalAPI.Domain.Entities.Patient;
using HospitalAPI.Domain.Interfaces.Repositories.Patient;
using HospitalAPI.Infrastructure.Data;

namespace HospitalAPI.Infrastructure.Repositories.Patient;

public class PatientRepository : GenericRepository<Domain.Entities.Patient.Patient>, IPatientRepository
{
    public PatientRepository(HospitalDbContext context) : base(context)
    {
    }

    public async Task<Domain.Entities.Patient.Patient?> GetByMrnAsync(Guid tenantId, string mrn)
    {
        return await _dbSet
            .FirstOrDefaultAsync(p => p.TenantId == tenantId && p.Mrn == mrn);
    }

    public async Task<IEnumerable<Domain.Entities.Patient.Patient>> SearchAsync(Guid tenantId, string searchTerm)
    {
        return await _dbSet
            .Where(p => p.TenantId == tenantId && 
                       (p.FirstName.Contains(searchTerm) || 
                        p.LastName!.Contains(searchTerm) || 
                        p.Mrn.Contains(searchTerm)))
            .ToListAsync();
    }

    public async Task<IEnumerable<Domain.Entities.Patient.Patient>> GetByBranchAsync(Guid tenantId, Guid branchId)
    {
        return await _dbSet
            .Where(p => p.TenantId == tenantId && p.BranchId == branchId)
            .ToListAsync();
    }
}
