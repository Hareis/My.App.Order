using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace My.App.Business.Service.JD.Entity
{
    [Serializable]
    public class Good
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        [XmlElement("ware_id")]
        public string ware_id { get; set; }

        /// <summary>
        /// spu ID
        /// </summary>
        [XmlElement("spu_id")]
        public string spu_id { get; set; }

        /// <summary>
        /// 分类ID 三级类目ID
        /// </summary>
        [XmlElement("cid")]
        public string cid { get; set; }

        /// <summary>
        /// 商家ID
        /// </summary>
        [XmlElement("vender_id")]
        public string vender_id { get; set; }

        /// <summary>
        /// 店铺ID
        /// </summary>
        [XmlElement("shop_id")]
        public string shop_id { get; set; }

        /// <summary>
        /// <para>商品状态:</para>
        /// <para>NEVER_UP:从未上架</para>
        /// <para>CUSTORMER_DOWN:自主下架</para>
        /// <para>SYSTEM_DOWN:系统下架</para>
        /// <para>ON_SALE:在售</para>
        /// <para>AUDIT_AWAIT: 待审核</para>
        /// <para>AUDIT_FAIL: 审核不通过</para>
        /// </summary>
        [XmlElement("ware_status")]
        public string ware_status { get; set; }

        /// <summary>
        /// 商品标题
        /// </summary>
        [XmlElement("title")]
        public string title { get; set; }

        /// <summary>
        /// 货号
        /// </summary>
        [XmlElement("item_num")]
        public string item_num { get; set; }

        /// <summary>
        /// UPC编码
        /// </summary>
        [XmlElement("upc_code")]
        public string upc_code { get; set; }

        /// <summary>
        /// 运费模板
        /// </summary>
        [XmlElement("transport_id")]
        public string transport_id { get; set; }

        /// <summary>
        /// 最后上架时间
        /// </summary>
        [XmlElement("online_time")]
        public string online_time { get; set; }

        /// <summary>
        /// 最后下架时间
        /// </summary>
        [XmlElement("offline_time")]
        public string offline_time { get; set; }

        /// <summary>
        /// 可选属性
        /// </summary>
        [XmlElement("attributes")]
        public string attributes { get; set; }

        /// <summary>
        /// 进货价, 精确到2位小数，单位:元
        /// </summary>
        [XmlElement("cost_price")]
        public string cost_price { get; set; }

        /// <summary>
        /// 市场价, 精确到2位小数，单位:元
        /// </summary>
        [XmlElement("market_price")]
        public string market_price { get; set; }

        /// <summary>
        /// 京东价,精确到2位小数，单位:元
        /// </summary>
        [XmlElement("jd_price")]
        public string jd_price { get; set; }

        /// <summary>
        /// 库存
        /// </summary>
        [XmlElement("stock_num")]
        public string stock_num { get; set; }

        /// <summary>
        /// 商品的主图
        /// </summary>
        [XmlElement("logo")]
        public string logo { get; set; }

        /// <summary>
        /// 录入人
        /// </summary>
        [XmlElement("creator")]
        public string creator { get; set; }

        /// <summary>
        /// <para>状态：</para>
        /// <para>Delete:删除</para>
        /// <para>Invalid:无效</para>
        /// <para>Valid :有效</para>
        /// </summary>
        [XmlElement("status")]
        public string status { get; set; }

        /// <summary>
        /// 重量,单位:公斤
        /// </summary>
        [XmlElement("weight")]
        public string weight { get; set; }

        /// <summary>
        /// WARE_WARE创建时间 时间格式：yyyy-MM-ddHH:mm:ss
        /// </summary>
        [XmlElement("created")]
        public string created { get; set; }

        /// <summary>
        /// WARE_WARE修改时间 时间格式：yyyy-MM-ddHH:mm:ss
        /// </summary>
        [XmlElement("modified")]
        public string modified { get; set; }

        /// <summary>
        /// 外部id,商家设置的外部id（保留字段）
        /// </summary>
        [XmlElement("outer_id")]
        public string outer_id { get; set; }
    }
}
