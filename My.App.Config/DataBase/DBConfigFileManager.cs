using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Config.Provider;
using System.IO;
using My.App.Config.IConfig;

namespace My.App.Config.DataBase
{
    /// <summary>
    /// 数据库连接管理类
    /// </summary>
    public class DBConfigFileManager : DefaultConfigFileManager
    {
        private static DataBaseConfig configinfo;
        private static DateTime fileoldchange;                                                                                                  //文件修改时间
        public static string filename = null;                                                                                                   //配置文件所在路径

        /// <summary>
        /// 初始化文件修改时间和对象实例
        /// </summary>
        static DBConfigFileManager() {
            if (File.Exists(ConfigFilePath)) {
                fileoldchange = File.GetLastWriteTime(ConfigFilePath);
                configinfo = (DataBaseConfig)DefaultConfigFileManager.DeserializeInfo(ConfigFilePath, typeof(DataBaseConfig));
            }
        }

        /// <summary>
        /// 当前的配置类实例
        /// </summary>
        public new static IConfigInfo ConfigInfo {
            get { return configinfo; }
            set { configinfo = (DataBaseConfig)value; }
        }


        /// <summary>
        /// 获取配置文件所在路径
        /// </summary>
        public new static string ConfigFilePath {
            get {
                if (filename == null) {
                    filename = PathConfig.DataBasePath;
                }
                return filename;
            }
        }

        /// <summary>
        /// 返回配置类实例
        /// </summary>
        /// <returns></returns>
        public static DataBaseConfig LoadConfig() {
            if (File.Exists(ConfigFilePath)) {
                ConfigInfo = DefaultConfigFileManager.LoadConfig(ref fileoldchange, ConfigFilePath, ConfigInfo);
                return ConfigInfo as DataBaseConfig;
            } else {
                return null;
            }
        }

        /// <summary>
        /// 保存配置类实例
        /// </summary>
        /// <returns></returns>
        public override bool SaveConfig() {
            return base.SaveConfig(ConfigFilePath, ConfigInfo);
        }
    }
}
