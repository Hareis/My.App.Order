using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using My.App.Business.Service.YHD.Entity;

namespace My.App.Business.Service.YHD.Util
{
    [Serializable]
    public class ApiResponse
    {
        /// <summary>
        /// 响应原始内容
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 错误描述信息列表
        /// </summary>
        [XmlArray("errInfoList")]
        [XmlArrayItem("errDetailInfo")]
        public List<errDetail> errDetailInfo { get; set; }

        /// <summary>
        /// 错误条数
        /// </summary>
        [XmlElement("errorCount")]
        public int errorCount { get; set; }

        /// <summary>
        /// 响应结果是否错误
        /// </summary>
        public bool IsError {
            get {
                return errorCount > 0;
            }
        }
    }
}
