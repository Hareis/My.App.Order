using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Business.Service.JD.Util;
using System.Xml.Serialization;

namespace My.App.Business.Service.JD.Response
{
    public class RemarkUpdateResponse : ApiResponse
    {
        /// <summary>
        /// 修改时间
        /// </summary>
        [XmlElement("modified")]
        public string modified { get; set; }

        /// <summary>
        /// 订单id
        /// </summary>
        [XmlElement("order_id")]
        public string order_id { get; set; }
    }
}
