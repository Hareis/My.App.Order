using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using My.App.Business.Service.JD.Util;

namespace My.App.Business.Service.JD.Response
{
    public class OrderGetResponse : ApiResponse
    {
        /// <summary>
        /// 订单信息列表
        /// </summary>
        [XmlElement("order")]
        public Entity.OrderInfo order { get; set; }
    }
}
