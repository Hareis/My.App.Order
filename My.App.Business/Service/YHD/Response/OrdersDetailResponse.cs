using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Business.Service.YHD.Util;
using System.Xml.Serialization;

namespace My.App.Business.Service.YHD.Response
{
    [Serializable]
    public class OrdersDetailResponse : ApiResponse
    {
        /// <summary>
        /// 订单详情列表
        /// </summary>
        [XmlArray("orderInfoList")]
        [XmlArrayItem("orderInfo")]
        public List<Entity.OrderInfo> Orders { get; set; }

        /// <summary>
        /// 查询成功记录数
        /// </summary>
        [XmlElement("totalCount")]
        public int totalCount { get; set; }
    }
}
