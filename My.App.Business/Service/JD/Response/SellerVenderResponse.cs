using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Business.Service.JD.Util;
using My.App.Business.Service.JD.Entity;
using System.Xml.Serialization;

namespace My.App.Business.Service.JD.Response
{
    public class SellerVenderResponse : ApiResponse
    {
        /// <summary>
        /// 商家信息
        /// </summary>
        [XmlElement("vender_info_result")]
        public VenderInfo VenderResult { get; set; }
    }
}
