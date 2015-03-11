using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace My.App.Business.Service.JD.Entity
{
    /// <summary>
    /// 处理结果类
    /// </summary>
    [Serializable]
    public class Result
    {
        /// <summary>
        /// 错误码
        /// </summary>
        [XmlElement("code")]
        public int code { get; set; }

        /// <summary>
        /// 运单号列表
        /// </summary>
        [XmlArray("")]
        [XmlElement("deliveryIdList")]
        public List<string> deliveryIdList { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        [XmlElement("message")]
        public string message { get; set; }

        /// <summary>
        /// 运单号
        /// </summary>
        [XmlElement("deliveryId")]
        public string deliveryId { get; set; }

        /// <summary>
        /// 外部订单编号
        /// </summary>
        [XmlElement("orderId")]
        public string orderId { get; set; }
    }
}
