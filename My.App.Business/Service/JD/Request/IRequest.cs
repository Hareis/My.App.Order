using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Business.Service.JD.Util;

namespace My.App.Business.Service.JD.Request
{
    /// <summary>
    /// API请求接口。
    /// </summary>
    internal interface IRequest<T> where T : ApiResponse
    {
        /// <summary>
        /// 获取API方法名称
        /// </summary>
        /// <returns></returns>
        string GetApiUrl();

        /// <summary>
        /// 获取所有的Key-Value形式的文本请求参数字典。其中：
        /// Key: 请求参数名
        /// Value: 请求参数文本值
        /// </summary>
        /// <returns>文本请求参数字典</returns>
        IDictionary<string, string> GetParameters();

        /// <summary>
        /// 提前验证参数。
        /// </summary>
        void Validate();
    }
}
