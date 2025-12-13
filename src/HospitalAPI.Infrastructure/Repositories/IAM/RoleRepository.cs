using Microsoft.EntityFrameworkCore;
using HospitalAPI.Domain.Entities.IAM;
using HospitalAPI.Domain.Interfaces.Repositories.IAM;
using HospitalAPI.Infrastructure.Data;

namespace HospitalAPI.Infrastructure.Repositories.IAM;

public class RoleRepository : GenericRepository<Role>, IRoleRepository
{
    public RoleRepository(HospitalDbContext context) : base(context)
    {
    }

    public async Task<Role?> GetByNameAsync(Guid tenantId, string name)
    {
        return await _dbSet
            .FirstOrDefaultAsync(r => r.TenantId == tenantId && r.Name == name);
    }

    public async Task<Role?> GetWithPermissionsAsync(Guid id)
    {
        return await _dbSet
            .Include(r => r.RolePermissions)
                .ThenInclude(rp => rp.Permission)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IEnumerable<Role>> GetByTenantAsync(Guid tenantId)
    {
        return await _dbSet
            .Where(r => r.TenantId == tenantId)
            .ToListAsync();
    }
}
