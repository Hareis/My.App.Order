using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace My.App.Business.Service.YHD.Parser
{
    /// <summary>
    /// API响应解释器接口。响应格式可以是XML, JSON等等。
    /// </summary>
    /// <typeparam name="T">领域对象</typeparam>
    internal interface IParser<T>
    {
        /// <summary>
        /// 把响应字符串解释成相应的领域对象。
        /// </summary>
        /// <param name="body">响应字符串</param>
        /// <returns>领域对象</returns>
        T Parse(string body);
    }
}
