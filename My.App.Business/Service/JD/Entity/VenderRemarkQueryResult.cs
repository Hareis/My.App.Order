using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace My.App.Business.Service.JD.Entity
{
    /// <summary>
    /// 订单备注结果集
    /// </summary>
    [Serializable]
    public class VenderRemarkQueryResult
    {
        /// <summary>
        /// 订单备注信息
        /// </summary>
        [XmlElement("vender_remark")]
        public VenderRemark Remark { get; set; }
    }
}
