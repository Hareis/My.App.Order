using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace My.App.Business.Service.JD.Entity
{
    /// <summary>
    /// 订单备注信息
    /// </summary>
    [Serializable]
    public class VenderRemark
    {
        /// <summary>
        /// 备注日期
        /// </summary>
        [XmlElement("created")]
        public string created { get; set; }

        /// <summary>
        /// 备注内容
        /// </summary>
        [XmlElement("remark")]
        public string remark { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        [XmlElement("order_id")]
        public string order_id { get; set; }

        /// <summary>
        /// 日期类型
        /// </summary>
        [XmlElement("modified")]
        public string modified { get; set; }

        /// <summary>
        /// 备注颜色标识
        /// </summary>
        [XmlElement("flag")]
        public string flag { get; set; }
    }
}
