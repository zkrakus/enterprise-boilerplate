namespace CloudCake;

public class BootstrapperInterceptorOptions
{
    public bool DisableValidationInterceptor { get; set; }

    public bool DisableAuditingInterceptor { get; set; }

    public bool DisableEntityHistoryInterceptor { get; set; }

    public bool DisableUnitOfWorkInterceptor { get; set; }

    public bool DisableAuthorizationInterceptor { get; set; }
}