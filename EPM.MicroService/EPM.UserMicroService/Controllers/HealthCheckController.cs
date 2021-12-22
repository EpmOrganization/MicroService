using Microsoft.AspNetCore.Mvc;
using System;

namespace EPM.UserMicroService.Controllers
{
    /// <summary>
    /// 健康检查
    /// </summary>
    [Route("api/user/HealthCheck")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
             //Console.WriteLine("UserService is ok");
             return Ok("UserService is ok");
        }
    }
}
