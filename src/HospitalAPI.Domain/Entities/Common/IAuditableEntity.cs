namespace HospitalAPI.Domain.Entities.Common;

public interface IAuditableEntity
{
    Guid CreatedBy { get; set; }
    DateTime CreatedOn { get; set; }
    Guid? UpdatedBy { get; set; }
    DateTime? UpdatedOn { get; set; }
}
