using System.Reflection;

namespace Evently.Api.Extensions;

internal static class ConfigurationExtensions
{
    internal static void AddModuleConfiguration(this IConfigurationBuilder configurationBuilder, Assembly[] assemblies)
    {
        string[] modules = assemblies.Select(assembly => 
            assembly.FullName!.Replace("Evently.Modules.", string.Empty).Split(".")[0].ToLowerInvariant()).ToArray();

        foreach (string module in modules)
        {
            configurationBuilder.AddJsonFile($"modules.{module}.json", false, true);
            configurationBuilder.AddJsonFile($"modules.{module}.Development.json", true, true);
        }
    }
}
