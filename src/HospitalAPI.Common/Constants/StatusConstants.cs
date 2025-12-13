namespace HospitalAPI.Common.Constants;

public static class StatusConstants
{
    public const string Active = "active";
    public const string Inactive = "inactive";
    public const string Deleted = "deleted";
    
    // Appointment statuses
    public const string Booked = "booked";
    public const string Confirmed = "confirmed";
    public const string Cancelled = "cancelled";
    public const string Completed = "completed";
    
    // Encounter statuses
    public const string Open = "open";
    public const string Closed = "closed";
    
    // Invoice statuses
    public const string Draft = "draft";
    public const string Finalized = "finalized";
    public const string Paid = "paid";
}
