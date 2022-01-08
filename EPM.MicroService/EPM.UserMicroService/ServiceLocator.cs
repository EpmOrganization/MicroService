using EPM.Core.ServiceBase;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace EPM.UserMicroService
{
    /// <summary>
    /// 服务定位器
    /// </summary>
    public static class ServiceLocator
    {
        public static IApplicationBuilder ApplicationBuilder { get; set; }
    }
}
