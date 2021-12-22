using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

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
#if DEBUG
            Console.WriteLine("DepartService is ok");
#endif
            return Ok("DepartService is ok");
        }
    }
}
