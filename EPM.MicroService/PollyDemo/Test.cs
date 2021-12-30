using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PollyDemo
{
    public class Test
    {
        public List<string> services = new List<string> { "localhost:5010", "localhost:5020" };
        public int serviceIndex = 0;
        public HttpClient client = new HttpClient();

        public Task<string> HttpInvokeAsync()
        {
            if (serviceIndex >= services.Count)
            {
                serviceIndex = 0;
            }
            var service = services[serviceIndex++];
            Console.WriteLine(DateTime.Now.ToString() + "开始服务：" + service);
            return client.GetStringAsync("https://" + service + "/weatherforecast");
        }
    }
}
