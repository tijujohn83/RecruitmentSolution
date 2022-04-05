using System.Text.Json.Serialization;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ApplicationStatus
{
    Open,
    Accepted,
    Rejected
}