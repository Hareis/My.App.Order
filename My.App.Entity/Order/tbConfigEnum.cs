using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace My.App.Entity.Order
{
    /// <summary>
    /// 订单获取枚举表
    /// </summary>
    [Serializable]
    public class tbConfigEnum
    {
        /// <summary>
        /// 枚举编号
        /// </summary>
        public int EnumId { get; set; }

        /// <summary>
        /// 枚举名称
        /// </summary>
        public string EnumName { get; set; }
    }
}
