using Microsoft.EntityFrameworkCore.Storage;
using HospitalAPI.Domain.Interfaces;
using HospitalAPI.Domain.Interfaces.Repositories.IAM;
using HospitalAPI.Domain.Interfaces.Repositories.Patient;
using HospitalAPI.Domain.Interfaces.Repositories.Billing;
using HospitalAPI.Infrastructure.Repositories.IAM;
using HospitalAPI.Infrastructure.Repositories.Patient;
using HospitalAPI.Infrastructure.Repositories.Billing;

namespace HospitalAPI.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly HospitalDbContext _context;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(HospitalDbContext context)
    {
        _context = context;
        Users = new UserRepository(_context);
        Roles = new RoleRepository(_context);
        Patients = new PatientRepository(_context);
        Invoices = new InvoiceRepository(_context);
    }

    public IUserRepository Users { get; }
    public IRoleRepository Roles { get; }
    public IPatientRepository Patients { get; }
    public IInvoiceRepository Invoices { get; }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}
