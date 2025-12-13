using Microsoft.AspNetCore.Mvc;
using HospitalAPI.Application.DTOs.Patient;
using HospitalAPI.Application.Interfaces.Services.Patient;
using HospitalAPI.Common.Models;

namespace HospitalAPI.API.Controllers.Patient;

[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientsController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<PatientDto>>>> GetAll([FromQuery] Guid tenantId)
    {
        var patients = await _patientService.GetAllAsync(tenantId);
        return Ok(ApiResponse<IEnumerable<PatientDto>>.SuccessResponse(patients));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<PatientDto>>> GetById(Guid id)
    {
        var patient = await _patientService.GetByIdAsync(id);
        if (patient == null)
            return NotFound(ApiResponse<PatientDto>.ErrorResponse("Patient not found"));

        return Ok(ApiResponse<PatientDto>.SuccessResponse(patient));
    }

    [HttpGet("mrn/{mrn}")]
    public async Task<ActionResult<ApiResponse<PatientDto>>> GetByMrn([FromQuery] Guid tenantId, string mrn)
    {
        var patient = await _patientService.GetByMrnAsync(tenantId, mrn);
        if (patient == null)
            return NotFound(ApiResponse<PatientDto>.ErrorResponse("Patient not found"));

        return Ok(ApiResponse<PatientDto>.SuccessResponse(patient));
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResponse<IEnumerable<PatientDto>>>> Search([FromQuery] Guid tenantId, [FromQuery] string searchTerm)
    {
        var patients = await _patientService.SearchAsync(tenantId, searchTerm);
        return Ok(ApiResponse<IEnumerable<PatientDto>>.SuccessResponse(patients));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<PatientDto>>> Create([FromQuery] Guid tenantId, [FromBody] CreatePatientDto dto)
    {
        // In a real app, get createdBy from authenticated user
        var createdBy = Guid.NewGuid();
        var patient = await _patientService.CreateAsync(tenantId, dto, createdBy);
        return CreatedAtAction(nameof(GetById), new { id = patient.Id }, ApiResponse<PatientDto>.SuccessResponse(patient, "Patient created successfully"));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<PatientDto>>> Update(Guid id, [FromBody] CreatePatientDto dto)
    {
        // In a real app, get updatedBy from authenticated user
        var updatedBy = Guid.NewGuid();
        var patient = await _patientService.UpdateAsync(id, dto, updatedBy);
        return Ok(ApiResponse<PatientDto>.SuccessResponse(patient, "Patient updated successfully"));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<object>>> Delete(Guid id)
    {
        await _patientService.DeleteAsync(id);
        return Ok(ApiResponse<object>.SuccessResponse(null, "Patient deleted successfully"));
    }
}
