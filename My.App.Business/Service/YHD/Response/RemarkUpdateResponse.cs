using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Business.Service.YHD.Util;
using System.Xml.Serialization;

namespace My.App.Business.Service.YHD.Response
{
    public class RemarkUpdateResponse : ApiResponse
    {
        /// <summary>
        /// 更新成功记录数
        /// </summary>
        [XmlElement("updatecount")]
        public int updatecount { get; set; }
    }
}
