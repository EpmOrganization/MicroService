using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EPM.DepartmentMicroService.Controllers
{
    /// <summary>
    /// 健康检查
    /// </summary>
    [Route("api/department/HealthCheck")]
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
