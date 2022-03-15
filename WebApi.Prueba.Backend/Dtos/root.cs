// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class root
{
   
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("neo_reference_id")]
    public string NeoReferenceId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("nasa_jpl_url")]
    public string NasaJplUrl { get; set; }

    [JsonPropertyName("absolute_magnitude_h")]
    public double AbsoluteMagnitudeH { get; set; }

    [JsonPropertyName("is_potentially_hazardous_asteroid")]
    public bool IsPotentiallyHazardousAsteroid { get; set; }

    [JsonPropertyName("is_sentry_object")]
    public bool IsSentryObject { get; set; }
}




