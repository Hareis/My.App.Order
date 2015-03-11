using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Business.Service.JD.Response;
using My.App.Business.Service.JD.Util;

namespace My.App.Business.Service.JD.Request
{
    /// <summary>
    /// 获取商品基本信息
    /// </summary>
    internal class ProductGetRequest : IRequest<ProductGetResponse>
    {
        /// <summary>
        /// SKU编号列表,多个sku编号用英文逗号分隔（每次调用支持50个sku的查询）
        /// </summary>
        public string ids { get; set; }

        /// <summary>
        /// 需要查询的字段，与返回值ProductBase中的字段对应
        /// </summary>
        public string filed { get; set; }

        public string GetApiUrl() {
            return "jingdong.ware.baseproduct.get";
        }

        public IDictionary<string, string> GetParameters() {
            ApiDictionary parameters = new ApiDictionary();
            parameters.Add("ids", this.ids);
            parameters.Add("base", filed);
            return parameters;
        }

        public void Validate() {
            RequestValidator.ValidateRequired("ids", this.ids);
            RequestValidator.ValidateRequired("base", this.filed);
        }
    }
}
