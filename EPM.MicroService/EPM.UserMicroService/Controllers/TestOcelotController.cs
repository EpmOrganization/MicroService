
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EPM.UserMicroService.Controllers
{
    /// <summary>
    /// 无实际作用，只是用来测试Ocelot网关
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TestOcelotController : ControllerBase
    {
        public ActionResult Get()
        {
            // 获取当前服务地址和端口
            var features = ServiceLocator.ApplicationBuilder.Properties["server.Features"] as FeatureCollection;
            var address = features.Get<IServerAddressesFeature>().Addresses.First();
            return Ok($"TestOcelot From Url:{address}");
        }
    }
}
