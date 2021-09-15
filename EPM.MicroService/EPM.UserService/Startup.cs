using Consul;
using EPM.Core.Extentions.MicroServiceExtentions;
using EPM.Model.ConfigModel;
using EPM.UserMicroService.Context;
using EPM.UserMicroService.Repositories;
using EPM.UserMicroService.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.UserMicroService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// 配置向consul进行注册
        /// </summary>
        private ServiceRegistryConfig ServiceRegistryConfig { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ServiceRegistryConfig = new ServiceRegistryConfig();

            // 读取配置文件内容，并绑定到配置类上
            Configuration.GetSection("ConsulRegistry").Bind(ServiceRegistryConfig);

            #region 数据库连接
            string connectionString = Configuration.GetSection("ConnectionString").GetSection("DbConnection").Value;
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
            #endregion

            #region 注入服务
            services.AddScoped<IUserService,UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            #endregion

            #region 添加consul注册中心，加载配置
            // 添加consul注册中心，加载配置
            //services.AddConsulRegistry(Configuration); 
            #endregion

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EPM.UserService", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EPM.UserService v1"));
            }

            #region 使用consul服务注册

            // 1、创建consul客户端连接
            var consulClient = new ConsulClient(configuration =>
            {
                //1.1 建立客户端和服务端连接
                configuration.Address = new Uri(ServiceRegistryConfig.ConsulRegistryAddress);
            });

            // 2、创建consul服务注册对象
            var registration = new AgentServiceRegistration()
            {
                // 编号，ID是唯一的，集群的时候根据编号去找到这个唯一的服务
                ID = Guid.NewGuid().ToString(),
                // 服务名字，做集群的时候根据服务名字获取所有的服务地址集合
                Name = ServiceRegistryConfig.Name,
                // 服务地址
                Address = ServiceRegistryConfig.Address,
                // 服务端口
                Port = ServiceRegistryConfig.Port,
                Tags = null,
                // 健康检查
                Check = new AgentServiceCheck
                {
                    // 3.1、consul健康检查超时间
                    Timeout = TimeSpan.FromSeconds(10),
                    // 3.2、服务停止5秒后注销服务
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
                    // 3.3、consul健康检查地址
                    HTTP =$"{ServiceRegistryConfig.Address}:{ServiceRegistryConfig.Port}{ServiceRegistryConfig.HealthCheckAddress}" ,
                    // 3.4 consul健康检查间隔时间
                    Interval = TimeSpan.FromSeconds(10),
                }
            };

            // 3、注册服务
            consulClient.Agent.ServiceRegister(registration).Wait();

            // 4、获取应用程序生命周期
            var lifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();

            // 5、服务器关闭时注销服务
            lifetime.ApplicationStopping.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(registration.ID).Wait();//服务停止时取消注册
            });
            #endregion

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
