using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace My.App.Entity.Client
{
    /// <summary>
    /// 用户反馈表
    /// </summary>
    [Serializable]
    public class tbFeedback
    {
        /// <summary>
        /// 反馈编号
        /// </summary>
        public int FeedbackId { get; set; }
        
        /// <summary>
        /// 反馈内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 联系邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public int UserId { get; set; }
    }
}
