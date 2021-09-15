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
        /// ������consul����ע��
        /// </summary>
        private ServiceRegistryConfig ServiceRegistryConfig { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ServiceRegistryConfig = new ServiceRegistryConfig();

            // ��ȡ�����ļ����ݣ����󶨵���������
            Configuration.GetSection("ConsulRegistry").Bind(ServiceRegistryConfig);

            #region ���ݿ�����
            string connectionString = Configuration.GetSection("ConnectionString").GetSection("DbConnection").Value;
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
            #endregion

            #region ע�����
            services.AddScoped<IUserService,UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            #endregion

            #region ���consulע�����ģ���������
            // ���consulע�����ģ���������
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

            #region ʹ��consul����ע��

            // 1������consul�ͻ�������
            var consulClient = new ConsulClient(configuration =>
            {
                //1.1 �����ͻ��˺ͷ��������
                configuration.Address = new Uri(ServiceRegistryConfig.ConsulRegistryAddress);
            });

            // 2������consul����ע�����
            var registration = new AgentServiceRegistration()
            {
                // ��ţ�ID��Ψһ�ģ���Ⱥ��ʱ����ݱ��ȥ�ҵ����Ψһ�ķ���
                ID = Guid.NewGuid().ToString(),
                // �������֣�����Ⱥ��ʱ����ݷ������ֻ�ȡ���еķ����ַ����
                Name = ServiceRegistryConfig.Name,
                // �����ַ
                Address = ServiceRegistryConfig.Address,
                // ����˿�
                Port = ServiceRegistryConfig.Port,
                Tags = null,
                // �������
                Check = new AgentServiceCheck
                {
                    // 3.1��consul������鳬ʱ��
                    Timeout = TimeSpan.FromSeconds(10),
                    // 3.2������ֹͣ5���ע������
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
                    // 3.3��consul��������ַ
                    HTTP =$"{ServiceRegistryConfig.Address}:{ServiceRegistryConfig.Port}{ServiceRegistryConfig.HealthCheckAddress}" ,
                    // 3.4 consul���������ʱ��
                    Interval = TimeSpan.FromSeconds(10),
                }
            };

            // 3��ע�����
            consulClient.Agent.ServiceRegister(registration).Wait();

            // 4����ȡӦ�ó�����������
            var lifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();

            // 5���������ر�ʱע������
            lifetime.ApplicationStopping.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(registration.ID).Wait();//����ֹͣʱȡ��ע��
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
