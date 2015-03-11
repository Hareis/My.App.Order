using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Business.Service.YHD.Response;
using My.App.Business.Service.YHD.Util;

namespace My.App.Business.Service.YHD.Request
{
    internal class DeliverysCompanyRequest : IRequest<DeliverysCompanyResponse>
    {
        public string GetApiUrl() {
            return "yhd.logistics.deliverys.company.get";
        }

        public IDictionary<string, string> GetParameters() {
            return new ApiDictionary();
        }

        public void Validate() { }
    }
}
