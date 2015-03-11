using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace My.App.Entity.Delivery
{
    public class tbLogistics
    {
        /// <summary>
        /// 物流信息编号
        /// </summary>
        public int LogisticsId { get; set; }

        /// <summary>
        /// 物流编码
        /// </summary>
        public string LogisticsCode { get; set; }

        /// <summary>
        /// 物流名称
        /// </summary>
        public string LogisticsName { get; set; }

        /// <summary>
        /// 物流公司对应的运单规则
        /// </summary>
        public string LogisticsReg { get; set; }

        /// <summary>
        /// 面单图片
        /// </summary>
        public string LogisticsImg { get; set; }

        /// <summary>
        /// 面单宽度(毫米)
        /// </summary>
        public int LogisticsWdith { get; set; }

        /// <summary>
        /// 面单高度(毫米)
        /// </summary>
        public int LogisticsHeight { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Detail { get; set; }
    }
}
