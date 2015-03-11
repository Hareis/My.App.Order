using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Business.Service.JD.Client;
using My.App.Business.Service.JD.Parser;

namespace My.App.Business.Service.JD.Util
{
    internal class DefaultClient : IClient
    {
        private JingdongApi Api;

        public DefaultClient(string AppKey, string AppSecret, string Token, string url, string v = "2.0") {
            Api = new JingdongApi(AppKey, AppSecret, Token, url, v);
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
                rsp.ErrorZhMessage = "参数传递错误";
                rsp.ErrorEnMessage = "";
                rsp.ErrorCode = "-1";
            }

            if (isok) {
                Api.InvokeApi(request.GetApiUrl(), new ApiDictionary(request.GetParameters()));

                try {
                    IParser<T> tp = new JsonParser<T>();
                    rsp = tp.Parse(Api.responseContent);
                } catch {
                    rsp.Body = Api.responseContent;
                    rsp.ErrorZhMessage = "解析返回数据失败";
                    rsp.ErrorEnMessage = "";
                    rsp.ErrorCode = "-2";
                }
            }

            return rsp;
        }
    }
}
