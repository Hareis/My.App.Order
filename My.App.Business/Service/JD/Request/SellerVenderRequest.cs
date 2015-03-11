using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Business.Service.JD.Response;
using My.App.Business.Service.JD.Util;

namespace My.App.Business.Service.JD.Request
{
    /// <summary>
    /// 查询商家基本信息
    /// </summary>
    internal class SellerVenderRequest : IRequest<SellerVenderResponse>
    {
        public string GetApiUrl() {
            return "jingdong.seller.vender.info.get";
        }

        public IDictionary<string, string> GetParameters() {
            return new ApiDictionary();
        }

        public void Validate() { }
    }
}
