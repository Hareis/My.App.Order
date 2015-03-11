using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace My.App.Business.Service
{
    /// <summary>
    /// 返回状态表
    /// </summary>
    [Serializable]
    internal class StatusTable
    {
        /// <summary>
        /// 状态信息
        /// </summary>
        internal bool IsSuccess { get; set; }

        /// <summary>
        /// 是否在执行过程中发生了错误
        /// </summary>
        internal bool IsError { get; set; }

        /// <summary>
        /// 描述信息
        /// </summary>
        internal string Msg { get; set; }
    }
}
