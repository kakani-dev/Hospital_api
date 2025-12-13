namespace HospitalAPI.Common.Helpers;

public static class MrnGenerator
{
    private static readonly Random _random = new();
    
    public static string Generate(string branchCode)
    {
        var timestamp = DateTime.UtcNow.ToString("yyyyMMdd");
        var randomPart = _random.Next(1000, 9999);
        return $"{branchCode}-{timestamp}-{randomPart}";
    }
}
