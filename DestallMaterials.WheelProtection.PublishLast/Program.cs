using DestallMaterials.WheelProtection.Extensions.String;
using DestallMaterials.WheelProtection.Linq;
using System.Security;

const string apiKeyVariableName = "NugetPublishingApiKey";

string? nugetApiKey = Environment.GetEnvironmentVariable(apiKeyVariableName);

if (nugetApiKey.IsEmpty())
{
    Console.WriteLine($"Api key for nuget should be set in environment variable {apiKeyVariableName}.");
    Console.WriteLine($"Aborted");
    return;
}

var curDir = Directory.GetCurrentDirectory();
var packages = Directory.GetFiles(curDir, "*.nupkg");

if (!packages.HasContent())
{
    Console.WriteLine($"");
}

var packagesFromNewestToOldest = packages.OrderByDescending(p => p);

var packageToPush = packagesFromNewestToOldest.First