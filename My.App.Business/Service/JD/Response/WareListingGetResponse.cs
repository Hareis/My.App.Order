using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Business.Service.JD.Util;
using System.Xml.Serialization;

namespace My.App.Business.Service.JD.Response
{
    public class WareListingGetResponse : ApiResponse
    {
        /// <summary>
        /// 商品信息列表
        /// </summary>
        [XmlArray("")]
        [XmlArrayItem("ware_infos")]
        public List<Entity.Good> goods { get; set; }

        /// <summary>
        /// 查询商品的数量
        /// </summary>
        [XmlElement("total")]
        public long total { get; set; }
    }
}
