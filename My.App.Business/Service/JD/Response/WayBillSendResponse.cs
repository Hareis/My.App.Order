using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using My.App.Business.Service.JD.Util;

namespace My.App.Business.Service.JD.Response
{
    [Serializable]
    public class WayBillSendResponse : ApiResponse
    {
        /// <summary>
        /// 处理结果类
        /// </summary>
        [XmlElement("resultInfo")]
        public Entity.Result resultInfo { get; set; }
    }
}
