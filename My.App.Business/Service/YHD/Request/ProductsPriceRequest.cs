using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Business.Service.YHD.Response;
using My.App.Business.Service.YHD.Util;

namespace My.App.Business.Service.YHD.Request
{
    internal class ProductsPriceRequest : IRequest<ProductsPriceResponse> {
        /// <summary>
        /// 1号商城产品ID列表（逗号分隔）,与outerIdList二选一,最大长度为100
        /// </summary>
        public string productIdList { get; set; }

        /// <summary>
        /// 外部产品ID列表（逗号分隔）,与productIdList二选一,最大长度为100
        /// </summary>
        public string outerIdList { get; set; }

        public string GetApiUrl() {
            return "yhd.products.price.get";
        }

        public IDictionary<string, string> GetParameters() {
            ApiDictionary parameters = new ApiDictionary();
            parameters.Add("productIdList", this.productIdList);
            parameters.Add("outerIdList", this.outerIdList);
            return parameters;
        }

        public void Validate() {
            
        }
    }
}
