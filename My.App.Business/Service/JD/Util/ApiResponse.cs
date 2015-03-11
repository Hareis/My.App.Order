using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace My.App.Business.Service.JD.Util
{
    [Serializable]
    public class ApiResponse
    {
        /// <summary>
        /// 错误码
        /// </summary>
        [XmlElement("code")]
        public string ErrorCode { get; set; }

        /// <summary>
        /// 错误描述(中文)
        /// </summary>
        [XmlElement("zh_desc")]
        public string ErrorZhMessage { get; set; }

        /// <summary>
        /// 错误描述(英文)
        /// </summary>
        [XmlElement("en_desc")]
        public string ErrorEnMessage { get; set; }

        /// <summary>
        /// 响应原始内容
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 响应结果是否错误
        /// </summary>
        public bool IsError {
            get {
                return int.Parse(ErrorCode) != 0;
            }
        }
    }
}
