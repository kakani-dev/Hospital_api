using HospitalAPI.Domain.Entities.IAM;
using HospitalAPI.Domain.Interfaces.Repositories;

namespace HospitalAPI.Domain.Interfaces.Repositories.IAM;

public interface IRoleRepository : IGenericRepository<Role>
{
    Task<Role?> GetByNameAsync(Guid tenantId, string name);
    Task<Role?> GetWithPermissionsAsync(Guid id);
    Task<IEnumerable<Role>> GetByTenantAsync(Guid tenantId);
}
