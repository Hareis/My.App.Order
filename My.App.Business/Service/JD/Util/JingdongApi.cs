using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Security.Cryptography;

namespace My.App.Business.Service.JD.Util
{
    /// <summary>
    /// 京东API
    /// </summary>
    internal class JingdongApi
    {
        /// <summary>
        /// 返回Body
        /// </summary>
        public String responseContent;

        /// <summary>
        /// AppKey
        /// </summary>
        private string AppKey { get; set; }

        /// <summary>
        /// AppSecret
        /// </summary>
        private string AppSecret { get; set; }

        /// <summary>
        /// 授权
        /// </summary>
        private string Token { get; set; }

        /// <summary>
        /// 请求地址
        /// </summary>
        private string url { get; set; }

        /// <summary>
        /// API协议版本，可选值:1.0。
        /// </summary>
        private string v { get; set; }

        /// <summary>
        /// API输入参数签名结果
        /// </summary>
        private string sign { get; set; }

        /// <summary>
        /// 时间戳
        /// <para>格式为yyyy-MM-dd HH:mm:ss</para>
        /// <para>京东API服务端允许客户端请求时间误差为6分钟</para>
        /// </summary>
        private string timestamp { get; set; }

        /// <summary>
        /// API方法名称
        /// </summary>
        private string method { get; set; }

        /// <summary>
        /// 参数列表
        /// </summary>
        private IDictionary<string, string> ListParam;

        /// <summary>
        /// 京东API有参构造函数
        /// </summary>
        /// <param name="vender_id">AppKey</param>
        /// <param name="vender_key">AppSecret</param>
        /// <param name="Token">授权</param>
        /// <param name="url">请求地址</param>
        /// <param name="v">API协议版本，可选值:1.0</param>
        public JingdongApi(string AppKey, string AppSecret, string Token, string url, string v) {
            this.url = url;
            this.AppKey = AppKey;
            this.AppSecret = AppSecret;
            this.Token = Token;
            this.v = v;
        }

        /// <summary>
        /// 加载参数列表
        /// </summary>
        /// <param name="ParamList">参数列表</param>
        /// <returns>请求url</returns>
        private string LoadParam(ApiDictionary ParamList) {
            ListParam = new Dictionary<string, string>();

            //先加载系统级参数
            ListParam.Add("app_key", this.AppKey);
            ListParam.Add("v", this.v);
            ListParam.Add("timestamp", this.timestamp);
            ListParam.Add("method", this.method);
            ListParam.Add("access_token", this.Token);

            //加载应用级参数
            ListParam.Add("360buy_param_json", GetJsonStr(ParamList));

            //签名
            this.sign = SignTopRequest(ListParam, this.AppSecret, true);

            //拼接Post提交数据
            List<string> Data = new List<string>();
            Data.Add("sign=" + this.sign);

            foreach (KeyValuePair<string, string> kvp in ListParam) {
                Data.Add(kvp.Key + "=" + System.Web.HttpUtility.UrlEncode(kvp.Value));
            }

            return String.Join("&", Data);
        }

        /// <summary>
        /// 调用京东API
        /// </summary>
        /// <param name="method">方法名称</param>
        /// <param name="ParamList">参数列表</param>
        public void InvokeApi(string method, ApiDictionary ParamList) { 
            this.timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            this.method = method;

            string PostData = LoadParam(ParamList);

            this.Request(this.url, PostData);
        }

        /// <summary>
        /// 请求API数据
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="PostData">post提交数据</param>
        /// <param name="encoding">编码(默认UTF-8)</param>
        private void Request(string url, string PostData, Encoding encoding = null) {
            try {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "post";
                request.Accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/xaml+xml, application/x-ms-xbap, application/x-ms-application, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
                request.ContentType = "application/x-www-form-urlencoded";
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET4.0C; .NET4.0E; .NET CLR 2.0.50727; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; InfoPath.3)";
                request.KeepAlive = false;

                if (encoding == null) {
                    encoding = Encoding.UTF8;
                }

                if (!String.IsNullOrEmpty(PostData)) {
                    byte[] buffer = encoding.GetBytes(PostData);
                    request.ContentLength = buffer.Length;
                    request.GetRequestStream().Write(buffer, 0, buffer.Length);
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                using (StreamReader reader = new StreamReader(response.GetResponseStream(), encoding)) {
                    this.responseContent = reader.ReadToEnd().Replace("/r/n", "").Replace("/t", "");
                }
            } catch { }
        }

        /// <summary>
        /// 将参数列表转换成Json格式
        /// </summary>
        /// <param name="ParamList">参数列表</param>
        /// <returns></returns>
        private string GetJsonStr(ApiDictionary ParamList) {
            List<string> param = new List<string>();
            foreach (KeyValuePair<string, string> kvp in ParamList) {
                param.Add("\"" + kvp.Key + "\":\"" + kvp.Value + "\"");
            }
            return "{" + String.Join(",", param) + "}";
        }

        /// <summary>
        /// 京东签名算法
        /// </summary>
        /// <param name="parameters">所有字符型的请求参数</param>
        /// <param name="secret">签名密钥</param>
        /// <param name="qhs">是否前后都加密钥进行签名</param>
        /// <returns>签名</returns>
        private string SignTopRequest(IDictionary<string, string> parameters, string secret, bool qhs) {
            // 第一步：把字典按Key的字母顺序排序
            IDictionary<string, string> sortedParams = new SortedDictionary<string, string>(parameters);
            IEnumerator<KeyValuePair<string, string>> dem = sortedParams.GetEnumerator();

            // 第二步：把所有参数名和参数值串在一起
            StringBuilder query = new StringBuilder(secret);
            while (dem.MoveNext())  {
                string key = dem.Current.Key;
                string value = dem.Current.Value;
                if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value)) {
                    query.Append(key).Append(value);
                }
            }

            if (qhs) {
                query.Append(secret);
            }

            // 第三步：使用MD5加密
            MD5 md5 = MD5.Create();
            byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(query.ToString()));

            // 第四步：把二进制转化为大写的十六进制
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++) {
                string hex = bytes[i].ToString("X");
                if (hex.Length == 1) {
                    result.Append("0");
                }
                result.Append(hex);
            }

            return result.ToString();
        }
    }
}
