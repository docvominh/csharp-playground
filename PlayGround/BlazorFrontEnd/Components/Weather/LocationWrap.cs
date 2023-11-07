using System.Text.Json.Serialization;

namespace BlazorFrontEnd.Components.Weather;

public class LocationWrap
{
    public Location? Location { get; set; }
    public Current? Current { get; set; }
}

public class Location
{
    // "name": "Da Nang",
    // "region": "",
    // "country": "Vietnam",
    // "lat": 21.72,
    // "lon": 105.35,
    // "tz_id": "Asia/Bangkok",
    // "localtime_epoch": 1692672656,
    // "localtime": "2023-08-22 9:50"

    public string Name { get; set; }
    public string Region { get; set; }
    public string Country { get; set; }
    public double Lat { get; set; }
    public double Lon { get; set; }
    public string TimezoneId { get; set; }

    // [JsonConverter(typeof(WeatherApiDateTimeConverter))] // Also work
    public DateTime LocalTime { get; set; }
}

public class Current
{
    [JsonPropertyName("temp_c")]
    public float TempC { get; set; }
    
    [JsonPropertyName("temp_f")]
    public float TempF { get; set; }



    public Condition Condition { get; set; }
    
// "last_updated_epoch": 1692672300,
// "last_updated": "2023-08-22 09:45",
// "temp_c": 27.3,
// "temp_f": 81.1,
// "is_day": 1,
// "condition": {
//     "text": "Cloudy",
//     "icon": "//cdn.weatherapi.com/weather/64x64/day/119.png",
//     "code": 1006
// },
// "wind_mph": 2.2,
// "wind_kph": 3.6,
// "wind_degree": 225,
// "wind_dir": "SW",
// "pressure_mb": 1007,
// "pressure_in": 29.72,
// "precip_mm": 0,
// "precip_in": 0,
// "humidity": 84,
// "cloud": 86,
// "feelslike_c": 31.4,
// "feelslike_f": 88.5,
// "vis_km": 10,
// "vis_miles": 6,
// "uv": 6,
// "gust_mph": 1.1,
// "gust_kph": 1.8
}

public class Condition
{
    public string Text { get; set; }
    public string Icon { get; set; }
    public int Code { get; set; }
}