using Consul;
using EPM.DepartmentMicroService;
using EPM.DepartmentMicroService.Context;
using EPM.DepartmentMicroService.Repositories;
using EPM.DepartmentMicroService.Service;
using EPM.Model.ConfigModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var ServiceRegistryConfig = new ServiceRegistryConfig();


// 读取配置文件内容，并绑定到配置类上
builder.Configuration.GetSection("ConsulRegistry").Bind(ServiceRegistryConfig);

#region 数据库连接
string connectionString = builder.Configuration.GetSection("ConnectionString").GetSection("DbConnection").Value;


builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});
#endregion

#region 注入服务
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
#endregion

#region 添加consul注册中心，加载配置
// 添加consul注册中心，加载配置
//services.AddConsulRegistry(Configuration); 
#endregion


builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "EPM.DepartmentService", Version = "v1" });
});

var app = builder.Build();



// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EPM.DepartmentService v1"));
app.UseHttpsRedirection();

#region 使用consul服务注册

var t = app.Urls.ToList();

//builder.Build().MapDynamicPageRoute]]
//// 获取服务地址
//var features = app.["server.Features"] as FeatureCollection;
//var address = features.Get<IServerAddressesFeature>().Addresses.First();
//var uri = new Uri(address);

//// 获取Scheme
//Dictionary<string, string> dict = new Dictionary<string, string>();
//dict.Add("Scheme", uri.Scheme);




//var list = app.Urls.ToList();

//// 1、创建consul客户端连接
//var consulClient = new ConsulClient(configuration =>
//{
//    //1.1 建立客户端和服务端连接
//    configuration.Address = new Uri(ServiceRegistryConfig.ConsulRegistryAddress);
//});

//// 2、创建consul服务注册对象
//var registration = new AgentServiceRegistration()
//{
//    // 编号，ID是唯一的，集群的时候根据编号去找到这个唯一的服务
//    ID = Guid.NewGuid().ToString(),
//    // 服务名字，做集群的时候根据服务名字获取所有的服务地址集合
//    Name = ServiceRegistryConfig.Name,
//    // 服务地址
//    Address = ServiceRegistryConfig.Address,
//    // 服务端口
//    Port = ServiceRegistryConfig.Port,
//    Tags = null,
//    // 健康检查
//    Check = new AgentServiceCheck
//    {
//        // 3.1、consul健康检查超时间
//        Timeout = TimeSpan.FromSeconds(10),
//        // 3.2、服务停止5秒后注销服务
//        DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
//        // 3.3、consul健康检查地址
//        HTTP = $"{ServiceRegistryConfig.Address}:{ServiceRegistryConfig.Port}{ServiceRegistryConfig.HealthCheckAddress}",
//        // 3.4 consul健康检查间隔时间
//        Interval = TimeSpan.FromSeconds(10),
//    }
//};

//// 3、注册服务
//consulClient.Agent.ServiceRegister(registration).Wait();

//// 4、获取应用程序生命周期
//var lifetime = app.Services.GetRequiredService<IHostApplicationLifetime>();

//// 5、服务器关闭时注销服务
//lifetime.ApplicationStopping.Register(() =>
//{
//    consulClient.Agent.ServiceDeregister(registration.ID).Wait();//服务停止时取消注册
//});

app.UseConsul(builder.Configuration);


#endregion


app.UseAuthorization();

app.MapControllers();

app.Run();
