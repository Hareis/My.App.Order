using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace My.App.Business.Service.JD.Entity
{
    [Serializable]
    public class OrderInfo
    {
        /// <summary>
        /// 订单信息
        /// </summary>
        [XmlElement("orderInfo")]
        public Entity.Order orderInfo { get; set; }
    }
}
