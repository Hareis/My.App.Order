using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace My.App.Business.Service.JD.Entity
{
    /// <summary>
    /// 物流公司信息
    /// </summary>
    [Serializable]
    public class ApiCompanyCustom
    {
        /// <summary>
        /// 物流公司ID
        /// </summary>
        [XmlElement("logistics_id")]
        public string logistics_id { get; set; }

        /// <summary>
        /// 物流公司名称
        /// </summary>
        [XmlElement("logistics_name")]
        public string logistics_name { get; set; }

        /// <summary>
        /// 备注说明
        /// </summary>
        [XmlElement("logistics_remark")]
        public string logistics_remark { get; set; }

        /// <summary>
        /// 序列
        /// </summary>
        [XmlElement("sequence")]
        public string sequence { get; set; }
    }
}
