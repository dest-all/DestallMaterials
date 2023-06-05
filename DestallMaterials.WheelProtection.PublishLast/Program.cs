using DestallMaterials.WheelProtection.Extensions;
using DestallMaterials.WheelProtection.Extensions.Enumerables;
using DestallMaterials.WheelProtection.Extensions.Strings;
using DestallMaterials.WheelProtection.PublishLast;
using System.Security;

string dir = Environment.CurrentDirectory;

Do(dir);
Console.ReadKey();

static void Do(string directory)
{
    const string apiKeyVariableName = "NugetPublishingApiKey";

    string? nugetApiKey = Environment.GetEnvironmentVariable(apiKeyVariableName);

    if (nugetApiKey.IsEmpty())
    {
        Console.WriteLine($"Api key for nuget should be set in environment variable {apiKeyVariableName}.");
        Console.WriteLine($"Aborted");
        return;
    }

    var packages = Directory.GetFiles(directory, "*.nupkg");

    if (packages.IsEmpty())
    {
        Console.WriteLine($"No *.nupkg found.");
        return;
    }

    var packagesFromNewestToOldest = packages.OrderDescending();

    var packageToPush = packagesFromNewestToOldest.First();

    string command = $"dotnet nuget push {packageToPush} --api-key {nugetApiKey} --source https://api.nuget.org/v3/index.json";

    Console.WriteLine(@$"Running command:
{command}");

    CmdRunner.Run(command);
}
