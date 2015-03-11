using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Business.Service.YHD.Response;
using My.App.Business.Service.YHD.Util;

namespace My.App.Business.Service.YHD.Request
{
    /// <summary>
    /// 批量查询系列子品信息
    /// </summary>
    public class SerialChildProductsRequest : IRequest<SerialChildProductsResponse>
    {
        /// <summary>
        /// 产品ID列表（逗号分隔）,最大长度为100
        /// <para>选填项</para>
        /// </summary>
        public string productIdList { get; set; }

        /// <summary>
        /// 外部产品ID列表（逗号分隔）,最大长度为100
        /// <para>选填项</para>
        /// </summary>
        public string outerIdList { get; set; }

        /// <summary>
        /// 产品编码列表（逗号分隔）,最大长度为100
        /// <para>选填项</para>
        /// </summary>
        public string productCodeList { get; set; }

        public string GetApiUrl() {
            return "yhd.serial.childproducts.get";
        }

        public IDictionary<string, string> GetParameters() {
            ApiDictionary parameters = new ApiDictionary();
            parameters.Add("productIdList", this.productIdList);
            parameters.Add("outerIdList", this.outerIdList);
            parameters.Add("productCodeList", this.productCodeList);
            return parameters;
        }

        public void Validate() { }
    }
}
