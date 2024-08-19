using System.Text.Json.Serialization;

namespace DotNet.ServiceTemplate.Domain;

public record Person
{
    [JsonIgnore]
    public int? Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
