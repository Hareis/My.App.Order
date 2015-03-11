using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace My.App.Business.Service.JD.Entity
{
    /// <summary>
    /// 收货人信息
    /// </summary>
    [Serializable]
    public class Consignee
    {
        /// <summary>
        /// 省
        /// </summary>
        [XmlElement("province")]
        public string province { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        [XmlElement("city")]
        public string city { get; set; }

        /// <summary>
        /// 县
        /// </summary>
        [XmlElement("county")]
        public string county { get; set; }

        /// <summary>
        /// 收货人姓名
        /// </summary>
        [XmlElement("fullname")]
        public string user_name { get; set; }

        /// <summary>
        /// 收货人地址
        /// </summary>
        [XmlElement("full_address")]
        public string user_address { get; set; }

        /// <summary>
        /// 收货人固定电话
        /// </summary>
        [XmlElement("telephone")]
        public string user_telephone { get; set; }

        /// <summary>
        /// 收货人手机
        /// </summary>
        [XmlElement("mobile")]
        public string user_mobilephone { get; set; }
    }
}
