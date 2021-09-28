using Microsoft.Extensions.DependencyInjection;
using System;

namespace EPM.Core.ServiceBase
{
    public class ServiceFactory
    {
        static IServiceCollection _services;
        public static IServiceCollection Services
        {
            get { return _services; }
            set
            {
                _services = value;
                ServiceProvider = _services.BuildServiceProvider();
            }
        }

        public static IServiceProvider ServiceProvider { get; private set; }
    }
}
