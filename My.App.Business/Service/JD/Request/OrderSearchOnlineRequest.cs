using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Business.Service.JD.Response;
using My.App.Business.Service.JD.Util;

namespace My.App.Business.Service.JD.Request
{
    /// <summary>
    /// 获取一段时间内订单信息-生产数据
    /// </summary>
    internal class OrderSearchOnlineRequest : IRequest<OrderSearchOnlineResponse>
    {
        /// <summary>
        /// 开始时间（格式是：2010-12-20 17:15:00)
        /// <para>必填项</para>
        /// </summary>
        public string start_date { get; set; }

        /// <summary>
        /// 结束时间（格式是：2010-12-20 17:15:00)
        /// <para>开始时间 和 结束时间 不得相差超过1个月</para>
        /// <para>必填项</para>
        /// </summary>
        public string end_date { get; set; }

        /// <summary>
        /// 订单状态
        /// <para>每次查询只能输入一个订单状态</para>
        /// <para>ALL(全部)</para>
        /// <para>WAIT_SELLER_STOCK_OUT(等待出库)</para>
        /// <para>TO_JD_DISTRIBUTION_CENTER(发往京东配送中心 LBP商家专用)</para>
        /// <para>JD_DISTRIBUTION_CENTER_RECEIVED(京东配送中心已收货 LBP商家专用)</para>
        /// <para>WAIT_SELLER_DELIVERY(等待发货)</para>
        /// <para>WAIT_BUYER_CONFIRM_GOODS(已发货)</para>
        /// <para>TRADE_FINISHED(买家已收货)</para>
        /// <para>TRADE_BUYER_REFUSED(拒收 LBP商家专用)</para>
        /// <para>必填项</para>
        /// </summary>
        public string order_state { get; set; }

        /// <summary>
        /// 查询的页数
        /// <para>最大100页</para>
        /// <para>必填项</para>
        /// </summary>
        public long? page { get; set; }

        /// <summary>
        /// 每页的条数
        /// <para>最大20条</para>
        /// <para>必填项</para>
        /// </summary>
        public long? page_size { get; set; }

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
            return "360buy.order.search";
        }

        public IDictionary<string, string> GetParameters() {
            ApiDictionary parameters = new ApiDictionary();
            parameters.Add("start_date", this.start_date);
            parameters.Add("end_date", this.end_date);
            parameters.Add("order_state", this.order_state);
            parameters.Add("page", this.page);
            parameters.Add("page_size", this.page_size);
            parameters.Add("optional_fields", String.IsNullOrEmpty(this.optional_fields) ? "" : this.optional_fields);
            return parameters;
        }

        public void Validate() {
            RequestValidator.ValidateRequired("start_date", this.start_date);
            RequestValidator.ValidateRequired("end_date", this.end_date);
            RequestValidator.ValidateRequired("order_state", this.order_state);
            RequestValidator.ValidateRequired("page", this.page);
            RequestValidator.ValidateMaxValue("page", this.page.Value, 100);
            RequestValidator.ValidateMinValue("page", this.page.Value, 0);
            RequestValidator.ValidateRequired("page_size", this.page_size);
            RequestValidator.ValidateMaxValue("page_size", this.page_size.Value, 20);
            RequestValidator.ValidateMinValue("page_size", this.page_size.Value, 0);
        }
    }
}
