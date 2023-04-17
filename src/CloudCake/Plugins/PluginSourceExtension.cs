using CloudCake.Modules;

namespace CloudCake.Plugins;

public static class PlugInSourceExtensions
{
    public static List<Type> GetModulesWithAllDependencies(this IPluginSource plugInSource)
    {
        return plugInSource
            .GetModules()
            .SelectMany(Module.FindDependedModuleTypesRecursivelyIncludingGivenModule)
            .Distinct()
            .ToList();
    }
}