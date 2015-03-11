using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Business.Service.JD.Response;
using My.App.Business.Service.JD.Util;

namespace My.App.Business.Service.JD.Request
{
    internal class VenderRemarkRequest : IRequest<VenderRemarkResponse>
    {
        /// <summary>
        /// 订单编号
        /// <para>必填项</para>
        /// </summary>
        public string order_id { get; set; }

        public string GetApiUrl() {
            return "jingdong.order.venderRemark.queryByOrderId";
        }

        public IDictionary<string, string> GetParameters() {
            ApiDictionary parameters = new ApiDictionary();
            parameters.Add("order_id", this.order_id);
            return parameters;
        }

        public void Validate() {
            RequestValidator.ValidateRequired("order_id", this.order_id);
        }
    }
}
