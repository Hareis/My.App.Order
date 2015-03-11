using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace My.App.Config.Encrypt
{
    /// <summary>
    /// 加密配置类
    /// </summary>
    [Serializable]
    public class EncryptInfo
    {
        /// <summary>
        /// 加密名称
        /// </summary>
        public string EncryptName { get; set; }

        /// <summary>
        /// 加密类型
        /// </summary>
        [XmlArray("EncryptTypeList")]
        public List<EncryptType> EncryptType { get; set; }
    }
}
