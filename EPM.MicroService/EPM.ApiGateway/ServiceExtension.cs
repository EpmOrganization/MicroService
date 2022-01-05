using EPM.ApiGateway.OcelotExtension;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;

namespace EPM.ApiGateway
{
    public static class ServiceExtension
    {
        public static IOcelotBuilder AddCustomLoadBalancer(this IOcelotBuilder builder)
        {
            builder.Services.AddSingleton<CustomRandomLoadBalancer>();
            return builder;
        }
    }
}
