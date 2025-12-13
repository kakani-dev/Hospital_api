using Microsoft.EntityFrameworkCore;
using HospitalAPI.Domain.Entities.IAM;
using HospitalAPI.Domain.Interfaces.Repositories.IAM;
using HospitalAPI.Infrastructure.Data;

namespace HospitalAPI.Infrastructure.Repositories.IAM;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(HospitalDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByEmailAsync(Guid tenantId, string email)
    {
        return await _dbSet
            .FirstOrDefaultAsync(u => u.TenantId == tenantId && u.Email == email);
    }

    public async Task<IEnumerable<User>> GetByTenantAsync(Guid tenantId)
    {
        return await _dbSet
            .Where(u => u.TenantId == tenantId)
            .ToListAsync();
    }

    public async Task<User?> GetWithRolesAsync(Guid id)
    {
        return await _dbSet
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Id == id);
    }
}
