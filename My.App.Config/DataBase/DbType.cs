using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace My.App.Config.DataBase
{
    /// <summary>
    /// 数据库类型枚举
    /// </summary>
    public enum DbType
    {
        /// <summary>
        /// SqlServer数据库
        /// </summary>
        SQLSERVER,
        /// <summary>
        /// Oracle数据库
        /// </summary>
        ORACLE,
        /// <summary>
        /// MySql数据库
        /// </summary>
        MYSQL,
        /// <summary>
        /// Access小型数据库
        /// </summary>
        ACCESS
    }
}
