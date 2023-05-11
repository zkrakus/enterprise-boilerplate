using CloudCake.Dependency;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using System.Linq.Expressions;
using CloudCake.Modules;

namespace CloudCake;

/// <summary>
/// Kernel (core) module of the CloudCake system.
/// No need to depend on this, it's automatically the first module always.
/// </summary>
public sealed class KernelModule : Module
{
    public override void PreInitialize()
    {
        IocManager?.AddConventionalRegistrar(new BasicConventionalRegistrar());

        IocManager?.Register<IScopedIocResolver, ScopedIocResolver>(DependencyLifeStyle.Transient);
        IocManager?.Register(typeof(IAmbientScopeProvider<>), typeof(DataContextAmbientScopeProvider<>), DependencyLifeStyle.Transient);

        //AddAuditingSelectors();
        //AddLocalizationSources();
        //AddSettingProviders();
        //AddUnitOfWorkFilters();
        //AddUnitOfWorkAuditFieldConfiguration();
        //ConfigureCaches();
        //AddIgnoredTypes();
        //AddMethodParameterValidators();
    }

    public override void Initialize()
    {
        foreach (var replaceAction in ((AbpStartupConfiguration)Configuration).ServiceReplaceActions.Values)
        {
            replaceAction();
        }

        //IocManager.IocContainer.Install(new EventBusInstaller(IocManager));

        //IocManager.Register(typeof(IOnlineClientManager<>), typeof(OnlineClientManager<>), DependencyLifeStyle.Singleton);
        //IocManager.Register(typeof(IOnlineClientStore<>), typeof(InMemoryOnlineClientStore<>), DependencyLifeStyle.Singleton);

        //IocManager.Register(typeof(EventTriggerAsyncBackgroundJob<>), DependencyLifeStyle.Transient);

        IocManager?.RegisterAssemblyByConvention(typeof(KernelModule).GetAssembly(),
            new ConventionalRegistrationConfig
            {
                InstallInstallers = false
            });

        RegisterInterceptors();
    }

    private void RegisterInterceptors()
    {
        //IocManager.Register(typeof(AbpAsyncDeterminationInterceptor<UnitOfWorkInterceptor>), DependencyLifeStyle.Transient);
        //IocManager.Register(typeof(AbpAsyncDeterminationInterceptor<AuditingInterceptor>), DependencyLifeStyle.Transient);
        //IocManager.Register(typeof(AbpAsyncDeterminationInterceptor<AuthorizationInterceptor>), DependencyLifeStyle.Transient);
        //IocManager.Register(typeof(AbpAsyncDeterminationInterceptor<ValidationInterceptor>), DependencyLifeStyle.Transient);
        //IocManager.Register(typeof(AbpAsyncDeterminationInterceptor<EntityHistoryInterceptor>), DependencyLifeStyle.Transient);
    }

    public override void PostInitialize()
    {
        //RegisterMissingComponents();

        //IocManager.Resolve<SettingDefinitionManager>().Initialize();
        //IocManager.Resolve<FeatureManager>().Initialize();
        //IocManager.Resolve<PermissionManager>().Initialize();
        //IocManager.Resolve<LocalizationManager>().Initialize();
        //IocManager.Resolve<NotificationDefinitionManager>().Initialize();
        //IocManager.Resolve<NavigationManager>().Initialize();
        //IocManager.Resolve<WebhookDefinitionManager>().Initialize();
        //IocManager.Resolve<DynamicEntityPropertyDefinitionManager>().Initialize();

        //if (Configuration.BackgroundJobs.IsJobExecutionEnabled)
        //{
        //    var workerManager = IocManager.Resolve<IBackgroundWorkerManager>();
        //    workerManager.Start();
        //    workerManager.Add(IocManager.Resolve<IBackgroundJobManager>());
        //}
    }

    public override void Shutdown()
    {
        //if (Configuration.BackgroundJobs.IsJobExecutionEnabled)
        //{
        //    IocManager.Resolve<IBackgroundWorkerManager>().StopAndWaitToStop();
        //}
    }

    //private void AddUnitOfWorkFilters()
    //{
    //    Configuration.UnitOfWork.RegisterFilter(AbpDataFilters.SoftDelete, true);
    //    Configuration.UnitOfWork.RegisterFilter(AbpDataFilters.MustHaveTenant, true);
    //    Configuration.UnitOfWork.RegisterFilter(AbpDataFilters.MayHaveTenant, true);
    //}

    //private void AddUnitOfWorkAuditFieldConfiguration()
    //{
    //    Configuration.UnitOfWork.RegisterAuditFieldConfiguration(AbpAuditFields.CreatorUserId, true);
    //    Configuration.UnitOfWork.RegisterAuditFieldConfiguration(AbpAuditFields.LastModifierUserId, true);
    //    Configuration.UnitOfWork.RegisterAuditFieldConfiguration(AbpAuditFields.LastModificationTime, true);
    //    Configuration.UnitOfWork.RegisterAuditFieldConfiguration(AbpAuditFields.DeleterUserId, true);
    //    Configuration.UnitOfWork.RegisterAuditFieldConfiguration(AbpAuditFields.DeletionTime, true);
    //}

    //private void AddSettingProviders()
    //{
    //    Configuration.Settings.Providers.Add<LocalizationSettingProvider>();
    //    Configuration.Settings.Providers.Add<EmailSettingProvider>();
    //    Configuration.Settings.Providers.Add<NotificationSettingProvider>();
    //    Configuration.Settings.Providers.Add<TimingSettingProvider>();
    //}

    //private void AddAuditingSelectors()
    //{
    //    Configuration.Auditing.Selectors.Add(
    //        new NamedTypeSelector(
    //            "Abp.ApplicationServices",
    //            type => typeof(IApplicationService).IsAssignableFrom(type)
    //        )
    //    );
    //}

    //private void AddLocalizationSources()
    //{
    //    Configuration.Localization.Sources.Add(
    //        new DictionaryBasedLocalizationSource(
    //            AbpConsts.LocalizationSourceName,
    //            new XmlEmbeddedFileLocalizationDictionaryProvider(
    //                typeof(KernelModule).GetAssembly(), "Abp.Localization.Sources.AbpXmlSource"
    //            )));
    //}

    //private void ConfigureCaches()
    //{
    //    Configuration.Caching.Configure(AbpCacheNames.ApplicationSettings, cache =>
    //    {
    //        cache.DefaultSlidingExpireTime = TimeSpan.FromHours(8);
    //    });

    //    Configuration.Caching.Configure(AbpCacheNames.TenantSettings, cache =>
    //    {
    //        cache.DefaultSlidingExpireTime = TimeSpan.FromMinutes(60);
    //    });

    //    Configuration.Caching.Configure(AbpCacheNames.UserSettings, cache =>
    //    {
    //        cache.DefaultSlidingExpireTime = TimeSpan.FromMinutes(20);
    //    });
    //}

    //private void AddIgnoredTypes()
    //{
    //    var commonIgnoredTypes = new[]
    //    {
    //            typeof(Stream),
    //            typeof(Expression)
    //        };

    //    foreach (var ignoredType in commonIgnoredTypes)
    //    {
    //        Configuration.Auditing.IgnoredTypes.AddIfNotContains(ignoredType);
    //        Configuration.Validation.IgnoredTypes.AddIfNotContains(ignoredType);
    //    }

    //    var validationIgnoredTypes = new[] { typeof(Type) };
    //    foreach (var ignoredType in validationIgnoredTypes)
    //    {
    //        Configuration.Validation.IgnoredTypes.AddIfNotContains(ignoredType);
    //    }
    //}

    //private void AddMethodParameterValidators()
    //{
    //    Configuration.Validation.Validators.Add<DataAnnotationsValidator>();
    //    Configuration.Validation.Validators.Add<ValidatableObjectValidator>();
    //    Configuration.Validation.Validators.Add<CustomValidator>();
    //}

    //private void RegisterMissingComponents()
    //{
    //    if (!IocManager.IsRegistered<IGuidGenerator>())
    //    {
    //        IocManager.IocContainer.Register(
    //            Component
    //                .For<IGuidGenerator, SequentialGuidGenerator>()
    //                .Instance(SequentialGuidGenerator.Instance)
    //        );
    //    }

    //    IocManager.RegisterIfNot<IUnitOfWork, NullUnitOfWork>(DependencyLifeStyle.Transient);
    //    IocManager.RegisterIfNot<IAuditingStore, SimpleLogAuditingStore>(DependencyLifeStyle.Singleton);
    //    IocManager.RegisterIfNot<IPermissionChecker, NullPermissionChecker>(DependencyLifeStyle.Singleton);
    //    IocManager.RegisterIfNot<INotificationStore, NullNotificationStore>(DependencyLifeStyle.Singleton);
    //    IocManager.RegisterIfNot<IUnitOfWorkFilterExecuter, NullUnitOfWorkFilterExecuter>(DependencyLifeStyle.Singleton);
    //    IocManager.RegisterIfNot<IClientInfoProvider, NullClientInfoProvider>(DependencyLifeStyle.Singleton);
    //    IocManager.RegisterIfNot<ITenantStore, NullTenantStore>(DependencyLifeStyle.Singleton);
    //    IocManager.RegisterIfNot<ITenantResolverCache, NullTenantResolverCache>(DependencyLifeStyle.Singleton);
    //    IocManager.RegisterIfNot<IEntityHistoryStore, NullEntityHistoryStore>(DependencyLifeStyle.Singleton);
    //    IocManager.RegisterIfNot<ICachedUniqueKeyPerUser, CachedUniqueKeyPerUser>(DependencyLifeStyle.Transient);

    //    if (Configuration.BackgroundJobs.IsJobExecutionEnabled)
    //    {
    //        IocManager.RegisterIfNot<IBackgroundJobStore, InMemoryBackgroundJobStore>(DependencyLifeStyle.Singleton);
    //    }
    //    else
    //    {
    //        IocManager.RegisterIfNot<IBackgroundJobStore, NullBackgroundJobStore>(DependencyLifeStyle.Singleton);
    //    }
    //}
}