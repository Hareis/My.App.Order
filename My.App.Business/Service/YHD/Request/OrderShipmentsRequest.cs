using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Business.Service.YHD.Response;
using My.App.Business.Service.YHD.Util;

namespace My.App.Business.Service.YHD.Request
{
    internal class OrderShipmentsRequest : IRequest<OrderShipmentsResponse> {
        /// <summary>
        /// 订单号(订单编码)
        /// <para>必填项</para>
        /// </summary>
        public string orderCode { get; set; }

        /// <summary>
        /// 配送商ID(从获取物流信息接口中获取)
        /// <para>必填项</para>
        /// </summary>
        public long? deliverySupplierId { get; set; }

        /// <summary>
        /// 运单号(快递编号)
        /// <para>必填项</para>
        /// </summary>
        public string expressNbr { get; set; }

        public string GetApiUrl() {
            return "yhd.logistics.order.shipments.update";
        }

        public IDictionary<string, string> GetParameters() {
            ApiDictionary parameters = new ApiDictionary();
            parameters.Add("orderCode", this.orderCode);
            parameters.Add("deliverySupplierId", this.deliverySupplierId);
            parameters.Add("expressNbr", this.expressNbr);
            return parameters;
        }

        public void Validate() {
            RequestValidator.ValidateRequired("orderCode", this.orderCode);
            RequestValidator.ValidateRequired("deliverySupplierId", this.deliverySupplierId);
            RequestValidator.ValidateRequired("expressNbr", this.expressNbr);
        }
    }
}
