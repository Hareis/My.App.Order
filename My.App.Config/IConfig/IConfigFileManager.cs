using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace My.App.Config.IConfig
{
    /// <summary>
    /// 配置文件管理类接口
    /// </summary>
    public interface IConfigFileManager
    {
        /// <summary>
        /// 加载配置文件
        /// </summary>
        /// <returns></returns>
        IConfigInfo LoadConfig();


        /// <summary>
        /// 保存配置文件
        /// </summary>
        /// <returns></returns>
        bool SaveConfig();
    }
}
