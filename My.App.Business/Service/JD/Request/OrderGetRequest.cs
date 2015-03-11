using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Business.Service.JD.Response;
using My.App.Business.Service.JD.Util;

namespace My.App.Business.Service.JD.Request
{
    internal class OrderGetRequest : IRequest<OrderGetResponse>
    {
        /// <summary>
        /// 订单编号
        /// <para>必填项</para>
        /// </summary>
        public string order_id { get; set; }

        /// <summary>
        /// 商家希望返回的订单的信息字段,每个字段以逗号分隔
        /// <para>默认返回的字段:</para>
        /// <para>vender_id(商家编号)</para>
        /// <para>freight_price(订单运费)</para>
        /// <para>fact_freight_price(实际运费)</para>
        /// <para>delivery_date_remark(送货日期)</para>
        /// <para>ware_total_price(商品总金额)</para>
        /// <para>order_total_price(订单总金额)</para>
        /// <para>order_state(订单状态)</para>
        /// <para>invoice_info(发票信息)</para>
        /// <para>buyer_remark(买家订单备注)</para>
        /// <para>order_start_time(下单时间)</para>
        /// <para>ware_id(商品id)</para>
        /// <para>sku_id(京东内部SKU id)</para>
        /// <para>sku_out_id(对应商家的SKU的id)</para>
        /// <para>jd_price(SKU京东价)</para>
        /// <para>ware_discount_fee(优惠金额)</para>
        /// <para>ware_total(商品数量)</para>
        /// <para>consignee_info(用户信息)</para>
        /// <para>选填项</para>
        /// </summary>
        public string optional_fields { get; set; }

        /// <summary>
        /// 获取方法名称
        /// </summary>
        /// <returns></returns>
        public string GetApiUrl() {
            return "360buy.order.get";
        }

        public IDictionary<string, string> GetParameters() {
            ApiDictionary parameters = new ApiDictionary();
            parameters.Add("order_id", this.order_id);
            parameters.Add("optional_fields", String.IsNullOrEmpty(this.optional_fields) ? "" : this.optional_fields);
            return parameters;
        }

        public void Validate() {
            RequestValidator.ValidateRequired("order_id", this.order_id);
        }
    }
}
