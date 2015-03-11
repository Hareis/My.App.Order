using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace My.App.Entity.Delivery
{
    /// <summary>
    /// 店铺物流关系表
    /// </summary>
    [Serializable]
    public class tbShopLogistics
    {
        /// <summary>
        /// 店铺物流编号
        /// </summary>
        public int SLID { get; set; }

        /// <summary>
        /// 店铺编号
        /// </summary>
        public int ShopId { get; set; }

        /// <summary>
        /// 物流编号
        /// </summary>
        public int LogisticsId { get; set; }

        /// <summary>
        /// 物流信息
        /// </summary>
        public tbLogistics Logistics { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 打印数量
        /// </summary>
        public int Number { get; set; }
    }
}
