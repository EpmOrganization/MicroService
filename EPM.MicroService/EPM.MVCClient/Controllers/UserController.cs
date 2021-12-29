using Consul;
using EPM.Core.Discovery;
using EPM.Core.HttpClientConsul;
using EPM.Core.ServiceBase;
using EPM.Model.ApiModel;
using EPM.Model.ConfigModel;
using EPM.Model.DbModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace EPM.MVCClient.Controllers
{
    public class UserController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConsulHttpClient _consulHttpClient;

        public UserController(IHttpClientFactory httpClientFactory, IConsulHttpClient consulHttpClient)
        {
            _httpClientFactory = httpClientFactory;
            _consulHttpClient = consulHttpClient;
        }

        // GET: UserController1
        public async Task<ActionResult> Index()
        {

            #region 普通调用方式，不利于调用集群
            //List<User> list = new List<User>();
            //HttpClient httpClient = _httpClientFactory.CreateClient();

            //// 这里调用user微服务的url是写死的，不利于调用集群
            //HttpResponseMessage response = await httpClient.GetAsync("https://localhost:6011/api/user");

            //// 3.1json转换成对象
            //if (response.StatusCode == HttpStatusCode.OK)
            //{
            //    string json = await response.Content.ReadAsStringAsync();

            //    list = JsonConvert.DeserializeObject<ApiResponseWithData<List<User>>>(json).Data;
            //}
            #endregion

            #region 使用服务发现 代码重复
            //List<User> list = new List<User>();
            //// 1、创建consul客户端连接
            //var consulClient = new ConsulClient(configuration =>
            //{
            //    //1.1 建立客户端和服务端连接
            //    configuration.Address = new Uri("http://127.0.0.1:8500");
            //});
            //// 2、consul查询服务,根据具体的服务名称查询
            //var queryResult = await consulClient.Catalog.Service("UserService");

            //// 3、将服务进行拼接
            //var listServices = new List<string>();
            //foreach (var service in queryResult.Response)
            //{
            //    listServices.Add(service.ServiceMeta["Scheme"]+"://"+ service.ServiceAddress + ":" + service.ServicePort);
            //}
            //HttpClient httpClient = _httpClientFactory.CreateClient();
            //// 获取一个随机数
            //int index = new Random().Next(0, listServices.Count);
            //// 这里为了测试，取第一个地址，真实项目中应该根据负载均衡去获取地址
            //HttpResponseMessage response = await httpClient.GetAsync($"{listServices[index]}/api/user");
            //// 3.1json转换成对象
            //if (response.StatusCode == HttpStatusCode.OK)
            //{
            //    string json = await response.Content.ReadAsStringAsync();

            //    list = JsonConvert.DeserializeObject<ApiResponseWithData<List<User>>>(json).Data;
            //}
            #endregion

            #region 封装consul服务发现
            // 获取配置
            var userServiceConfig = ServiceFactory.ServiceProvider.GetService<IOptions<UserServiceConfig>>().Value;
            List<User> list = new List<User>();
            var result =await _consulHttpClient.GetAsync<ApiResponseWithData<List<User>>>(userServiceConfig.ServiceName,userServiceConfig.GetUri);
            if(result!=null)
            {
                list = result.Data;
            }

            #endregion

            //ViewData["url"] = listServices[index];
            return View(list);
        }

        // GET: UserController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        /// <summary>
        /// 显示Add对应的视图
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// Form表单提交以后执行的Add方法
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(User user)
        {
            user.ID = Guid.NewGuid();
            user.CreateUser = user.UpdateUser = "admin";
            return Ok("添加成功");
        }

        // POST: UserController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
