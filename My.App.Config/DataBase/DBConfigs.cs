using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace My.App.Config.DataBase
{
    /// <summary>
    /// 数据库配置类
    /// </summary>
    public class DBConfigs
    {
        /// <summary>
        /// 间隔为10分钟
        /// </summary>
        private static System.Timers.Timer EncryptConfigTimer = new System.Timers.Timer(600000);
        private static DataBaseConfig configinfo;

        static DBConfigs() {
            configinfo = DBConfigFileManager.LoadConfig();
            EncryptConfigTimer.AutoReset = true;
            EncryptConfigTimer.Enabled = true;
            EncryptConfigTimer.Elapsed += new System.Timers.ElapsedEventHandler(Timer_Elapsed);
            EncryptConfigTimer.Start();
        }

        private static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
            ResetConfig();
        }

        /// <summary>
        /// 重设配置类实例
        /// </summary>
        public static void ResetConfig() {
            configinfo = DBConfigFileManager.LoadConfig();
        }

        /// <summary>
        /// 获取配置类实例
        /// </summary>
        /// <returns></returns>
        public static DataBaseConfig GetConfig() {
            return configinfo;
        }

        /// <summary>
        /// 保存配置类实例
        /// </summary>
        /// <param name="configinfo">数据库连接信息</param>
        /// <returns></returns>
        public static bool SaveConfig(DataBaseConfig configinfo) {
            DBConfigFileManager rcfm = new DBConfigFileManager();
            DBConfigFileManager.ConfigInfo = configinfo;
            return rcfm.SaveConfig();
        }
    }
}
