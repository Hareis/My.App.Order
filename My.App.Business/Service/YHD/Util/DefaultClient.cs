using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Business.Service.YHD.Client;
using My.App.Business.Service.YHD.Parser;

namespace My.App.Business.Service.YHD.Util
{
    internal class DefaultClient : IClient
    {
        private yhdApi Api;
        private string format = "xml";

        public DefaultClient(string AppKey, string AppSecret,string SessionKey, string url, string ver = "1.0", string format = "xml") {
            Api = new yhdApi(AppKey, AppSecret, SessionKey, url, ver = "1.0", format = "xml");
            this.format = format;
        }

        public T Execute<T>(Request.IRequest<T> request) where T : ApiResponse {
            //检查参数的有效性
            T rsp = Activator.CreateInstance<T>();
            bool isok = false;
            try {
                request.Validate();
                isok = true;
            } catch {
                rsp.Body = "";
                rsp.errDetailInfo = new List<Entity.errDetail>();
                rsp.errDetailInfo.Add(new Entity.errDetail() {
                    errorCode = "",
                    errorMsg = "参数传递错误"
                });

                rsp.errorCount = 1;
            }

            if (isok) {
                Api.InvokeApi(request.GetApiUrl(), new ApiDictionary(request.GetParameters()));

                try {
                    IParser<T> tp = new XmlParser<T>();
                    rsp = tp.Parse(Api.responseContent.Replace("&", "@"));
                } catch {
                    rsp.Body = Api.responseContent;
                    rsp.errDetailInfo = new List<Entity.errDetail>();
                    rsp.errDetailInfo.Add(new Entity.errDetail() {
                        errorCode = "",
                        errorMsg = "解析返回数据失败"
                    });
                    rsp.errorCount = 1;
                }
            }

            return rsp;
        }
    }
}
