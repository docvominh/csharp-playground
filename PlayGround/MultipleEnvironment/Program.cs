// See https://aka.ms/new-console-template for more information

using Configuration.MultipleEnvironment;
using Microsoft.Extensions.Configuration;

// Try get from environment variable. If not found, fallback to dev
var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "dev";

var config = new ConfigurationBuilder()
    .AddJsonFile($"appsettings.{environment}.json")
    .Build();

var connectionStringSettings = config.GetSection("ConnectionStrings").Get<ConnectionStringSettings>();
Console.WriteLine(connectionStringSettings?.AsiaDb);
Console.WriteLine(connectionStringSettings?.EuroDb);