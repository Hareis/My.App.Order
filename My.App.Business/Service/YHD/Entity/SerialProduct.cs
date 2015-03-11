﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace My.App.Business.Service.YHD.Entity
{
    [Serializable]
    public class SerialProduct
    {
        /// <summary>
        /// 商家产品编码
        /// </summary>
        [XmlElement("productCode")]
        public string productCode { get; set; }

        /// <summary>
        /// 产品中文名称
        /// </summary>
        [XmlElement("productCname")]
        public string productCname { get; set; }

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
        /// 是否可见(强制'上'下架),1是0否
        /// </summary>
        [XmlElement("canShow")]
        public int canShow { get; set; }

        /// <summary>
        /// 上下架状态0：下架，1：上架
        /// </summary>
        [XmlElement("canSale")]
        public int canSale { get; set; }

        /// <summary>
        /// 产品条形码
        /// </summary>
        [XmlElement("ean13")]
        public string ean13 { get; set; }

        /// <summary>
        /// 产品类目ID
        /// </summary>
        [XmlElement("categoryId")]
        public long categoryId { get; set; }

        /// <summary>
        /// 产品审核状态:1.新增未审核;2.编辑待审核;3.审核未通过;4.审核通过;5.图片审核失败;6.文描审核失败
        /// </summary>
        [XmlElement("verifyFlg")]
        public int verifyFlg { get; set; }

        /// <summary>
        /// 是否二次审核0：非二次审核；1：是二次审核
        /// </summary>
        [XmlElement("isDupAudit")]
        public int isDupAudit { get; set; }

        /// <summary>
        /// 图片信息列表
        /// <para>图片之间逗号分隔</para>
        /// <para>图片id、图片URL、主图标识之间用竖线分隔</para>
        /// <para>其中1：表示主图，0：表示非主图</para>
        /// </summary>
        [XmlElement("prodImg")]
        public string prodImg { get; set; }

        /// <summary>
        /// 前台商品详情页链接（正式产品才会有）
        /// </summary>
        [XmlElement("prodDetailUrl")]
        public string prodDetailUrl { get; set; }
    }
}
