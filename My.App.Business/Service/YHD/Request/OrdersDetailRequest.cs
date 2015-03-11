using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Business.Service.YHD.Response;
using My.App.Business.Service.YHD.Util;

namespace My.App.Business.Service.YHD.Request
{
    internal class OrdersDetailRequest : IRequest<OrdersDetailResponse>
    {
        /// <summary>
        /// 订单编码列表（逗号分隔）,最大长度为50
        /// <para>必填项</para>
        /// </summary>
        public string orderCodeList { get; set; }

        public string GetApiUrl() {
            return "yhd.orders.detail.get";
        }

        public IDictionary<string, string> GetParameters() {
            ApiDictionary parameters = new ApiDictionary();
            parameters.Add("orderCodeList", this.orderCodeList);
            return parameters;
        }

        public void Validate() {
            RequestValidator.ValidateRequired("orderCodeList", this.orderCodeList);
        }
    }
}
