namespace HospitalAPI.Common.Constants;

public static class PermissionConstants
{
    // Patient permissions
    public const string PatientRead = "patient.read";
    public const string PatientWrite = "patient.write";
    public const string PatientDelete = "patient.delete";
    
    // Clinical permissions
    public const string ClinicalRead = "clinical.read";
    public const string ClinicalWrite = "clinical.write";
    public const string ClinicalDelete = "clinical.delete";
    
    // Billing permissions
    public const string BillingRead = "billing.read";
    public const string BillingWrite = "billing.write";
    public const string BillingDelete = "billing.delete";
    
    // IAM permissions
    public const string IAMRead = "iam.read";
    public const string IAMWrite = "iam.write";
    public const string IAMDelete = "iam.delete";
    
    // Organization permissions
    public const string OrgRead = "org.read";
    public const string OrgWrite = "org.write";
    public const string OrgDelete = "org.delete";
}
