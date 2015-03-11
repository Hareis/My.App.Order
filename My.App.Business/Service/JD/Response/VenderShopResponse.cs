using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Business.Service.JD.Util;
using System.Xml.Serialization;
using My.App.Business.Service.JD.Entity;

namespace My.App.Business.Service.JD.Response
{
    public class VenderShopResponse : ApiResponse
    {
        /// <summary>
        /// 店铺信息
        /// </summary>
        [XmlElement("shop_jos_result")]
        public ShopInfo Shop { get; set; }
    }
}
