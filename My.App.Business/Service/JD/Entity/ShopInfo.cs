using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace My.App.Business.Service.JD.Entity
{
    /// <summary>
    /// 店铺信息
    /// </summary>
    [Serializable]
    public class ShopInfo
    {
        /// <summary>
        /// 商家编号 
        /// </summary>
        [XmlElement("vender_id")]
        public string vender_id { get; set; }

        /// <summary>
        /// 店铺编号 
        /// </summary>
        [XmlElement("shop_id")]
        public string shop_id { get; set; }

        /// <summary>
        /// 店铺名称 
        /// </summary>
        [XmlElement("shop_name")]
        public string shop_name { get; set; }

        /// <summary>
        /// 开店时间 
        /// </summary>
        [XmlElement("open_time")]
        public string open_time { get; set; }

        /// <summary>
        /// logo地址 
        /// </summary>
        [XmlElement("logo_url")]
        public string logo_url { get; set; }

        /// <summary>
        /// 店铺简介 
        /// </summary>
        [XmlElement("brief")]
        public string brief { get; set; }

        /// <summary>
        /// 主营类目编号 
        /// </summary>
        [XmlElement("category_main")]
        public string category_main { get; set; }

        /// <summary>
        /// 主营类目名称 
        /// </summary>
        [XmlElement("category_main_name")]
        public string category_main_name { get; set; }
    }
}
