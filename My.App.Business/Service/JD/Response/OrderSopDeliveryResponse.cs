using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Business.Service.JD.Util;
using System.Xml.Serialization;

namespace My.App.Business.Service.JD.Response
{
    public class OrderSopDeliveryResponse : ApiResponse
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        [XmlElement("order_id")]
        public string order_id { get; set; }

        /// <summary>
        /// 商家ID
        /// </summary>
        [XmlElement("vender_id")]
        public string vender_id { get; set; }

        /// <summary>
        /// 订单发货操作时间
        /// </summary>
        [XmlElement("modified")]
        public string modified { get; set; }
    }
}
