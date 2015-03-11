using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Entity.Delivery;

namespace My.App.Entity.Order
{
    /// <summary>
    /// 发货单表
    /// </summary>
    [Serializable]
    public class tbOrderShipping
    {
        /// <summary>
        /// 发货单编号
        /// </summary>
        public int ShippingId { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrdersNumber { get; set; }

        /// <summary>
        /// 配送方式
        /// </summary>
        public tbLogistics Logistics { get; set; }

        /// <summary>
        /// 物流单号
        /// </summary>
        public string LogisticsNumber { get; set; }

        /// <summary>
        /// 后台审核员
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 发货时间
        /// </summary>
        public DateTime ShippingDate { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        public string ShippingDetail { get; set; }
    }
}
