using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Business.Service.JD.Response;
using My.App.Business.Service.JD.Util;

namespace My.App.Business.Service.JD.Request
{
    /// <summary>
    /// 商家获取物流公司
    /// </summary>
    internal class DeliveryLogisticsGetRequest : IRequest<DeliveryLogisticsGetResponse>
    {
        public string GetApiUrl() {
            return "360buy.delivery.logistics.get";
        }

        public IDictionary<string, string> GetParameters() {
            return new ApiDictionary();
        }

        public void Validate() { }
    }
}
