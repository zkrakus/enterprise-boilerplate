using System.Reflection;

namespace CloudCake.Plugins;

public interface IPluginSource
{
    List<Assembly> GetAssemblies();

    List<Type> GetModules();
}