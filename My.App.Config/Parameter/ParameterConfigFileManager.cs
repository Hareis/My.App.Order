using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Config.Provider;
using System.IO;
using My.App.Config.IConfig;

namespace My.App.Config.Parameter
{
    /// <summary>
    /// 字体配置管理类
    /// </summary>
    public class ParameterConfigFileManager : DefaultConfigFileManager
    {
        private static ParameterConfig configinfo;
        private static DateTime fileoldchange;                                                                                                  //文件修改时间
        public static string filename = null;                                                                                                   //配置文件所在路径

        /// <summary>
        /// 初始化文件修改时间和对象实例
        /// </summary>
        static ParameterConfigFileManager() {
            if (File.Exists(ConfigFilePath)) {
                fileoldchange = File.GetLastWriteTime(ConfigFilePath);
                configinfo = (ParameterConfig)DefaultConfigFileManager.DeserializeInfo(ConfigFilePath, typeof(ParameterConfig));
            }
        }

        /// <summary>
        /// 当前的配置类实例
        /// </summary>
        public new static IConfigInfo ConfigInfo {
            get { return configinfo; }
            set { configinfo = (ParameterConfig)value; }
        }


        /// <summary>
        /// 获取配置文件所在路径
        /// </summary>
        public new static string ConfigFilePath {
            get {
                if (filename == null) {
                    filename = PathConfig.ParameterPath;
                }
                return filename;
            }
        }

        /// <summary>
        /// 返回配置类实例
        /// </summary>
        /// <returns></returns>
        public static ParameterConfig LoadConfig() {
            if (File.Exists(ConfigFilePath)) {
                ConfigInfo = DefaultConfigFileManager.LoadConfig(ref fileoldchange, ConfigFilePath, ConfigInfo);
                return ConfigInfo as ParameterConfig;
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
