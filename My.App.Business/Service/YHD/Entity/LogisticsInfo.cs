using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace My.App.Business.Service.YHD.Entity
{
    /// <summary>
    /// 物流公司信息
    /// </summary>
    [Serializable]
    public class LogisticsInfo
    {
        /// <summary>
        /// 快递公司编号
        /// </summary>
        [XmlElement("id")]
        public long id { get; set; }

        /// <summary>
        /// 快递公司名称
        /// </summary>
        [XmlElement("companyName")]
        public string companyName { get; set; }

        /// <summary>
        /// 快递公司查询地址
        /// </summary>
        [XmlElement("queryURL")]
        public string queryURL { get; set; }

        /// <summary>
        /// 状态   0:启用 1:关闭
        /// </summary>
        [XmlElement("status")]
        public int status { get; set; }
    }
}
