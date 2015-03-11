using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace My.App.Business.Service.YHD.Entity
{
    /// <summary>
    /// 店铺信息
    /// </summary>
    [Serializable]
    public class ShopInfo
    {
        /// <summary>
        /// 店铺ID
        /// </summary>
        [XmlElement("storeId")]
        public string storeId { get; set; }

        /// <summary>
        /// 店铺名称
        /// </summary>
        [XmlElement("storeName")]
        public string storeName { get; set; }

        /// <summary>
        /// 店铺网址
        /// </summary>
        [XmlElement("storeAddress")]
        public string storeAddress { get; set; }

        /// <summary>
        /// 开店时间
        /// </summary>
        [XmlElement("storeOpenTime")]
        public string storeOpenTime { get; set; }

        /// <summary>
        /// 店铺所属的类目编号
        /// </summary>
        [XmlElement("storeCategoryCode")]
        public string storeCategoryCode { get; set; }

        /// <summary>
        /// 卖家昵称
        /// </summary>
        [XmlElement("storeNickName")]
        public string storeNickName { get; set; }

        /// <summary>
        /// 店铺评分(服务态度)
        /// </summary>
        [XmlElement("attitudeExactExpPoint")]
        public string attitudeExactExpPoint { get; set; }

        /// <summary>
        /// 店铺评分(描述相符)
        /// </summary>
        [XmlElement("descriptExactExpPoint")]
        public string descriptExactExpPoint { get; set; }

        /// <summary>
        /// 店铺评分(发货速度)
        /// </summary>
        [XmlElement("logisticeExactExpPoint")]
        public string logisticeExactExpPoint { get; set; }

        /// <summary>
        /// 主营类目
        /// </summary>
        [XmlElement("storeOperateCategory")]
        public string storeOperateCategory { get; set; }

        /// <summary>
        /// 店铺模式（自配送、供应商）
        /// </summary>
        [XmlElement("storeMode")]
        public string storeMode { get; set; }
    }
}
