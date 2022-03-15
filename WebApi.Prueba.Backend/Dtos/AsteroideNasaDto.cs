using Newtonsoft.Json;
using System;
using System.Collections.Generic;
public class AsteroideNasaDto
{
    [JsonProperty("links")]
    public Links Links { get; set; }

    [JsonProperty("element_count")]
    public int ElementCount { get; set; }

    [JsonProperty("near_earth_objects")]
    public Dictionary<DateTime,List<Info>> NearEarthObjects { get; set; }
}
public class Links
{
    [JsonProperty("next")]
    public string Next { get; set; }

    [JsonProperty("prev")]
    public string Prev { get; set; }

    [JsonProperty("self")]
    public string Self { get; set; }
}

public class Kilometers
{
    [JsonProperty("estimated_diameter_min")]
    public double EstimatedDiameterMin { get; set; }

    [JsonProperty("estimated_diameter_max")]
    public double EstimatedDiameterMax { get; set; }
}

public class Meters
{
    [JsonProperty("estimated_diameter_min")]
    public double EstimatedDiameterMin { get; set; }

    [JsonProperty("estimated_diameter_max")]
    public double EstimatedDiameterMax { get; set; }
}

public class Miles
{
    [JsonProperty("estimated_diameter_min")]
    public double EstimatedDiameterMin { get; set; }

    [JsonProperty("estimated_diameter_max")]
    public double EstimatedDiameterMax { get; set; }
}

public class Feet
{
    [JsonProperty("estimated_diameter_min")]
    public double EstimatedDiameterMin { get; set; }

    [JsonProperty("estimated_diameter_max")]
    public double EstimatedDiameterMax { get; set; }
}

public class EstimatedDiameter
{
    [JsonProperty("kilometers")]
    public Kilometers Kilometers { get; set; }

    [JsonProperty("meters")]
    public Meters Meters { get; set; }

    [JsonProperty("miles")]
    public Miles Miles { get; set; }

    [JsonProperty("feet")]
    public Feet Feet { get; set; }
}

public class RelativeVelocity
{
    [JsonProperty("kilometers_per_second")]
    public string KilometersPerSecond { get; set; }

    [JsonProperty("kilometers_per_hour")]
    public string KilometersPerHour { get; set; }

    [JsonProperty("miles_per_hour")]
    public string MilesPerHour { get; set; }
}

public class MissDistance
{
    [JsonProperty("astronomical")]
    public string Astronomical { get; set; }

    [JsonProperty("lunar")]
    public string Lunar { get; set; }

    [JsonProperty("kilometers")]
    public string Kilometers { get; set; }

    [JsonProperty("miles")]
    public string Miles { get; set; }
}

public class CloseApproachData
{
    [JsonProperty("close_approach_date")]
    public string CloseApproachDate { get; set; }

    [JsonProperty("close_approach_date_full")]
    public string CloseApproachDateFull { get; set; }

    [JsonProperty("epoch_date_close_approach")]
    public object EpochDateCloseApproach { get; set; }

    [JsonProperty("relative_velocity")]
    public RelativeVelocity RelativeVelocity { get; set; }

    [JsonProperty("miss_distance")]
    public MissDistance MissDistance { get; set; }

    [JsonProperty("orbiting_body")]
    public string OrbitingBody { get; set; }
}

public class Info
    {
    [JsonProperty("links")]
    public Links Links { get; set; }

    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("neo_reference_id")]
    public string NeoReferenceId { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("nasa_jpl_url")]
    public string NasaJplUrl { get; set; }

    [JsonProperty("absolute_magnitude_h")]
    public double AbsoluteMagnitudeH { get; set; }

    [JsonProperty("estimated_diameter")]
    public EstimatedDiameter EstimatedDiameter { get; set; }

    [JsonProperty("is_potentially_hazardous_asteroid")]
    public bool IsPotentiallyHazardousAsteroid { get; set; }

    [JsonProperty("close_approach_data")]
    public List<CloseApproachData> CloseApproachData { get; set; }

    [JsonProperty("is_sentry_object")]
    public bool IsSentryObject { get; set; }
}




