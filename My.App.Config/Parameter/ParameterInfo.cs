using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace My.App.Config.Parameter
{
    /// <summary>
    /// 参数列表类
    /// </summary>
    [Serializable]
    public class ParameterInfo
    {
        /// <summary>
        /// 平台名称
        /// </summary>
        [XmlElement("Name")]
        public string Name { get; set; }

        /// <summary>
        /// 参数列表
        /// </summary>
        [XmlArray("KeyValueList")]
        [XmlArrayItem("KeyValuePair")]
        public List<KVInfo> KVList { get; set; }
    }
}
