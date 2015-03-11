using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace My.App.Entity.Order
{
    /// <summary>
    /// 订单汇总信息
    /// </summary>
    [Serializable]
    public class OrderCollect
    {
        /// <summary>
        /// 总用户数
        /// </summary>
        public int MaxUser { get; set; }

        /// <summary>
        /// 总订单数
        /// </summary>
        public int MaxOrder { get; set; }

        /// <summary>
        /// 待打印订单数
        /// </summary>
        public int NPrintOrder { get; set; }

        /// <summary>
        /// 已打印订单数
        /// </summary>
        public int YPrintOrder { get; set; }

        /// <summary>
        /// 订单总金额
        /// </summary>
        public decimal MaxPrice { get; set; }
    }
}
