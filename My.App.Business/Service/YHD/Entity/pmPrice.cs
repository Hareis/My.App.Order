using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace My.App.Business.Service.YHD.Entity
{
    [Serializable]
    public class pmPrice
    {
        /// <summary>
        /// 产品ID
        /// </summary>
        [XmlElement("productId")]
        public long productId { get; set; }

        /// <summary>
        /// 外部产品ID
        /// </summary>
        [XmlElement("outerId")]
        public string outerId { get; set; }

        /// <summary>
        /// 进价
        /// </summary>
        [XmlElement("inPrice")]
        public decimal inPrice { get; set; }

        /// <summary>
        /// 1号商城价
        /// </summary>
        [XmlElement("nonMemberPrice")]
        public decimal nonMemberPrice { get; set; }

        /// <summary>
        /// 促销1号商城价
        /// </summary>
        [XmlElement("promNonMemberPrice")]
        public decimal promNonMemberPrice { get; set; }

        /// <summary>
        /// 市场价
        /// </summary>
        [XmlElement("productListPrice")]
        public decimal productListPrice { get; set; }

        /// <summary>
        /// 促销开始时间
        /// </summary>
        [XmlElement("promStartTime")]
        public string promStartTime { get; set; }

        /// <summary>
        /// 促销结束时间
        /// </summary>
        [XmlElement("promEndTime")]
        public string promEndTime { get; set; }

        /// <summary>
        /// 特价限制总数量: 0表示无限制
        /// </summary>
        [XmlElement("specialPriceLimitNumber")]
        public int specialPriceLimitNumber { get; set; }

        /// <summary>
        /// 每人特价限制数量: 0表示无限制
        /// </summary>
        [XmlElement("userPriceLimitNumber")]
        public int userPriceLimitNumber { get; set; }

        /// <summary>
        /// 是否是vip产品（1：是、0：否）
        /// </summary>
        [XmlElement("isCanVipDiscount")]
        public int isCanVipDiscount { get; set; }
    }
}
