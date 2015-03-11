using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace My.App.Entity.Order
{
    /// <summary>
    /// 买家信息表
    /// </summary>
    [Serializable]
    public class tbBuyerInfo
    {
        /// <summary>
        /// 买家编号
        /// </summary>
        public int BuyerId { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrdersNumber { get; set; }

        /// <summary>
        /// 买家昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 买家姓名
        /// </summary>
        public string BuyerName { get; set; }

        /// <summary>
        /// 买家手机
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 买家电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 买家邮箱
        /// </summary>
        public string BuyerEmail { get; set; }
    }
}
