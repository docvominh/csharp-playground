// See https://aka.ms/new-console-template for more information

using Configuration.Simple;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var connectionStringSettings = config.GetSection("ConnectionStrings").Get<ConnectionStringSettings>();
Console.WriteLine(connectionStringSettings?.AsiaDb);
Console.WriteLine(connectionStringSettings?.EuroDb);