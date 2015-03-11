using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Business.Service.JD.Response;
using My.App.Business.Service.JD.Util;

namespace My.App.Business.Service.JD.Request
{
    internal class VenderShopRequest : IRequest<VenderShopResponse>
    {
        public string GetApiUrl() {
            return "jingdong.vender.shop.query";
        }

        public IDictionary<string, string> GetParameters() {
            return new ApiDictionary();
        }

        public void Validate() { }
    }
}
