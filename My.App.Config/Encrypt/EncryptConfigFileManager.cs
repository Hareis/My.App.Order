using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Config.Provider;
using System.IO;
using My.App.Config.IConfig;

namespace My.App.Config.Encrypt
{
    /// <summary>
    /// 加密模式管理类
    /// </summary>
    public class EncryptConfigFileManager : DefaultConfigFileManager
    {
        private static EncryptConfig configinfo;
        private static DateTime fileoldchange;                                                                                                  //文件修改时间
        public static string filename = null;                                                                                                   //配置文件所在路径

        /// <summary>
        /// 初始化文件修改时间和对象实例
        /// </summary>
        static EncryptConfigFileManager() {
            if (File.Exists(ConfigFilePath)) {
                fileoldchange = File.GetLastWriteTime(ConfigFilePath);
                configinfo = (EncryptConfig)DefaultConfigFileManager.DeserializeInfo(ConfigFilePath, typeof(EncryptConfig));
            }
        }

        /// <summary>
        /// 当前的配置类实例
        /// </summary>
        public new static IConfigInfo ConfigInfo {
            get { return configinfo; }
            set { configinfo = (EncryptConfig)value; }
        }


        /// <summary>
        /// 获取配置文件所在路径
        /// </summary>
        public new static string ConfigFilePath {
            get {
                if (filename == null) {
                    filename = PathConfig.EncryptPath;
                }
                return filename;
            }
        }

        /// <summary>
        /// 返回配置类实例
        /// </summary>
        /// <returns></returns>
        public static EncryptConfig LoadConfig() {
            if (File.Exists(ConfigFilePath)) {
                ConfigInfo = DefaultConfigFileManager.LoadConfig(ref fileoldchange, ConfigFilePath, ConfigInfo);
                return ConfigInfo as EncryptConfig;
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
