using HospitalAPI.Domain.Interfaces.Repositories.IAM;
using HospitalAPI.Domain.Interfaces.Repositories.Patient;
using HospitalAPI.Domain.Interfaces.Repositories.Billing;

namespace HospitalAPI.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    IRoleRepository Roles { get; }
    IPatientRepository Patients { get; }
    IInvoiceRepository Invoices { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}
