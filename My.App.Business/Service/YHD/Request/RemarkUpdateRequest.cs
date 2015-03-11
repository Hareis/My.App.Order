using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Business.Service.YHD.Response;
using My.App.Business.Service.YHD.Util;

namespace My.App.Business.Service.YHD.Request
{
    public class RemarkUpdateRequest : IRequest<RemarkUpdateResponse>
    {
        /// <summary>
        /// 订单编码
        /// <para>必填项</para>
        /// </summary>
        public string orderCode { get; set; }

        /// <summary>
        /// 订单卖家备注
        /// <para>最大长度为150个汉字</para>
        /// <para>必填项</para>
        /// </summary>
        public string remark { get; set; }

        public string GetApiUrl() {
            return "yhd.order.merchant.remark.update";
        }

        public IDictionary<string, string> GetParameters() {
            ApiDictionary parameters = new ApiDictionary();
            parameters.Add("orderCode", this.orderCode);
            parameters.Add("remark", this.remark);
            return parameters;
        }

        public void Validate() {
            RequestValidator.ValidateRequired("orderCode", this.orderCode);
            RequestValidator.ValidateRequired("remark", this.remark);
        }
    }
}
