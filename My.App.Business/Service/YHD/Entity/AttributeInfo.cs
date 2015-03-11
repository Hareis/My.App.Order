using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace My.App.Business.Service.YHD.Entity
{
    /// <summary>
    /// 属性信息
    /// </summary>
    [Serializable]
    public class AttributeInfo
    {
        /// <summary>
        /// 自定义颜色名称 注：输入参数为虚品时，只返回主品的自定义颜色名称
        /// </summary>
        [XmlElement("customColorName")]
        public string customColorName { get; set; }

        /// <summary>
        /// 自定义尺码名称 注：输入参数为虚品时，只返回主品的自定义尺码名称
        /// </summary>
        [XmlElement("customSizeName")]
        public string customSizeName { get; set; }
    }
}
