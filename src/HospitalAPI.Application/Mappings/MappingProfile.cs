using AutoMapper;
using HospitalAPI.Application.DTOs.Patient;
using HospitalAPI.Application.DTOs.IAM;

namespace HospitalAPI.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Patient mappings
        CreateMap<Domain.Entities.Patient.Patient, PatientDto>();
        CreateMap<CreatePatientDto, Domain.Entities.Patient.Patient>();

        // User mappings
        CreateMap<Domain.Entities.IAM.User, UserDto>();
        CreateMap<CreateUserDto, Domain.Entities.IAM.User>();
    }
}
