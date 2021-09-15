using Microsoft.AspNetCore.Mvc;

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
            System.Console.WriteLine("UserService is ok");
             return Ok("UserService is ok");
        }
    }
}
