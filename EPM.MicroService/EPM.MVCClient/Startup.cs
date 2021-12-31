using EPM.Core.Discovery;
using EPM.Core.HttpClientConsul.Extentions;
using EPM.Core.HttpClientPolly;
using EPM.Core.ServiceBase;
using EPM.Model.ConfigModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace EPM.MVCClient
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // 1、自定义异常处理(用缓存处理)
            var fallbackResponse = new HttpResponseMessage
            {
                Content = new StringContent("系统正繁忙，请稍后重试"),// 内容，自定义内容
                StatusCode = HttpStatusCode.GatewayTimeout // 504
            };

            // 使用HttpClientFactory
            //services.AddHttpClient();

            #region 服务发现
            //services.AddScoped<IServiceDiscovery, ConsulServiceDiscovery>();
            services.Configure<ServiceDiscoveryConfig>(Configuration.GetSection("ConsulDiscovery"));
            //services.AddHttpClientConsul(Configuration);
            #endregion


            services.Configure<UserServiceConfig>(Configuration.GetSection("UserServiceConfig"));
            services.Configure<DeptServiceConfig>(Configuration.GetSection("DeptServiceConfig"));

            
            PollyConfig pollyConfig= new PollyConfig();
            Configuration.GetSection("PollyConfig").Bind(pollyConfig);

            services.AddPollyHttpClient("mrico", options =>
            {
                options.TimeoutTime = pollyConfig.TimeoutTime; // 1、超时时间
                options.RetryCount = pollyConfig.RetryCount;// 2、重试次数
                options.CircuitBreakerOpenFallCount = pollyConfig.CircuitBreakerOpenFallCount;// 3、熔断器开启(多少次失败开启)
                options.CircuitBreakerDownTime = pollyConfig.CircuitBreakerDownTime;// 4、熔断器开启时间
                options.httpResponseMessage = fallbackResponse;// 5、降级处理
            }).AddHttpClientConsul(Configuration) ;

            // 写在最后面,否则会导致写在这行代码后面的注入获取不到
            ServiceFactory.Services = services;
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
