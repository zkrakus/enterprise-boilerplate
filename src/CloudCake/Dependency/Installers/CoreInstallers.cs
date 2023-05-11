﻿using CloudCake.Modules;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace CloudCake.Dependency.Installers;

internal class CoreInstaller : IWindsorInstaller
{
    public void Install(IWindsorContainer container, IConfigurationStore store)
    {
        container.Register(
            //Component.For<IUnitOfWorkDefaultOptions, UnitOfWorkDefaultOptions>().ImplementedBy<UnitOfWorkDefaultOptions>().LifestyleSingleton(),
            //Component.For<IAbpValidationDefaultOptions, AbpValidationDefaultOptions>().ImplementedBy<AbpValidationDefaultOptions>().LifestyleSingleton(),
            //Component.For<IAbpAuditingDefaultOptions, AbpAuditingDefaultOptions>().ImplementedBy<AbpAuditingDefaultOptions>().LifestyleSingleton(),
            //Component.For<INavigationConfiguration, NavigationConfiguration>().ImplementedBy<NavigationConfiguration>().LifestyleSingleton(),
            //Component.For<ILocalizationConfiguration, LocalizationConfiguration>().ImplementedBy<LocalizationConfiguration>().LifestyleSingleton(),
            //Component.For<IAuthorizationConfiguration, AuthorizationConfiguration>().ImplementedBy<AuthorizationConfiguration>().LifestyleSingleton(),
            //Component.For<IValidationConfiguration, ValidationConfiguration>().ImplementedBy<ValidationConfiguration>().LifestyleSingleton(),
            //Component.For<IFeatureConfiguration, FeatureConfiguration>().ImplementedBy<FeatureConfiguration>().LifestyleSingleton(),
            //Component.For<ISettingsConfiguration, SettingsConfiguration>().ImplementedBy<SettingsConfiguration>().LifestyleSingleton(),
            //Component.For<IModuleConfigurations, ModuleConfigurations>().ImplementedBy<ModuleConfigurations>().LifestyleSingleton(),
            //Component.For<IEventBusConfiguration, EventBusConfiguration>().ImplementedBy<EventBusConfiguration>().LifestyleSingleton(),
            //Component.For<IMultiTenancyConfig, MultiTenancyConfig>().ImplementedBy<MultiTenancyConfig>().LifestyleSingleton(),
            //Component.For<ICachingConfiguration, CachingConfiguration>().ImplementedBy<CachingConfiguration>().LifestyleSingleton(),
            //Component.For<IAuditingConfiguration, AuditingConfiguration>().ImplementedBy<AuditingConfiguration>().LifestyleSingleton(),
            //Component.For<IBackgroundJobConfiguration, BackgroundJobConfiguration>().ImplementedBy<BackgroundJobConfiguration>().LifestyleSingleton(),
            //Component.For<INotificationConfiguration, NotificationConfiguration>().ImplementedBy<NotificationConfiguration>().LifestyleSingleton(),
            //Component.For<IEmbeddedResourcesConfiguration, EmbeddedResourcesConfiguration>().ImplementedBy<EmbeddedResourcesConfiguration>().LifestyleSingleton(),
            //Component.For<IAbpStartupConfiguration, AbpStartupConfiguration>().ImplementedBy<AbpStartupConfiguration>().LifestyleSingleton(),
            //Component.For<IEntityHistoryConfiguration, EntityHistoryConfiguration>().ImplementedBy<EntityHistoryConfiguration>().LifestyleSingleton(),
            //Component.For<ITypeFinder, TypeFinder>().ImplementedBy<TypeFinder>().LifestyleSingleton(),
            //Component.For<IPluginManager, PluginManager>().ImplementedBy<PluginManager>().LifestyleSingleton(),
            Component.For<IModuleManager, ModuleManager>().ImplementedBy<ModuleManager>().LifestyleSingleton()
            //Component.For<IAssemblyFinder, AbpAssemblyFinder>().ImplementedBy<AbpAssemblyFinder>().LifestyleSingleton(),
            //Component.For<ILocalizationManager, LocalizationManager>().ImplementedBy<LocalizationManager>().LifestyleSingleton(),
            //Component.For<IWebhooksConfiguration, WebhooksConfiguration>().ImplementedBy<WebhooksConfiguration>().LifestyleSingleton(),
            //Component.For<IDynamicEntityPropertyDefinitionContext, DynamicEntityPropertyDefinitionContext>().ImplementedBy<DynamicEntityPropertyDefinitionContext>().LifestyleTransient(),
            //Component.For<IDynamicEntityPropertyConfiguration, DynamicEntityPropertyConfiguration>().ImplementedBy<DynamicEntityPropertyConfiguration>().LifestyleSingleton()
        );
    }
}