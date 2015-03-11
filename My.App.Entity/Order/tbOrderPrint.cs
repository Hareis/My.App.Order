using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace My.App.Entity.Order
{
    /// <summary>
    /// 订单打印表
    /// </summary>
    [Serializable]
    public class tbOrderPrint
    {
        /// <summary>
        /// 打印编号
        /// </summary>
        public int OrderPrintId { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrdersNumber { get; set; }

        /// <summary>
        /// 打印时间
        /// </summary>
        public DateTime OrderPrintDate { get; set; }

        /// <summary>
        /// 后台操作员
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        public string Detail { get; set; }
    }
}
