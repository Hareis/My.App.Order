using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Config.IConfig;

namespace My.App.Config.DataBase
{
    /// <summary>
    /// 数据库连接信息
    /// </summary>
    [Serializable]
    public class DataBaseConfig : IConfigInfo
    {
        /// <summary>
        /// 服务器地址
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// 数据库名称
        /// </summary>
        public string DBName { get; set; }

        /// <summary>
        /// 登录帐号
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// 登录口令
        /// </summary>
        public string PassWord { get; set; }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public DbType DataBaseType { get; set; }

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string Character { get; set; }
    }
}
