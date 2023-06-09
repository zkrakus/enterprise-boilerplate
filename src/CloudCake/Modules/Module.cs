﻿using CloudCake.Collections.Extensions;
using CloudCake.Configuration.Startup;
using CloudCake.Dependency;
using CloudCake.Exceptions;
using Microsoft.Extensions.Logging.Abstractions;
using System.Reflection;

namespace CloudCake.Modules;

/// <summary>
/// This class must be implemented by all module definition classes.
/// </summary>
/// <remarks>
/// A module definition class is generally located in its own assembly
/// and implements some action in module events on application startup and shutdown.
/// It also defines depended modules.
/// </remarks>
public abstract class Module
{
    /// <summary>
    /// Gets a reference to the IOC manager.
    /// </summary>
    protected internal IIocManager? IocManager { get; internal set; }

    /// <summary>
    /// Gets a reference to the CloudCake configuration.
    /// </summary>
    protected internal IStartupConfiguration? Configuration { get; internal set; }

    /// <summary>
    /// Gets or sets the logger.
    /// </summary>
    public ILogger Logger { get; set; }

    protected Module()
    {
        Logger = NullLogger.Instance;
    }

    /// <summary>
    /// This is the first event called on application startup.
    /// Codes can be placed here to run before dependency injection registrations.
    /// </summary>
    public abstract void PreInitialize();

    /// <summary>
    /// This method is used to register dependencies for this module.
    /// </summary>
    public abstract void Initialize();

    /// <summary>
    /// This method is called lastly on application startup.
    /// </summary>
    public abstract void PostInitialize();

    /// <summary>
    /// This method is called when the application is being shutdown.
    /// </summary>
    public abstract void Shutdown();

    public virtual Assembly[] GetAdditionalAssemblies()
    {
        return new Assembly[0];
    }

    /// <summary>
    /// Checks if given type is an Abp module class.
    /// </summary>
    /// <param name="type">Type to check</param>
    public static bool IsAbpModule(Type type)
    {
        var typeInfo = type.GetTypeInfo();
        return
            typeInfo.IsClass &&
            !typeInfo.IsAbstract &&
            !typeInfo.IsGenericType &&
            typeof(Module).IsAssignableFrom(type);
    }

    /// <summary>
    /// Finds direct depended modules of a module (excluding given module).
    /// </summary>
    public static List<Type> FindDependedModuleTypes(Type moduleType)
    {
        if (!IsAbpModule(moduleType))
        {
            throw new CloudCakeInitializationException("This type is not an ABP module: " + moduleType.AssemblyQualifiedName);
        }

        var list = new List<Type>();

        if (moduleType.GetTypeInfo().IsDefined(typeof(DependsOnAttribute), true))
        {
            var dependsOnAttributes = moduleType.GetTypeInfo().GetCustomAttributes(typeof(DependsOnAttribute), true).Cast<DependsOnAttribute>();
            foreach (var dependsOnAttribute in dependsOnAttributes)
            {
                foreach (var dependedModuleType in dependsOnAttribute.DependedModuleTypes)
                {
                    list.Add(dependedModuleType);
                }
            }
        }

        return list;
    }

    public static List<Type> FindDependedModuleTypesRecursivelyIncludingGivenModule(Type moduleType)
    {
        var list = new List<Type>();
        AddModuleAndDependenciesRecursively(list, moduleType);
        list.AddIfNotContains(typeof(KernelModule));
        return list;
    }

    private static void AddModuleAndDependenciesRecursively(List<Type> modules, Type module)
    {
        if (!IsAbpModule(module))
        {
            throw new CloudCakeInitializationException("This type is not an ABP module: " + module.AssemblyQualifiedName);
        }

        if (modules.Contains(module))
        {
            return;
        }

        modules.Add(module);

        var dependedModules = FindDependedModuleTypes(module);
        foreach (var dependedModule in dependedModules)
        {
            AddModuleAndDependenciesRecursively(modules, dependedModule);
        }
    }
}
