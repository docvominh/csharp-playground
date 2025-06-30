// See https://aka.ms/new-console-template for more information

using System.Reflection;
using DbUp;

Console.WriteLine("Migration started");

var connectionString =
    "Server=localhost,1444;User ID=sa;Password=MyPassword12#;Database=dotSolutions_dbUp;trustServerCertificate=True;";

// Make sure db always create
EnsureDatabase.For.SqlDatabase(connectionString);

var upgrader =
    DeployChanges.To
        .SqlDatabase(connectionString)
        .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
        .LogToConsole()
        .Build();

var result = upgrader.PerformUpgrade();

if (!result.Successful)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(result.Error);
    Console.ResetColor();
#if DEBUG
    Console.ReadLine();
#endif
    return -1;
}

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Success!");
Console.ResetColor();
return 0;