using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Business.Service.JD.Response;
using My.App.Business.Service.JD.Util;

namespace My.App.Business.Service.JD.Request
{
    /// <summary>
    /// 获取商品上架的商品信息
    /// </summary>
    internal class WareListingGetRequest : IRequest<WareListingGetResponse>
    {
        /// <summary>
        /// 类目id
        /// </summary>
        public int? cid { get; set; }

        /// <summary>
        /// 起始的修改时间
        /// </summary>
        public string start_modified { get; set; }

        /// <summary>
        /// 结束的修改时间
        /// </summary>
        public string end_modified { get; set; }

        /// <summary>
        /// 分页
        /// </summary>
        public long? page { get; set; }

        /// <summary>
        /// 每页多少条
        /// </summary>
        public long? page_size { get; set; }

        public string GetApiUrl() {
            return "360buy.ware.listing.get";
        }

        public IDictionary<string, string> GetParameters() {
            ApiDictionary parameters = new ApiDictionary();
            parameters.Add("cid", this.cid);
            parameters.Add("start_modified", this.start_modified);
            parameters.Add("end_modified", this.end_modified);
            parameters.Add("page", this.page);
            parameters.Add("page_size", this.page_size);
            return parameters;
        }

        public void Validate() {
            RequestValidator.ValidateRequired("page", this.page);
            RequestValidator.ValidateRequired("page_size", this.page_size);
        }
    }
}
