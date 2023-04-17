using System.Reflection;

namespace CloudCake.Plugins;

public class PlugInSourceList : List<IPluginSource>
{
    public List<Assembly> GetAllAssemblies()
    {
        return this
            .SelectMany(pluginSource => pluginSource.GetAssemblies())
            .Distinct()
            .ToList();
    }

    public List<Type> GetAllModules()
    {
        return this
            .SelectMany(pluginSource => pluginSource.GetModulesWithAllDependencies())
            .Distinct()
            .ToList();
    }
}
