﻿using CloudCake.Dependency;
using CloudCake.Plugins;
using System.Collections.Immutable;
using Castle.Core.Logging;
using CloudCake.Exceptions;
using CloudCake.Collections.Extensions;

namespace CloudCake.Modules;

/// <summary>
/// This class is used to manage modules.
/// </summary>
public class ModuleManager : IModuleManager
{
    public ModuleInfo StartupModule { get; private set; }

    public IReadOnlyList<ModuleInfo> Modules => _modules.ToImmutableList();

    public Castle.Core.Logging.ILogger Logger { get; set; }

    private ModuleCollection _modules;

    private readonly IIocManager _iocManager;
    private readonly IPluginManager _pluginManager;

    public ModuleManager(IIocManager iocManager, IPluginManager plugInManager)
    {
        _iocManager = iocManager;
        _pluginManager = plugInManager;

        Logger = NullLogger.Instance;
    }

    public virtual void Initialize(Type startupModule)
    {
        _modules = new ModuleCollection(startupModule);
        LoadAllModules();
    }

    public virtual void StartModules()
    {
        var sortedModules = _modules.GetSortedModuleListByDependency();
        sortedModules.ForEach(module => module.Instance.PreInitialize());
        sortedModules.ForEach(module => module.Instance.Initialize());
        sortedModules.ForEach(module => module.Instance.PostInitialize());
    }

    public virtual void ShutdownModules()
    {
        Logger.Debug("Shutting down has been started");

        var sortedModules = _modules.GetSortedModuleListByDependency();
        sortedModules.Reverse();
        sortedModules.ForEach(sm => sm.Instance.Shutdown());

        Logger.Debug("Shutting down completed.");
    }

    private void LoadAllModules()
    {
        Logger.Debug("Loading Abp modules...");

        List<Type> plugInModuleTypes;
        var moduleTypes = FindAllModuleTypes(out plugInModuleTypes).Distinct().ToList();

        Logger.Debug("Found " + moduleTypes.Count + " ABP modules in total.");

        RegisterModules(moduleTypes);
        CreateModules(moduleTypes, plugInModuleTypes);

        _modules.EnsureKernelModuleToBeFirst();
        _modules.EnsureStartupModuleToBeLast();

        SetDependencies();

        Logger.DebugFormat("{0} modules loaded.", _modules.Count);
    }

    private List<Type> FindAllModuleTypes(out List<Type> plugInModuleTypes)
    {
        plugInModuleTypes = new List<Type>();

        var modules = Module.FindDependedModuleTypesRecursivelyIncludingGivenModule(_modules.StartupModuleType);

        foreach (var plugInModuleType in _pluginManager.PlugInSources.GetAllModules())
        {
            if (modules.AddIfNotContains(plugInModuleType))
            {
                plugInModuleTypes.Add(plugInModuleType);
            }
        }

        return modules;
    }

    private void CreateModules(ICollection<Type> moduleTypes, List<Type> plugInModuleTypes)
    {
        foreach (var moduleType in moduleTypes)
        {
            var moduleObject = _iocManager.Resolve(moduleType) as Module;
            if (moduleObject == null)
            {
                throw new CloudCakeInitializationException("This type is not an ABP module: " + moduleType.AssemblyQualifiedName);
            }

            moduleObject.IocManager = _iocManager;
            moduleObject.Configuration = _iocManager.Resolve<IStartupConfiguration>();

            var moduleInfo = new ModuleInfo(moduleType, moduleObject, plugInModuleTypes.Contains(moduleType));

            _modules.Add(moduleInfo);

            if (moduleType == _modules.StartupModuleType)
            {
                StartupModule = moduleInfo;
            }

            Logger.DebugFormat("Loaded module: " + moduleType.AssemblyQualifiedName);
        }
    }

    private void RegisterModules(ICollection<Type> moduleTypes)
    {
        foreach (var moduleType in moduleTypes)
        {
            _iocManager.RegisterIfNot(moduleType);
        }
    }

    private void SetDependencies()
    {
        foreach (var moduleInfo in _modules)
        {
            moduleInfo.Dependencies.Clear();

            //Set dependencies for defined DependsOnAttribute attribute(s).
            foreach (var dependedModuleType in Module.FindDependedModuleTypes(moduleInfo.Type))
            {
                var dependedModuleInfo = _modules.FirstOrDefault(m => m.Type == dependedModuleType);
                if (dependedModuleInfo == null)
                {
                    throw new InitializationException("Could not find a depended module " + dependedModuleType.AssemblyQualifiedName + " for " + moduleInfo.Type.AssemblyQualifiedName);
                }

                if ((moduleInfo.Dependencies.FirstOrDefault(dm => dm.Type == dependedModuleType) == null))
                {
                    moduleInfo.Dependencies.Add(dependedModuleInfo);
                }
            }
        }
    }
}
