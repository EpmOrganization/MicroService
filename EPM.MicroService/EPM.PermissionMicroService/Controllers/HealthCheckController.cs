using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EPM.PermissionMicroService.Controllers
{
    /// <summary>
    /// 健康检查
    /// </summary>
    [Route("api/permission/HealthCheck")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            //Console.WriteLine("UserService is ok");
            return Ok("PermissionService is ok");
        }
    }
}
