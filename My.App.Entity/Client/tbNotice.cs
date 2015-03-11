using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace My.App.Entity.Client
{
    /// <summary>
    /// 通知信息表
    /// </summary>
    [Serializable]
    public class tbNotice
    {
        /// <summary>
        /// 通知编号
        /// </summary>
        public int NoticeId { get; set; }

        /// <summary>
        /// 通知内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}
