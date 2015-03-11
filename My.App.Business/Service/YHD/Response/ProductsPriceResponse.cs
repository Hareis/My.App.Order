using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Business.Service.YHD.Util;
using System.Xml.Serialization;

namespace My.App.Business.Service.YHD.Response
{
    [Serializable]
    public class ProductsPriceResponse : ApiResponse
    {

        [XmlArray("pmPriceList")]
        [XmlArrayItem("pmPrice")]
        public List<Entity.pmPrice> Prices { get; set; }
    }
}
