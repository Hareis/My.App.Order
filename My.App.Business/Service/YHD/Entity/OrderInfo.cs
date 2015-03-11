using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace My.App.Business.Service.YHD.Entity
{
    /// <summary>
    /// 订单信息
    /// </summary>
    [Serializable]
    public class OrderInfo
    {
        /// <summary>
        /// 订单详情
        /// </summary>
        [XmlElement("orderDetail")]
        public OrderDetail Detail { get; set; }


        /// <summary>
        /// 商品信息列表
        /// </summary>
        [XmlArray("orderItemList")]
        [XmlArrayItem("orderItem")]
        public List<OrderItem> Items { get; set; }
    }
}
