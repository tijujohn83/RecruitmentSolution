using System.Text.Json.Serialization;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ApplicationStatus
{
    Open,
    Selected,
    Rejected
}

public enum UpdateStatusResult
{
    NotFound,
    Success,
    Failure
}