namespace HospitalAPI.Common.Exceptions;

public class TenantMismatchException : Exception
{
    public TenantMismatchException(string message) : base(message)
    {
    }

    public TenantMismatchException() 
        : base("Tenant mismatch detected. Access denied.")
    {
    }
}
