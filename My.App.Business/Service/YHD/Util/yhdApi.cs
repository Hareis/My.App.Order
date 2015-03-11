using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Security.Cryptography;

namespace My.App.Business.Service.YHD.Util
{
    /// <summary>
    /// 一号店API
    /// </summary>
    internal class yhdApi
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
        /// SessionKey
        /// </summary>
        private string SessionKey { get; set; }

        /// <summary>
        /// 请求地址
        /// </summary>
        private string url { get; set; }

        /// <summary>
        /// API版本
        /// </summary>
        private string ver { get; set; }

        /// <summary>
        /// API方法名称
        /// </summary>
        private string method { get; set; }

        /// <summary>
        /// API输入参数签名结果
        /// </summary>
        private string sign { get; set; }

        /// <summary>
        /// 返回数据格式（支持xml，json）
        /// </summary>
        private string format { get; set; }

        /// <summary>
        /// 参数列表
        /// </summary>
        private IDictionary<string, string> ListParam;

        /// <summary>
        /// 一号店API有参构造函数
        /// </summary>
        /// <param name="AppKey">AppKey</param>
        /// <param name="AppSecret">AppSecret</param>
        /// <param name="SessionKey">SessionKey</param>
        /// <param name="url">请求地址</param>
        /// <param name="ver">API协议版本，可选值:1.0</param>
        /// <param name="format">返回数据格式（支持xml，json）</param>
        public yhdApi(string AppKey, string AppSecret,string SessionKey, string url, string ver, string format) {
            this.AppKey = AppKey;
            this.AppSecret = AppSecret;
            this.SessionKey = SessionKey;
            this.url = url;
            this.ver = ver;
            this.format = format;
        }

        /// <summary>
        /// 调用一号店API
        /// </summary>
        /// <param name="method">方法名称</param>
        /// <param name="ParamList">参数列表</param>
        public void InvokeApi(string method, ApiDictionary ParamList) {
            this.method = method;
            string PostData = LoadParam(ParamList);
            this.Request(this.url, PostData);
        }

        /// <summary>
        /// 加载参数列表
        /// </summary>
        /// <param name="ParamList">参数列表</param>
        /// <returns>请求url</returns>
        private string LoadParam(ApiDictionary ParamList) {
            ListParam = new Dictionary<string, string>();

            //先加载系统级参数
            ListParam.Add("appKey", this.AppKey);
            ListParam.Add("sessionKey", this.SessionKey);
            ListParam.Add("timestamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            ListParam.Add("format", this.format);
            ListParam.Add("ver", this.ver);
            ListParam.Add("method", this.method);


            //加载应用级参数
            foreach (KeyValuePair<string, string> kvp in ParamList) {
                ListParam.Add(kvp.Key, kvp.Value);
            }

            //签名
            this.sign = getSignature(ListParam, this.AppSecret);

            //拼接Post提交数据
            List<string> Data = new List<string>();
            Data.Add("sign=" + this.sign);

            foreach (KeyValuePair<string, string> kvp in ListParam) {
                Data.Add(kvp.Key + "=" + System.Web.HttpUtility.UrlEncode(kvp.Value));
            }

            return String.Join("&", Data);
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
                    this.responseContent = reader.ReadToEnd().Replace("/r/n", "");
                }
            } catch { }
        }

        /// <summary>
        /// 计算参数签名
        /// </summary>
        /// <param name="params">请求参数集，所有参数必须已转换为字符串类型</param>
        /// <param name="secret">签名密钥</param>
        /// <returns>签名</returns>
        private string getSignature(IDictionary<string, string> parameters, string secret) {
            // 先将参数以其参数名的字典序升序进行排序
            IDictionary<string, string> sortedParams = new SortedDictionary<string, string>(parameters);
            IEnumerator<KeyValuePair<string, string>> iterator = sortedParams.GetEnumerator();

            // 遍历排序后的字典，将所有参数按"key=value"格式拼接在一起
            StringBuilder basestring = new StringBuilder();
            basestring.Append(secret);
            while (iterator.MoveNext()) {
                string key = iterator.Current.Key;
                string value = iterator.Current.Value;
                if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value)) {
                    basestring.Append(key).Append(value);
                }
            }
            basestring.Append(secret);

            // 使用MD5对待签名串求签
            MD5 md5 = MD5.Create();
            byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(basestring.ToString()));

            // 将MD5输出的二进制结果转换为小写的十六进制
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++) {
                string hex = bytes[i].ToString("x");
                if (hex.Length == 1) {
                    result.Append("0");
                }
                result.Append(hex);
            }

            return result.ToString();
        }
    }
}
