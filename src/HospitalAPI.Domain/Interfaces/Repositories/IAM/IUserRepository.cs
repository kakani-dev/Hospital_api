using HospitalAPI.Domain.Entities.IAM;
using HospitalAPI.Domain.Interfaces.Repositories;

namespace HospitalAPI.Domain.Interfaces.Repositories.IAM;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetByEmailAsync(Guid tenantId, string email);
    Task<IEnumerable<User>> GetByTenantAsync(Guid tenantId);
    Task<User?> GetWithRolesAsync(Guid id);
}
