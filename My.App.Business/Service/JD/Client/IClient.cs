using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Business.Service.JD.Request;
using My.App.Business.Service.JD.Util;

namespace My.App.Business.Service.JD.Client
{
    internal interface IClient
    {
        /// <summary>
        /// 执行API请求。
        /// </summary>
        /// <typeparam name="T">领域对象</typeparam>
        /// <param name="request">具体的API请求</param>
        /// <returns>领域对象</returns>
        T Execute<T>(IRequest<T> request) where T : ApiResponse;
    }
}
