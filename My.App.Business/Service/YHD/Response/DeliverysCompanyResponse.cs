using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Business.Service.YHD.Util;
using System.Xml.Serialization;

namespace My.App.Business.Service.YHD.Response
{
    public class DeliverysCompanyResponse : ApiResponse
    {
        /// <summary>
        /// 物流信息列表
        /// </summary>
        [XmlArray("logisticsInfoList")]
        [XmlArrayItem("logisticsInfo")]
        public List<Entity.LogisticsInfo> Logistices { get; set; }
    }
}
