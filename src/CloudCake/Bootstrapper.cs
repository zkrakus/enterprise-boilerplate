using CloudCake.Dependency;
using CloudCake.Modules;
using CloudCake.Plugins;
using CloudCake.Utilities;
using JetBrains.Annotations;
using System.ComponentModel;
using System.Reflection;
using Castle.Core.Logging;
using Castle.MicroKernel.Registration;
using ILogger = Castle.Core.Logging.ILogger;
using NullLogger = Castle.Core.Logging.NullLogger;
using Module = CloudCake.Modules.Module;
using Component = Castle.MicroKernel.Registration.Component;
using ILoggerFactory = Castle.Core.Logging.ILoggerFactory;

namespace CloudCake;

/// <summary>
/// This is the main class that is responsible to start entire CloudCake system.
/// Prepares dependency injection and registers core components needed for startup.
/// It must be instantiated and initialized (see <see cref="Initialize"/>) first in an application.
/// </summary>
public class Bootstrapper : IDisposable
{
    /// <summary>
    /// Get the startup module of the application which depends on other used modules.
    /// </summary>
    public Type StartupModule { get; }

    /// <summary>
    /// A list of plug in folders.
    /// </summary>
    public PlugInSourceList PlugInSources { get; }

    /// <summary>
    /// Gets IIocManager object used by this class.
    /// </summary>
    public IIocManager IocManager { get; }

    /// <summary>
    /// Is this object disposed before?
    /// </summary>
    protected bool IsDisposed;

    private ModuleManager? _moduleManager;
    private ILogger _logger;

    /// <summary>
    /// Creates a new <see cref="Bootstrapper"/> instance.
    /// </summary>
    /// <param name="startupModule">Startup module of the application which depends on other used modules. Should be derived from <see cref="Modules.Module"/>.</param>
    /// <param name="optionsAction">An action to set options</param>
    private Bootstrapper(
        [NotNull] Type startupModule,
        [CanBeNull] Action<BootstrapperOptions>? optionsAction = null)
    {
        Check.NotNull(startupModule, nameof(startupModule));

        var options = new BootstrapperOptions();
        optionsAction?.Invoke(options);

        if (!typeof(Modules.Module).GetTypeInfo().IsAssignableFrom(startupModule))
        {
            throw new ArgumentException($"{nameof(startupModule)} should be derived from {nameof(Modules.Module)}.");
        }

        StartupModule = startupModule;

        IocManager = options.IocManager;
        PlugInSources = options.PlugInSources;

        _logger = NullLogger.Instance;

        AddInterceptorRegistrars(options.InterceptorOptions);
    }

    /// <summary>
    /// Creates a new <see cref="AbpBootstrapper"/> instance.
    /// </summary>
    /// <typeparam name="TStartupModule">Startup module of the application which depends on other used modules. Should be derived from <see cref="AbpModule"/>.</typeparam>
    /// <param name="optionsAction">An action to set options</param>
    public static Bootstrapper Create<TStartupModule>(
        [CanBeNull] Action<BootstrapperOptions>? optionsAction = null)
        where TStartupModule : Module
    {
        return new Bootstrapper(typeof(TStartupModule), optionsAction);
    }

    /// <summary>
    /// Creates a new <see cref="AbpBootstrapper"/> instance.
    /// </summary>
    /// <param name="startupModule">Startup module of the application which depends on other used modules. Should be derived from <see cref="AbpModule"/>.</param>
    /// <param name="optionsAction">An action to set options</param>
    public static Bootstrapper Create(
        [NotNull] Type startupModule,
        [CanBeNull] Action<BootstrapperOptions>? optionsAction = null)
    {
        return new Bootstrapper(startupModule, optionsAction);
    }

    private void AddInterceptorRegistrars(
        BootstrapperInterceptorOptions options)
    {
        //if (!options.DisableValidationInterceptor)
        //{
        //    ValidationInterceptorRegistrar.Initialize(IocManager);
        //}

        //if (!options.DisableAuditingInterceptor)
        //{
        //    AuditingInterceptorRegistrar.Initialize(IocManager);
        //}

        //if (!options.DisableEntityHistoryInterceptor)
        //{
        //    EntityHistoryInterceptorRegistrar.Initialize(IocManager);
        //}

        //if (!options.DisableUnitOfWorkInterceptor)
        //{
        //    UnitOfWorkRegistrar.Initialize(IocManager);
        //}

        //if (!options.DisableAuthorizationInterceptor)
        //{
        //    AuthorizationInterceptorRegistrar.Initialize(IocManager);
        //}
    }

    /// <summary>
    /// Initializes the CloudCake system.
    /// </summary>
    public virtual void Initialize()
    {
        ResolveLogger();

        try
        {
            RegisterBootstrapper();
            IocManager.IocContainer.Install(new CoreInstaller());

            IocManager.Resolve<PluginManager>().PlugInSources.AddRange(PlugInSources);
            IocManager.Resolve<StartupConfiguration>().Initialize();

            _moduleManager = IocManager.Resolve<ModuleManager>();
            _moduleManager.Initialize(StartupModule);
            _moduleManager.StartModules();
        }
        catch (Exception ex)
        {
            _logger.Fatal(ex.ToString(), ex);
            throw;
        }
    }

    private void ResolveLogger()
    {
        if (IocManager.IsRegistered<ILoggerFactory>())
        {
            _logger = IocManager.Resolve<ILoggerFactory>().Create(typeof(Bootstrapper));
        }
    }

    private void RegisterBootstrapper()
    {
        if (!IocManager.IsRegistered<Bootstrapper>())
        {
            IocManager.IocContainer.Register(
                Component.For<Bootstrapper>().Instance(this)
            );
        }
    }

    /// <summary>
    /// Disposes the ABP system.
    /// </summary>
    public virtual void Dispose()
    {
        if (IsDisposed)
        {
            return;
        }

        IsDisposed = true;

        _moduleManager?.ShutdownModules();
    }
}