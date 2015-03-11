using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Business.Service.YHD.Util;
using System.Xml.Serialization;

namespace My.App.Business.Service.YHD.Response
{
    [Serializable]
    public class SerialProductResponse : ApiResponse
    {
        [XmlArray("serialChildProdList")]
        [XmlArrayItem("serialChildProd")]
        public List<Entity.SerialChildProd> Products { get; set; }
    }
}
