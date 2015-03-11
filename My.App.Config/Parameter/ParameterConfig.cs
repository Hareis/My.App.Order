using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Config.IConfig;
using System.Xml.Serialization;

namespace My.App.Config.Parameter
{
    /// <summary>
    /// API参数配置类
    /// </summary>
    [Serializable]
    public class ParameterConfig : IConfigInfo
    {
        /// <summary>
        /// 参数信息列表
        /// </summary>
        [XmlArray("ParameterList")]
        [XmlArrayItem("ParameterInfo")]
        public List<ParameterInfo> Parameters { get; set; }
    }
}
