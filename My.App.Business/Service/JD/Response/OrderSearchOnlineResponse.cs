using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using My.App.Business.Service.JD.Util;

namespace My.App.Business.Service.JD.Response
{
    [Serializable]
    public class OrderSearchOnlineResponse : ApiResponse
    {
        /// <summary>
        /// 订单信息列表
        /// </summary>
        [XmlArray("order_search")]
        [XmlArrayItem("order_info_list")]
        public List<Entity.Order> order_info_list { get; set; }

        /// <summary>
        /// 查询订单的数量
        /// </summary>
        [XmlArray("order_search")]
        [XmlArrayItem("order_total")]
        public long order_total { get; set; }
    }
}
