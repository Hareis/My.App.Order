using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Business.Service.YHD.Util;
using System.Xml.Serialization;

namespace My.App.Business.Service.YHD.Response
{
    public class ProductAttributeGetResponse:ApiResponse
    {
        /// <summary>
        /// 查询成功记录数
        /// </summary>
        [XmlElement("totalCount")]
        public int totalCount { get; set; }

        /// <summary>
        /// 产品系列属性信息
        /// </summary>
        [XmlElement("prodSerialAttributeInfo")]
        public Entity.AttributeInfo Attribute { get; set; }
    }
}
