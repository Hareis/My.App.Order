using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace My.App.Business.Service.JD.Entity
{
    /// <summary>
    /// 面单打印数据
    /// </summary>
    [Serializable]
    public class PrintData
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        [XmlElement("id")]
        public string id { get; set; }

        /// <summary>
        /// 出库时间
        /// </summary>
        [XmlElement("out_bound_date")]
        public string out_bound_date { get; set; }

        /// <summary>
        /// 是否送货前通知
        /// </summary>
        [XmlElement("bf_deli_good_glag")]
        public string bf_deli_good_glag { get; set; }

        /// <summary>
        /// 送货时间
        /// </summary>
        [XmlElement("cod_time_name")]
        public string cod_time_name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [XmlElement("remark")]
        public string remark { get; set; }

        /// <summary>
        /// 配送中心名称
        /// </summary>
        [XmlElement("cky2_name")]
        public string cky2_name { get; set; }

        /// <summary>
        /// 分拣代码
        /// </summary>
        [XmlElement("sorting_code")]
        public string sorting_code { get; set; }

        /// <summary>
        /// 订购时间
        /// </summary>
        [XmlElement("create_date")]
        public string create_date { get; set; }

        /// <summary>
        /// 支付金额
        /// </summary>
        [XmlElement("should_pay")]
        public string should_pay { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        [XmlElement("payment_typeStr")]
        public string payment_typeStr { get; set; }

        /// <summary>
        /// 配送站点名称
        /// </summary>
        [XmlElement("partner")]
        public string partner { get; set; }

        /// <summary>
        /// 条形码
        /// </summary>
        [XmlElement("generade")]
        public string generade { get; set; }

        /// <summary>
        /// 商品总数
        /// </summary>
        [XmlElement("items_count")]
        public string items_count { get; set; }

        /*/// <summary>
        /// 商品信息
        /// </summary>
        [XmlArray("")]
        [XmlElement("order_item")]
        public List<OrderItem> Item { get; set; }

        /// <summary>
        /// 商品信息
        /// </summary>
        [XmlElement("Consignee")]
        public PrintConsignee consignee { get; set; }*/

        /// <summary>
        /// 运费
        /// </summary>
        [XmlElement("freight")]
        public string freight { get; set; }
    }
}
