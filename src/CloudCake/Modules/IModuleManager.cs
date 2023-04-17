namespace CloudCake.Modules;

public interface IModuleManager
{
    ModuleInfo StartupModule { get; }

    IReadOnlyList<ModuleInfo> Modules { get; }

    void Initialize(Type startupModule);

    void StartModules();

    void ShutdownModules();
}