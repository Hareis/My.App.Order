using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Business.Service.JD.Response;
using My.App.Business.Service.JD.Util;

namespace My.App.Business.Service.JD.Request
{
    /// <summary>
    /// SOP发货操作
    /// </summary>
    internal class OrderSopDeliveryRequest : IRequest<OrderSopDeliveryResponse>
    {
        /// <summary>
        /// 订单ID
        /// <para>必填项</para>
        /// </summary>
        public long? order_id { get; set; }

        /// <summary>
        /// 物流公司ID(只可通过获取商家物流公司接口获得)
        /// <para>必填项</para>
        /// </summary>
        public int? logistics_id { get; set; }

        /// <summary>
        /// 运单号(当商家直送时运单号可为空)
        /// <para>选填项</para>
        /// </summary>
        public string waybill { get; set; }

        /// <summary>
        /// 流水号
        /// <para>选填项</para>
        /// </summary>
        public string trade_no { get; set; }

        public string GetApiUrl() {
            return "360buy.order.sop.outstorage";
        }

        public IDictionary<string, string> GetParameters() {
            ApiDictionary parameters = new ApiDictionary();
            parameters.Add("order_id", this.order_id);
            parameters.Add("logistics_id", this.logistics_id);
            parameters.Add("waybill", String.IsNullOrEmpty(this.waybill) ? "" : this.waybill);
            parameters.Add("trade_no", this.trade_no);
            return parameters;
        }

        public void Validate() {
            RequestValidator.ValidateRequired("order_id", this.order_id);
            RequestValidator.ValidateRequired("logistics_id", this.logistics_id);
        }
    }
}
