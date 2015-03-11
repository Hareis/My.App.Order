using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace My.App.Config.Parameter
{
    /// <summary>
    /// 参数信息类
    /// </summary>
    [Serializable]
    public class KVInfo
    {
        /// <summary>
        /// 键
        /// </summary>
        [XmlElement("Key")]
        public string Key { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [XmlElement("Value")]
        public string Value { get; set; }
    }
}
