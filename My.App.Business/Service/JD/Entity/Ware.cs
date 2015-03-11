using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace My.App.Business.Service.JD.Entity
{
    /// <summary>
    /// 商品信息
    /// </summary>
    [Serializable]
    public class Ware
    {
        /// <summary>
        /// 商品id
        /// </summary>
        [XmlElement("ware_id")]
        public string ware_id { get; set; }

        /// <summary>
        /// 商品的名称+SKU规格
        /// </summary>
        [XmlElement("sku_name")]
        public string ware_name { get; set; }

        /// <summary>
        /// Sku外部id
        /// </summary>
        [XmlElement("outer_sku_id")]
        public string sku_out_id { get; set; }

        /// <summary>
        /// Sku内部id
        /// </summary>
        [XmlElement("sku_id")]
        public string sku_id { get; set; }

        /// <summary>
        /// 京东价
        /// </summary>
        [XmlElement("jd_price")]
        public string jd_price { get; set; }

        /// <summary>
        /// 商品数量
        /// </summary>
        [XmlElement("item_total")]
        public string ware_total { get; set; }

        /// <summary>
        /// 商品货号
        /// </summary>
        [XmlElement("product_no")]
        public string product_no { get; set; }

        /// <summary>
        /// 赠送积分
        /// </summary>
        [XmlElement("gift_point")]
        public string gift_point { get; set; }
    }
}
