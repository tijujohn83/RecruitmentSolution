using System.Text.Json.Serialization;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ApplicationStatus
{
    Open,
    Accepted,
    Rejected
}

public enum UpdateStatusResult
{
    NotFound,
    Success,
    Failure
}