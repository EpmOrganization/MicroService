using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPM.Core.HttpClientConsul
{
    /// <summary>
    /// 封装consul服务发现
    /// </summary>
    public interface IConsulHttpClient
    {
        Task<T> GetAsync<T>(string serviceName, string serviceLink);

        Task<T> GetByGatewayAsync<T>(string url);
    }
}
