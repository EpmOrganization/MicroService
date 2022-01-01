using EPM.Core.ServiceBase;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace EPM.UserMicroService
{
    public class ServiceLocator
    {
        public static IApplicationBuilder ApplicationBuilder()
        {
           return ServiceFactory.ServiceProvider.GetService<IApplicationBuilder>();
        }
    }
}
