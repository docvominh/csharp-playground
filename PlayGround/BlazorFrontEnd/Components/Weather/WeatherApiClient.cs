using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlazorFrontEnd.Components.Weather;

public class WeatherApiClient
{
    private const string RequestPath = @"/v1/current.json?key=c3715dabff8440939fc32648231602";
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _serializerOptions;

    public WeatherApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _serializerOptions = new JsonSerializerOptions()
        {
            ReferenceHandler = ReferenceHandler.Preserve,
            PropertyNameCaseInsensitive = true,
            Converters = { new WeatherApiDateTimeConverter() }
        };
    }

    public async Task<LocationWrap?> GetLocationByCity(string city)
    {
        return await _httpClient.GetFromJsonAsync<LocationWrap>($"{RequestPath}&q={city}", _serializerOptions);
    }

    public async Task<List<LocationWrap>?> GetLocationByCities(IEnumerable<string> cities)
    {
        var locations = new List<LocationWrap>();

        foreach (var city in cities)
        {
            var locationWrap =
                await _httpClient.GetFromJsonAsync<LocationWrap>($"{RequestPath}&q={city}", _serializerOptions);
            if (locationWrap != null) locations.Add(locationWrap);
        }

        return locations;
    }
}