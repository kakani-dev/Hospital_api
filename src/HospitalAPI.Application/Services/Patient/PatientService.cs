using AutoMapper;
using HospitalAPI.Application.DTOs.Patient;
using HospitalAPI.Application.Interfaces.Services.Patient;
using HospitalAPI.Common.Exceptions;
using HospitalAPI.Common.Helpers;
using HospitalAPI.Domain.Interfaces;

namespace HospitalAPI.Application.Services.Patient;

public class PatientService : IPatientService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PatientService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PatientDto?> GetByIdAsync(Guid id)
    {
        var patient = await _unitOfWork.Patients.GetByIdAsync(id);
        return patient == null ? null : _mapper.Map<PatientDto>(patient);
    }

    public async Task<PatientDto?> GetByMrnAsync(Guid tenantId, string mrn)
    {
        var patient = await _unitOfWork.Patients.GetByMrnAsync(tenantId, mrn);
        return patient == null ? null : _mapper.Map<PatientDto>(patient);
    }

    public async Task<IEnumerable<PatientDto>> GetAllAsync(Guid tenantId)
    {
        var patients = await _unitOfWork.Patients.FindAsync(p => p.TenantId == tenantId);
        return _mapper.Map<IEnumerable<PatientDto>>(patients);
    }

    public async Task<IEnumerable<PatientDto>> SearchAsync(Guid tenantId, string searchTerm)
    {
        var patients = await _unitOfWork.Patients.SearchAsync(tenantId, searchTerm);
        return _mapper.Map<IEnumerable<PatientDto>>(patients);
    }

    public async Task<PatientDto> CreateAsync(Guid tenantId, CreatePatientDto dto, Guid createdBy)
    {
        var patient = _mapper.Map<Domain.Entities.Patient.Patient>(dto);
        patient.Id = Guid.NewGuid();
        patient.TenantId = tenantId;
        patient.Mrn = MrnGenerator.Generate("MRN"); // You can pass branch code here
        patient.CreatedBy = createdBy;
        patient.CreatedOn = DateTime.UtcNow;

        await _unitOfWork.Patients.AddAsync(patient);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<PatientDto>(patient);
    }

    public async Task<PatientDto> UpdateAsync(Guid id, CreatePatientDto dto, Guid updatedBy)
    {
        var patient = await _unitOfWork.Patients.GetByIdAsync(id);
        if (patient == null)
            throw new NotFoundException("Patient", id);

        _mapper.Map(dto, patient);
        patient.UpdatedBy = updatedBy;
        patient.UpdatedOn = DateTime.UtcNow;

        await _unitOfWork.Patients.UpdateAsync(patient);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<PatientDto>(patient);
    }

    public async Task DeleteAsync(Guid id)
    {
        var patient = await _unitOfWork.Patients.GetByIdAsync(id);
        if (patient == null)
            throw new NotFoundException("Patient", id);

        patient.IsDeleted = true;
        await _unitOfWork.Patients.UpdateAsync(patient);
        await _unitOfWork.SaveChangesAsync();
    }
}
