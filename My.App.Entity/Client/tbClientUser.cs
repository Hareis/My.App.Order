using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Entity.Shop;

namespace My.App.Entity.Client
{
    /// <summary>
    /// 会员信息表
    /// </summary>
    [Serializable]
    public class tbClientUser
    {
        /// <summary>
        /// 会员编号
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 会员昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 会员头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 会员生日
        /// </summary>
        public string Birthday { get; set; }

        /// <summary>
        /// 会员密码
        /// </summary>
        public string UserPsw { get; set; }

        /// <summary>
        /// 会员性别(1:男2:女3：人妖)
        /// </summary>
        public int Sex { get; set; }

        /// <summary>
        /// 会员信用
        /// </summary>
        public string Credit { get; set; }

        /// <summary>
        /// 会员状态(1:正常2:未激活3:删除4:冻结5:监管)
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 来源平台
        /// </summary>
        public int PfId { get; set; }

        /// <summary>
        /// 外部会员编号
        /// </summary>
        public int OutId { get; set; }

        /// <summary>
        /// 授权码
        /// </summary>
        public string SessionKey { get; set; }
    }
}
