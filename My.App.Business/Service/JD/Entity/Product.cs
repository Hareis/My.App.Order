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
    public class Product
    {
        /// <summary>
        /// skus编码 
        /// </summary>
        [XmlElement("sku_id")]
        public string sku_id { get; set; }

        /// <summary>
        /// sku名称 
        /// </summary>
        [XmlElement("name")]
        public string name { get; set; }

        /// <summary>
        /// sku主图地址
        /// </summary>
        [XmlElement("image_path")]
        public string image_path { get; set; }

        /// <summary>
        /// 上下柜状态 
        /// <para>0、下柜</para>
        /// <para>1、上柜</para>
        /// <para>2、可上柜（基本信息完备，采销没有正式上柜）</para>
        /// <para>10、POPSKU删除 </para>
        /// </summary>
        [XmlElement("state")]
        public string state { get; set; }

        /// <summary>
        /// 是否有效 
        /// <para>0无效</para>
        /// <para>1有效</para>
        /// </summary>
        [XmlElement("is_delete")]
        public string is_delete { get; set; }

        /// <summary>
        /// 品牌名称 
        /// </summary>
        [XmlElement("brand_name")]
        public string brand_name { get; set; }

        /// <summary>
        /// 重量单位
        /// </summary>
        [XmlElement("value_weight")]
        public string value_weight { get; set; }

        /// <summary>
        /// 长度单位
        /// </summary>
        [XmlElement("length")]
        public string length { get; set; }

        /// <summary>
        /// 宽度单位
        /// </summary>
        [XmlElement("width")]
        public string width { get; set; }

        /// <summary>
        /// 高度单位
        /// </summary>
        [XmlElement("height")]
        public string height { get; set; }

        /// <summary>
        /// 是否在线支付商品 
        /// </summary>
        [XmlElement("value_pay_first")]
        public string value_pay_first { get; set; }

        /// <summary>
        /// 重量
        /// </summary>
        [XmlElement("weight")]
        public string weight { get; set; }

        /// <summary>
        /// 商品的第二分类(3级) 
        /// </summary>
        [XmlElement("cid2")]
        public string cid2 { get; set; }

        /// <summary>
        /// 商品产地 
        /// </summary>
        [XmlElement("product_area")]
        public string product_area { get; set; }

        /// <summary>
        /// 上下柜日期 (如果当前商品状态是上柜，saleDate为上柜日期；反之为下柜日期 )
        /// </summary>
        [XmlElement("sale_date")]
        public string sale_date { get; set; }

        /// <summary>
        /// 质保 
        /// <para>无、无质保</para>
        /// <para>1年质保</para>
        /// <para>3年质保 </para>
        /// </summary>
        [XmlElement("wserve")]
        public string wserve { get; set; }

        /// <summary>
        /// 图片标签 默认0 没有标签 
        /// <para>0、未设置标签</para>
        /// <para>1、新品</para>
        /// <para>2、热卖 </para>
        /// </summary>
        [XmlElement("allnum")]
        public string allnum { get; set; }

        /// <summary>
        /// 新品牌id 
        /// </summary>
        [XmlElement("brand_id")]
        public string brand_id { get; set; }

        /// <summary>
        /// 颜色 
        /// </summary>
        [XmlElement("color")]
        public string color { get; set; }

        /// <summary>
        /// 颜色顺序 
        /// </summary>
        [XmlElement("color_sequence")]
        public string color_sequence { get; set; }

        /// <summary>
        /// 尺码 
        /// </summary>
        [XmlElement("size")]
        public string size { get; set; }

        /// <summary>
        /// 尺码顺序 
        /// </summary>
        [XmlElement("size_sequence")]
        public string size_sequence { get; set; }

        /// <summary>
        /// 品牌英文名称 
        /// </summary>
        [XmlElement("ebrand")]
        public string ebrand { get; set; }

        /// <summary>
        /// 品牌中文名称
        /// </summary>
        [XmlElement("cbrand")]
        public string cbrand { get; set; }

        /// <summary>
        /// 型号 
        /// </summary>
        [XmlElement("model")]
        public string model { get; set; }

        /// <summary>
        /// 分类信息，格式为：一级分类；二级分类；三级分类 
        /// </summary>
        [XmlElement("category")]
        public string category { get; set; }
    }
}
