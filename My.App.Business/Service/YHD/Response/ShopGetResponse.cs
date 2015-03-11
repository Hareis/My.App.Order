using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Business.Service.YHD.Util;
using System.Xml.Serialization;

namespace My.App.Business.Service.YHD.Response
{
    public class ShopGetResponse : ApiResponse
    {
        /// <summary>
        /// 查询成功记录数
        /// </summary>
        [XmlElement("totalCount")]
        public string totalCount { get; set; }

        /// <summary>
        /// 店铺信息
        /// </summary>
        [XmlElement("storeMerchantStoreInfo")]
        public Entity.ShopInfo Shop { get; set; }
    }
}
