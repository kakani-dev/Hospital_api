using Microsoft.EntityFrameworkCore;
using HospitalAPI.Domain.Entities.IAM;
using HospitalAPI.Domain.Entities.Organization;
using HospitalAPI.Domain.Entities.Patient;
using HospitalAPI.Domain.Entities.Clinical;
using HospitalAPI.Domain.Entities.Billing;
using HospitalAPI.Domain.Entities.Audit;

namespace HospitalAPI.Infrastructure.Data;

public class HospitalDbContext : DbContext
{
    public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options)
    {
    }

    // IAM Schema
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }

    // Organization Schema
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<Facility> Facilities { get; set; }
    public DbSet<Department> Departments { get; set; }

    // Patient Schema
    public DbSet<Domain.Entities.Patient.Patient> Patients { get; set; }

    // Clinical Schema
    public DbSet<Staff> Staff { get; set; }
    public DbSet<Encounter> Encounters { get; set; }
    public DbSet<Appointment> Appointments { get; set; }

    // Billing Schema
    public DbSet<ServiceCatalog> ServiceCatalog { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<InvoiceItem> InvoiceItems { get; set; }
    public DbSet<Payment> Payments { get; set; }

    // Audit Schema
    public DbSet<AuditLog> AuditLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply configurations
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HospitalDbContext).Assembly);

        // Global query filters for soft delete
        modelBuilder.Entity<User>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Role>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Tenant>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Branch>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Facility>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Department>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Domain.Entities.Patient.Patient>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Staff>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Encounter>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Appointment>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Invoice>().HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Payment>().HasQueryFilter(e => !e.IsDeleted);
    }
}
