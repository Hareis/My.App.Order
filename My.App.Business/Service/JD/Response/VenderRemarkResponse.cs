using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Business.Service.JD.Util;
using My.App.Business.Service.JD.Entity;
using System.Xml.Serialization;

namespace My.App.Business.Service.JD.Response
{
    public class VenderRemarkResponse : ApiResponse
    {
        /// <summary>
        /// 结果集
        /// </summary>
        [XmlElement("venderRemarkQueryResult")]
        public VenderRemarkQueryResult Result { get; set; }
    }
}
