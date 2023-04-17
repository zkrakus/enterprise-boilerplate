using CloudCake.Dependency;
using CloudCake.Plugins;

namespace CloudCake;

public class BootstrapperOptions
{
    /// <summary>
    /// Used to disable all interceptors added by ABP.
    /// </summary>
    public BootstrapperInterceptorOptions InterceptorOptions { get; set; }

    /// <summary>
    /// IIocManager that is used to bootstrap the ABP system. If set to null, uses global <see cref="IocManager.Instance"/>
    /// </summary>
    public IIocManager IocManager { get; set; }

    /// <summary>
    /// List of plugin sources.
    /// </summary>
    public PlugInSourceList PlugInSources { get; }

    public BootstrapperOptions()
    {
        IocManager = Dependency.IocManager.Instance;
        PlugInSources = new PlugInSourceList();
        InterceptorOptions = new BootstrapperInterceptorOptions();
    }
}

public class BootstrapperInterceptorOptions
{
    public bool DisableValidationInterceptor { get; set; }

    public bool DisableAuditingInterceptor { get; set; }

    public bool DisableEntityHistoryInterceptor { get; set; }

    public bool DisableUnitOfWorkInterceptor { get; set; }

    public bool DisableAuthorizationInterceptor { get; set; }
}