using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace My.App.Business.Service.YHD.Entity
{
    /// <summary>
    /// 错误描述表
    /// </summary>
    [Serializable]
    public class errDetail
    {
        /// <summary>
        /// 错误代码
        /// </summary>
        [XmlElement("errorCode")]
        public string errorCode { get; set; }

        /// <summary>
        /// 错误描述
        /// </summary>
        [XmlElement("errorDes")]
        public string errorMsg { get; set; }
    }
}
