using System.Reflection;

namespace Evently.Api.Extensions;

internal static class ConfigurationExtensions
{
    internal static void AddModuleConfiguration(this IConfigurationBuilder configurationBuilder, params Assembly[] assemblies)
    {
        IEnumerable<string> modules = assemblies.Select(assembly => assembly.FullName!.Split('.')[2].ToLowerInvariant());

        foreach (string module in modules)
        {
            configurationBuilder.AddJsonFile($"modules.{module}.json", false, true);
            configurationBuilder.AddJsonFile($"modules.{module}.Development.json", true, true);
        }
    }
}
