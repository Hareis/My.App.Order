using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Business.Service.JD.Response;
using My.App.Business.Service.JD.Util;

namespace My.App.Business.Service.JD.Request
{
    internal class WayBillCodeGetRequest : IRequest<WayBillCodeGetResponse>
    {
        /// <summary>
        /// <para>获取运单号数量（最大100）</para>
        /// <para>必填项</para>
        /// </summary>
        public string preNum { get; set; }

        /// <summary>
        /// <para>商家编码</para>
        /// <para>必填项</para>
        /// </summary>
        public string customerCode { get; set; }

        public string GetApiUrl() {
            return "jingdong.etms.waybillcode.get";
        }

        public IDictionary<string, string> GetParameters() {
            ApiDictionary parameters = new ApiDictionary();
            parameters.Add("preNum", this.preNum);
            parameters.Add("customerCode", this.customerCode);
            return parameters;
        }

        public void Validate() {
            RequestValidator.ValidateRequired("preNum", this.preNum);
            RequestValidator.ValidateRequired("customerCode", this.customerCode);
        }
    }
}
