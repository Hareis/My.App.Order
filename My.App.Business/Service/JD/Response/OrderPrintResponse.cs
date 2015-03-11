using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using My.App.Business.Service.JD.Util;

namespace My.App.Business.Service.JD.Response
{
    [Serializable]
    public class OrderPrintResponse : ApiResponse
    {
        [XmlElement("order_printdata")]
        public Entity.PrintData Data { get; set; }
    }
}
