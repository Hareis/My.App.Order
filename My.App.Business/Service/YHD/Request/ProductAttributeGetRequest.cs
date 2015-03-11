using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Business.Service.YHD.Response;
using My.App.Business.Service.YHD.Util;

namespace My.App.Business.Service.YHD.Request
{
    public class ProductAttributeGetRequest : IRequest<ProductAttributeGetResponse>
    {
        /// <summary>
        /// 一号店产品ID（逗号分隔，优先使用）与outerId二选一
        /// <para>选填项</para>
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// 外部产品ID（逗号分隔）与productId二选一
        /// <para>选填项</para>
        /// </summary>
        public string outerId { get; set; }

        public string GetApiUrl() {
            return "yhd.serial.product.attribute.get";
        }

        public IDictionary<string, string> GetParameters() {
            ApiDictionary parameters = new ApiDictionary();
            parameters.Add("productId", this.productId);
            parameters.Add("outerId", this.outerId);
            return parameters;
        }

        public void Validate() { }
    }
}
