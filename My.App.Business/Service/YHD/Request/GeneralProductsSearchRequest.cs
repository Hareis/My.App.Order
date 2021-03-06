﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Business.Service.YHD.Response;
using My.App.Business.Service.YHD.Util;

namespace My.App.Business.Service.YHD.Request
{
    public class GeneralProductsSearchRequest : IRequest<GeneralProductsSearchResponse>
    {
        /// <summary>
        /// 是否可见(强制'上'下架),1是0否
        /// </summary>
        public int? canShow { get; set; }

        /// <summary>
        /// 上下架状态0：下架，1：上架
        /// </summary>
        public int? canSale { get; set; }

        /// <summary>
        /// 产品审核状态:1.未审核;2.审核通过;3.审核失败
        /// </summary>
        public int? verifyFlg { get; set; }

        /// <summary>
        /// 1号商城产品ID列表(逗号分隔,优先于outerIdList),最多100个
        /// </summary>
        public string productIdList { get; set; }

        /// <summary>
        /// 外部产品编码列表(逗号分隔),最多100个
        /// </summary>
        public string outerIdList { get; set; }

        /// <summary>
        /// 产品编码列表（逗号分隔）最大长度为100
        /// </summary>
        public string productCodeList { get; set; }

        /// <summary>
        /// 品牌类别ID
        /// </summary>
        public string categoryId { get; set; }

        /// <summary>
        /// 产品类别类型（0:1号店类别,1:商家自定义类别,默认为0）
        /// </summary>
        public string categoryType { get; set; }

        /// <summary>
        /// 品牌ID
        /// </summary>
        public string brandId { get; set; }

        /// <summary>
        /// 产品中文名称
        /// </summary>
        public string productCname { get; set; }

        /// <summary>
        /// 当前页数(默认1)
        /// </summary>
        public long? curPage { get; set; }

        /// <summary>
        /// 每页显示记录数(默认50、最大限制：100)
        /// </summary>
        public long? pageRows { get; set; }

        public string GetApiUrl() {
            return "yhd.general.products.search";
        }

        public IDictionary<string, string> GetParameters() {
            ApiDictionary parameters = new ApiDictionary();
            parameters.Add("canShow", this.canShow);
            parameters.Add("canSale", this.canSale);
            parameters.Add("verifyFlg", this.verifyFlg);
            parameters.Add("productIdList", this.productIdList);
            parameters.Add("productCodeList", this.productCodeList);
            parameters.Add("outerIdList", this.outerIdList);
            parameters.Add("productCname", this.productCname);
            parameters.Add("categoryId", this.categoryId);
            parameters.Add("categoryType", this.categoryType);
            parameters.Add("brandId", this.brandId);
            parameters.Add("curPage", this.curPage);
            parameters.Add("pageRows", this.pageRows);
            return parameters;
        }

        public void Validate() {
            
        }
    }
}
