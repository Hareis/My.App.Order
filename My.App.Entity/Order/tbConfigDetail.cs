using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace My.App.Entity.Order
{
    /// <summary>
    /// 订单获取配置详情表
    /// </summary>
    [Serializable]
    public class tbConfigDetail
    {
        /// <summary>
        /// 配置详情编号
        /// </summary>
        public int DetailId { get; set; }

        /// <summary>
        /// 配置编号
        /// </summary>
        public int ConfigId { get; set; }

        /// <summary>
        /// 枚举编号
        /// </summary>
        public int EnumId { get; set; }

        /// <summary>
        /// 键
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
    }
}
