using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPM.Core.Security
{
    public class CiphertextInfo
    {
        /// <summary>
        /// 时间戳
        /// </summary>
        public long Timestamp { get; set; }

        /// <summary>
        /// 请求数据
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// 签名  MD5 32位小写
        /// </summary>
        public string Sign { get; set; }

        public string Encrypt()
        {
            Sign = MD5Utility.Get32LowerMD5(Data);
            string key = MD5Utility.Get32LowerMD5(Sign);
            string iv = MD5Utility.Get16LowerMD5(Timestamp.ToString());
            string result = AESUtility.AESEncrypt(Data, key, iv);

            return JsonConvert.SerializeObject(new
            {
                Data = result,
                Timestamp = Timestamp,
                Sign = Sign
            });
        }

        public string Dencrypt()
        {
            string key = MD5Utility.Get32LowerMD5(Sign);
            string iv = MD5Utility.Get16LowerMD5(Timestamp.ToString());
            string result = AESUtility.AESDecrypt(Data, key, iv);
            return result;
        }
    }
}
