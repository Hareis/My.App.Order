using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace My.App.Entity.Order
{
    /// <summary>
    /// 配货单
    /// </summary>
    [Serializable]
    public class Distribution
    {
        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 商家编码
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// 销售属性
        /// </summary>
        public string ProductAtt { get; set; }

        /// <summary>
        /// 商品数量
        /// </summary>
        public int ProductNumber { get; set; }
    }
}
