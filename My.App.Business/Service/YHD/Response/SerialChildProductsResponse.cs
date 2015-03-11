using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Business.Service.YHD.Util;
using System.Xml.Serialization;

namespace My.App.Business.Service.YHD.Response
{
    public class SerialChildProductsResponse : ApiResponse
    {
        /// <summary>
        /// 查询成功记录数
        /// </summary>
        [XmlElement("totalCount")]
        public int totalCount { get; set; }

        [XmlArray("serialChildProdList")]
        [XmlArrayItem("serialChildProd")]
        public List<Entity.SerialChildProd> Products { get; set; }
    }
}
