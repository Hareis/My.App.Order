using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Business.Service.YHD.Util;
using System.Xml.Serialization;

namespace My.App.Business.Service.YHD.Response
{
    [Serializable]
    public class OrderSearchResponse : ApiResponse
    {
        /// <summary>
        /// 订单列表
        /// </summary>
        [XmlArray("orderList")]
        [XmlArrayItem("order")]
        public List<Entity.Order> Orders { get; set; }

        /// <summary>
        /// 查询成功记录数
        /// </summary>
        [XmlElement("totalCount")]
        public int totalCount { get; set; }
    }
}
