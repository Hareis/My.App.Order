using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Business.Service.JD.Util;
using System.Xml.Serialization;

namespace My.App.Business.Service.JD.Response
{
    public class DeliveryLogisticsGetResponse : ApiResponse
    {
        /// <summary>
        /// 商家ID
        /// </summary>
        [XmlArray("logistics_companies")]
        [XmlArrayItem("vender_id")]
        public string vender_id { get; set; }

        /// <summary>
        /// 物流公司列表
        /// </summary>
        [XmlArray("logistics_companies")]
        [XmlArrayItem("logistics_list")]
        public List<Entity.ApiCompanyCustom> logistics_list { get; set; }
    }
}
