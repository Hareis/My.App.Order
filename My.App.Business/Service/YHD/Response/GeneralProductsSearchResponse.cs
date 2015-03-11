using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Business.Service.YHD.Util;
using System.Xml.Serialization;

namespace My.App.Business.Service.YHD.Response
{
    [Serializable]
    public class GeneralProductsSearchResponse : ApiResponse
    {
        /// <summary>
        /// 查询成功记录数
        /// </summary>
        [XmlElement("totalCount")]
        public int totalCount { get; set; }

        /// <summary>
        /// 产品列表
        /// </summary>
        [XmlArray("productList")]
        [XmlArrayItem("product")]
        public List<Entity.GeneralProduct> Products { get; set; }
    }
}
