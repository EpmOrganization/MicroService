using Consul;
using EPM.Model.ApiModel;
using EPM.Model.Dto.Response.DeptResponse;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace EPM.MVCClient.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DepartmentController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ActionResult> Index()
        {

            #region 普通调用方式，不利于调用集群
            //List<DeptResponseDto> list = new List<DeptResponseDto>();
            //HttpClient httpClient = _httpClientFactory.CreateClient();

            //// 这里调用user微服务的url是写死的，不利于调用集群
            //HttpResponseMessage response = await httpClient.GetAsync("https://localhost:7021/api/department");

            //// 3.1json转换成对象
            //if (response.StatusCode == HttpStatusCode.OK)
            //{
            //    string json = await response.Content.ReadAsStringAsync();

            //    list = JsonConvert.DeserializeObject<ApiResponseWithData<List<DeptResponseDto>>>(json).Data;
            //}
            #endregion

            #region 使用服务发现
            List<DeptResponseDto> list = new List<DeptResponseDto>();
            // 1、创建consul客户端连接
            var consulClient = new ConsulClient(configuration =>
            {
                //1.1 建立客户端和服务端连接
                configuration.Address = new Uri("http://127.0.0.1:8500");
            });
            // 2、consul查询服务,根据具体的服务名称查询
            var queryResult = await consulClient.Catalog.Service("DepartmentService");

            // 3、将服务进行拼接
            var listServices = new List<string>();
            foreach (var service in queryResult.Response)
            {
                listServices.Add(service.ServiceAddress + ":" + service.ServicePort);
            }
            HttpClient httpClient = _httpClientFactory.CreateClient();
            // 获取一个随机数
            int index = new Random().Next(0, listServices.Count);
            // 这里为了测试，取第一个地址，真实项目中应该根据负载均衡去获取地址
            HttpResponseMessage response = await httpClient.GetAsync($"https://{listServices[index]}/api/department");


            // 3.1json转换成对象
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string json = await response.Content.ReadAsStringAsync();

                list = JsonConvert.DeserializeObject<ApiResponseWithData<List<DeptResponseDto>>>(json).Data;
            }
            #endregion
            ViewData["url"] = listServices[index];

            //ViewData["url"] = "https://localhost:7021";
            return View(list);
        }
    }
}
