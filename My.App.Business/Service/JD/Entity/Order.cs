using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace My.App.Business.Service.JD.Entity
{
    /// <summary>
    /// 订单信息
    /// </summary>
    [Serializable]
    public class Order
    {
        /// <summary>
        /// 订单号（必须返回的字段）
        /// </summary>
        [XmlElement("order_id")]
        public string order_id { get; set; }

        /// <summary>
        /// 商家编号
        /// </summary>
        [XmlElement("vender_id")]
        public string vender_id { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        [XmlElement("pay_type")]
        public string pay_type { get; set; }

        /// <summary>
        /// 商品信息对象
        /// </summary>
        [XmlArray("")]
        [XmlArrayItem("item_info_list")]
        public List<Ware> ware_infos { get; set; }

        /// <summary>
        /// 商品的运费
        /// </summary>
        [XmlElement("freight_price")]
        public string freight_price { get; set; }

        /// <summary>
        /// 订单总金额
        /// </summary>
        [XmlElement("order_total_price")]
        public string order_total_price { get; set; }

        /// <summary>
        /// 实收金额
        /// </summary>
        [XmlElement("order_payment")]
        public string payment { get; set; }

        /// <summary>
        /// 收货人基本信息
        /// </summary>
        [XmlElement("consignee_info")]
        public Consignee consignee_info { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        [XmlElement("order_state")]
        public string order_state { get; set; }

        /// <summary>
        /// 实际运费
        /// </summary>
        [XmlElement("fact_freight_price")]
        public string fact_freight_price { get; set; }

        /// <summary>
        /// 送货日期
        /// </summary>
        [XmlElement("delivery_type")]
        public string delivery_date_remark { get; set; }

        /// <summary>
        /// 优惠总金额
        /// </summary>
        [XmlElement("seller_discount")]
        public string total_discount_fee { get; set; }

        /// <summary>
        /// 发票信息
        /// </summary>
        [XmlElement("invoice_info")]
        public string invoice_info { get; set; }

        /// <summary>
        /// 买家订单备注
        /// </summary>
        [XmlElement("order_remark")]
        public string buyer_order_remark { get; set; }

        /// <summary>
        /// 商家订单备注
        /// </summary>
        [XmlElement("vender_remark")]
        public string vender_remark { get; set; }

        /// <summary>
        /// 下单时间
        /// </summary>
        [XmlElement("order_start_time")]
        public string order_start_time { get; set; }

        /// <summary>
        /// 结单时间
        /// </summary>
        [XmlElement("order_end_time")]
        public string order_end_time { get; set; }

        /// <summary>
        /// 付款确认时间
        /// </summary>
        [XmlElement("payment_confirm_time")]
        public string payment_confirm_time { get; set; }

        /// <summary>
        /// 运单号
        /// </summary>
        [XmlElement("waybill")]
        public string waybill { get; set; }

        /// <summary>
        /// 备注颜色标识
        /// </summary>
        public string Flag { get; set; }
    }
}
