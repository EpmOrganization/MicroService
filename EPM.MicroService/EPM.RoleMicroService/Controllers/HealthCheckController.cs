using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EPM.RoleMicroService.Controllers
{
    /// <summary>
    /// 健康检查
    /// </summary>
    [Route("api/role/HealthCheck")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            //Console.WriteLine("UserService is ok");
            return Ok("RoleService is ok");
        }
    }
}
