using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace My.App.Business.Service.JD.Entity
{
    /// <summary>
    /// 商家信息
    /// </summary>
    [Serializable]
    public class VenderInfo
    {
        /// <summary>
        /// 商家编号
        /// </summary>
        [XmlElement("vender_id")]
        public string vender_id { get; set; }

        /// <summary>
        /// 商家类型
        /// <para>0：SOP</para>
        /// <para>1：FBP</para>
        /// <para>2：LBP</para>
        /// <para>5：SOPL</para>
        /// </summary>
        [XmlElement("col_type")]
        public string col_type { get; set; }

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
        /// 主营类目编号
        /// </summary>
        [XmlElement("cate_main")]
        public string cate_main { get; set; }
    }
}
