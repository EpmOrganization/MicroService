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


// ��ȡ�����ļ����ݣ����󶨵���������
builder.Configuration.GetSection("ConsulRegistry").Bind(ServiceRegistryConfig);

#region ���ݿ�����
string connectionString = builder.Configuration.GetSection("ConnectionString").GetSection("DbConnection").Value;


builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});
#endregion

#region ע�����
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
#endregion

#region ���consulע�����ģ���������
// ���consulע�����ģ���������
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

#region ʹ��consul����ע��

var t = app.Urls.ToList();

//builder.Build().MapDynamicPageRoute]]
//// ��ȡ�����ַ
//var features = app.["server.Features"] as FeatureCollection;
//var address = features.Get<IServerAddressesFeature>().Addresses.First();
//var uri = new Uri(address);

//// ��ȡScheme
//Dictionary<string, string> dict = new Dictionary<string, string>();
//dict.Add("Scheme", uri.Scheme);




//var list = app.Urls.ToList();

//// 1������consul�ͻ�������
//var consulClient = new ConsulClient(configuration =>
//{
//    //1.1 �����ͻ��˺ͷ��������
//    configuration.Address = new Uri(ServiceRegistryConfig.ConsulRegistryAddress);
//});

//// 2������consul����ע�����
//var registration = new AgentServiceRegistration()
//{
//    // ��ţ�ID��Ψһ�ģ���Ⱥ��ʱ����ݱ��ȥ�ҵ����Ψһ�ķ���
//    ID = Guid.NewGuid().ToString(),
//    // �������֣�����Ⱥ��ʱ����ݷ������ֻ�ȡ���еķ����ַ����
//    Name = ServiceRegistryConfig.Name,
//    // �����ַ
//    Address = ServiceRegistryConfig.Address,
//    // ����˿�
//    Port = ServiceRegistryConfig.Port,
//    Tags = null,
//    // �������
//    Check = new AgentServiceCheck
//    {
//        // 3.1��consul������鳬ʱ��
//        Timeout = TimeSpan.FromSeconds(10),
//        // 3.2������ֹͣ5���ע������
//        DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
//        // 3.3��consul��������ַ
//        HTTP = $"{ServiceRegistryConfig.Address}:{ServiceRegistryConfig.Port}{ServiceRegistryConfig.HealthCheckAddress}",
//        // 3.4 consul���������ʱ��
//        Interval = TimeSpan.FromSeconds(10),
//    }
//};

//// 3��ע�����
//consulClient.Agent.ServiceRegister(registration).Wait();

//// 4����ȡӦ�ó�����������
//var lifetime = app.Services.GetRequiredService<IHostApplicationLifetime>();

//// 5���������ر�ʱע������
//lifetime.ApplicationStopping.Register(() =>
//{
//    consulClient.Agent.ServiceDeregister(registration.ID).Wait();//����ֹͣʱȡ��ע��
//});

app.UseConsul(builder.Configuration);


#endregion


app.UseAuthorization();

app.MapControllers();

app.Run();
