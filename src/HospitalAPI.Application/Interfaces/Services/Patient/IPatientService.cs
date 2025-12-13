using HospitalAPI.Application.DTOs.Patient;
using HospitalAPI.Common.Models;

namespace HospitalAPI.Application.Interfaces.Services.Patient;

public interface IPatientService
{
    Task<PatientDto?> GetByIdAsync(Guid id);
    Task<PatientDto?> GetByMrnAsync(Guid tenantId, string mrn);
    Task<IEnumerable<PatientDto>> GetAllAsync(Guid tenantId);
    Task<IEnumerable<PatientDto>> SearchAsync(Guid tenantId, string searchTerm);
    Task<PatientDto> CreateAsync(Guid tenantId, CreatePatientDto dto, Guid createdBy);
    Task<PatientDto> UpdateAsync(Guid id, CreatePatientDto dto, Guid updatedBy);
    Task DeleteAsync(Guid id);
}
