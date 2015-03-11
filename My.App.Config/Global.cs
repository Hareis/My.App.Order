using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace My.App.Config
{
    /// <summary>
    /// 全局配置类
    /// </summary>
    public class Global
    {
        /// <summary>
        /// 登陆授权页面地址
        /// </summary>
        public static string Url = ConfigurationManager.AppSettings["Url"];

        /// <summary>
        /// 一号店请求授权页面地址
        /// </summary>
        public static string YHD_Authorize_Url = ConfigurationManager.AppSettings["Y_Authorize_Url"];

        /// <summary>
        /// 京东请求授权页面地址
        /// </summary>
        public static string JD_Authorize_Url = ConfigurationManager.AppSettings["J_Authorize_Url"];
    }
}
