using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Business.Service.YHD.Response;
using My.App.Business.Service.YHD.Util;

namespace My.App.Business.Service.YHD.Request
{
    internal class SerialProductRequest : IRequest<SerialProductResponse> {
        /// <summary>
        /// 1号商城产品ID(系列产品id,与outerId二选一,优先于outerId)
        /// </summary>
        public long? productId { get; set; }

        /// <summary>
        /// 外部产品编码(系列产品outerId,与productId二选一)
        /// </summary>
        public string outerId { get; set; }

        
        public string GetApiUrl() {
            return "yhd.serial.product.get";
        }

        public IDictionary<string, string> GetParameters() {
            ApiDictionary parameters = new ApiDictionary();
            parameters.Add("productId", this.productId);
            parameters.Add("outerId", this.outerId);
            return parameters;
        }

        public void Validate() {
            
        }
    }
}
