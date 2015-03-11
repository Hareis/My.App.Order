using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace My.App.Business.Service.YHD.Entity
{
    /// <summary>
    /// 商品信息
    /// </summary>
    [Serializable]
    public class OrderItem
    {
        /// <summary>
        /// 订单明细ID
        /// </summary>
        [XmlElement("id")]
        public long id { get; set; }

        /// <summary>
        /// 订单ID
        /// </summary>
        [XmlElement("orderId")]
        public long orderId { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        [XmlElement("productCName")]
        public string productCName { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        [XmlElement("orderItemAmount")]
        public decimal orderItemAmount { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [XmlElement("orderItemNum")]
        public int orderItemNum { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        [XmlElement("orderItemPrice")]
        public decimal orderItemPrice { get; set; }

        /// <summary>
        /// 产品原价
        /// </summary>
        [XmlElement("originalPrice")]
        public decimal originalPrice { get; set; }

        /// <summary>
        /// 产品id
        /// </summary>
        [XmlElement("productId")]
        public long productId { get; set; }

        /// <summary>
        /// 团购产品标识，1表示团购产品, 0表示非团购产品
        /// </summary>
        [XmlElement("groupFlag")]
        public int groupFlag { get; set; }

        /// <summary>
        /// 商家id
        /// </summary>
        [XmlElement("merchantId")]
        public long merchantId { get; set; }

        /// <summary>
        /// 退换货完成时间
        /// </summary>
        [XmlElement("processFinishDate")]
        public string processFinishDate { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [XmlElement("updateTime")]
        public string updateTime { get; set; }

        /// <summary>
        /// 产品外部编码
        /// </summary>
        [XmlElement("outerId")]
        public string outerId { get; set; }

        /// <summary>
        /// 商品运费分摊金额
        /// </summary>
        [XmlElement("deliveryFeeAmount")]
        public decimal deliveryFeeAmount { get; set; }

        /// <summary>
        /// 促销活动立减分摊金额
        /// </summary>
        [XmlElement("promotionAmount")]
        public decimal promotionAmount { get; set; }

        /// <summary>
        /// 商家抵用券分摊金额
        /// </summary>
        [XmlElement("couponAmountMerchant")]
        public decimal couponAmountMerchant { get; set; }

        /// <summary>
        /// 1mall平台抵用券分摊金额
        /// </summary>
        [XmlElement("couponPlatformDiscount")]
        public decimal couponPlatformDiscount { get; set; }

        /// <summary>
        /// 节能补贴金额
        /// </summary>
        [XmlElement("subsidyAmount")]
        public decimal subsidyAmount { get; set; }
    }
}
