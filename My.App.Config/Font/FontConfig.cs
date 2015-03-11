using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using My.App.Config.IConfig;

namespace My.App.Config.Font
{
    /// <summary>
    /// 字体配置类
    /// </summary>
    [Serializable]
    public class FontConfig : IConfigInfo
    {
        /// <summary>
        /// 加密信息列表
        /// </summary>
        [XmlArray("FontList")]
        public List<FontInfo> FontInfo { get; set; }
    }
}
