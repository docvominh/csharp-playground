using System;
using System.Net.Http;
using BlazorFrontEnd.Components.Weather;

namespace BlazorFrontEnd.Tests.Components.Weather;

public class WeatherApiClientTests
{
    private readonly WeatherApiClient _client;

    public WeatherApiClientTests()
    {
        var httpClient = new HttpClient { BaseAddress = new Uri("https://api.weatherapi.com") };
        _client = new WeatherApiClient(httpClient);
    }


    [Fact]
    public async void TestGetLocationByCity()
    {
        var locationWrap = await _client.GetLocationByCity("Da Nang");

        Assert.NotNull(locationWrap);
        Assert.Equal("Vietnam", locationWrap.Location?.Country);
        Assert.Equal("Da Nang", locationWrap.Location?.Name);
        Assert.Equal(DateTime.Now.Year, locationWrap.Location?.LocalTime.Year);
        Assert.Equal(DateTime.Now.Month, locationWrap.Location?.LocalTime.Month);
        Assert.Equal(DateTime.Now.Day, locationWrap.Location?.LocalTime.Day);


        Assert.NotNull(locationWrap.Current);
        Assert.NotEqual(0, locationWrap.Current.TempC);
        Assert.NotEqual(0, locationWrap.Current.TempF);
        Assert.NotNull(locationWrap.Current);
        Assert.NotNull(locationWrap.Current.Condition);
    }

    [Fact]
    public async void TestGetLocationByCities()
    {
        var locationWraps = await _client.GetLocationByCities(new[] { "Da Nang", "Ha Noi" });

        Assert.NotNull(locationWraps);
        Assert.Equal(2, locationWraps.Count);
    }
}