using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Business.Service.JD.Util;
using System.Xml.Serialization;

namespace My.App.Business.Service.JD.Response
{
    [Serializable]
    public class WayBillCodeGetResponse : ApiResponse
    {
        /// <summary>
        /// 处理结果类
        /// </summary>
        [XmlElement("resultInfo")]
        public Entity.Result resultInfo { get; set; }
    }
}
