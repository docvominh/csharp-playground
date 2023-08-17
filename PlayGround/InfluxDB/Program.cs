// See https://aka.ms/new-console-template for more information

using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Writes;

namespace InfluxDB;

public class Program
{
    private static readonly Random _random = new Random();

    public static async Task Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        var client = GetClient();

        // Write
        var writeApi = client.GetWriteApi();
        for (var i = 100; i < 10000; i++)
        {
            var point = PointData.Measurement("myrandom")
                .Tag("plane", "test-plane")
                .Field("value", _random.Next(1000, 5000))
                .Timestamp(DateTime.UtcNow, WritePrecision.Ns);

            writeApi.WritePoint(point, "test-bucket", "organization");
        }

        writeApi.Flush();

        // Read
        var query = client.GetQueryApi();
        var flux = "from(bucket:\"test-bucket\") |> range(start: 0)";
        var tables = await query.QueryAsync(flux, "organization");
        var result = tables.SelectMany(table =>
            table.Records.Select(record =>
                new AltitudeModel
                {
                    Time = record.GetTime().ToString(),
                    Altitude = int.Parse(record.GetValueByKey("_value").ToString() ?? string.Empty)
                }));

        foreach (var model in result)
        {
            Console.WriteLine(model.DisplayText);
        }
    }

    private static InfluxDBClient GetClient()
    {
        const string token = "JMNlova9CWwbVfQYtdVBZMAbhKjaCneV3W8QkXfqFLyGHFVxgQLI6KsK6QLdF2xIuzCT0PFy0ZX-42dsKju1cA==";
        const string bucket = "bucket";
        const string org = "organization";
        return new InfluxDBClient("http://localhost:8086", token);
    }
}

public class AltitudeModel
{
    public string? Time { get; init; }
    public int Altitude { get; init; }
    public string DisplayText => $"Plane was at altitude {Altitude} ft. at {Time}.";
}