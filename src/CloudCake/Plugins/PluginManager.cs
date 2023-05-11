namespace CloudCake.Plugins;

public class PluginManager : IPluginManager
{
    public PlugInSourceList PlugInSources { get; }

    private static readonly object _lock = new();
    private static bool _isRegisteredToAssemblyResolve;

    public PluginManager()
    {
        PlugInSources = new PlugInSourceList();

        //TODO: Try to use AssemblyLoadContext.Default..?
        RegisterToAssemblyResolve(PlugInSources);
    }

    private static void RegisterToAssemblyResolve(PlugInSourceList plugInSources)
    {
        if (_isRegisteredToAssemblyResolve)
        {
            return;
        }

        lock (_lock)
        {
            if (_isRegisteredToAssemblyResolve)
            {
                return;
            }

            _isRegisteredToAssemblyResolve = true;

            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            {
                return plugInSources.GetAllAssemblies().FirstOrDefault(a => a.FullName == args.Name);
            };
        }
    }
}