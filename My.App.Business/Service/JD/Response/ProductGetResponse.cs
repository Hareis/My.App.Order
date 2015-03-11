using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Business.Service.JD.Util;
using My.App.Business.Service.JD.Entity;
using System.Xml.Serialization;

namespace My.App.Business.Service.JD.Response
{
    public class ProductGetResponse : ApiResponse
    {
        /// <summary>
        /// 商品属性列表
        /// </summary>
        [XmlArray("")]
        [XmlArrayItem("product_base")]
        public List<Entity.Product> Products { get; set; }
    }
}
