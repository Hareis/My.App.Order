using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Config.IConfig;
using System.IO;
using My.App.Config.Utils;

namespace My.App.Config.Provider
{
    /// <summary>
    /// 文件配置管理基类
    /// </summary>
    public class DefaultConfigFileManager
    {
        private static string configfilepath;                                                                                           //文件所在路径
        private static IConfigInfo configinfo = null;                                                                                   //临时配置对象

        /// <summary>
        /// 锁对象
        /// </summary>
        private static object LockHelper = new object();

        /// <summary>
        /// 获取或设置文件所在路径
        /// </summary>
        public static string ConfigFilePath {
            get { return configfilepath; }
            set { configfilepath = value; }
        }

        /// <summary>
        /// 获取或设置临时配置对象
        /// </summary>
        public static IConfigInfo ConfigInfo {
            get { return configinfo; }
            set { configinfo = value; }
        }

        /// <summary>
        /// 加载(反序列化)指定对象类型的配置对象
        /// </summary>
        /// <param name="FileOldChange">文件加载时间</param>
        /// <param name="ConfigFilePath">配置文件所在路径</param>
        /// <param name="ConfigInfo">相应的变量 注:该参数主要用于设置m_configinfo变量 和 获取类型.GetType()</param>
        /// <returns></returns>
        protected static IConfigInfo LoadConfig(ref DateTime FileOldChange, string ConfigFilePath, IConfigInfo ConfigInfo) {
            return LoadConfig(ref FileOldChange, ConfigFilePath, ConfigInfo, true);
        }

        /// <summary>
        /// 加载(反序列化)指定对象类型的配置对象
        /// </summary>
        /// <param name="FileOldChange">文件加载时间</param>
        /// <param name="ConfigFilePath">配置文件所在路径(包括文件名)</param>
        /// <param name="ConfigInfo">相应的变量 注:该参数主要用于设置m_configinfo变量 和 获取类型.GetType()</param>
        /// <param name="checkTime">是否检查并更新传递进来的"文件加载时间"变量</param>
        /// <returns></returns>
        protected static IConfigInfo LoadConfig(ref DateTime FileOldChange, string ConfigFilePath, IConfigInfo ConfigInfo, bool CheckTime) {
            lock (LockHelper) {
                configfilepath = ConfigFilePath;
                configinfo = ConfigInfo;

                if (CheckTime) {
                    DateTime filenewchange = File.GetLastWriteTime(ConfigFilePath);

                    //当程序运行中config文件发生变化时则对config重新赋值
                    if (FileOldChange != filenewchange) {
                        FileOldChange = filenewchange;
                        configinfo = DeserializeInfo(ConfigFilePath, ConfigInfo.GetType());
                    }
                } else {
                    configinfo = DeserializeInfo(ConfigFilePath, ConfigInfo.GetType());
                }

                return configinfo;
            }
        }

        /// <summary>
        /// 反序列化指定的类
        /// </summary>
        /// <param name="ConfigFilePath">config 文件的路径</param>
        /// <param name="ConfigType">相应的类型</param>
        /// <returns></returns>
        public static IConfigInfo DeserializeInfo(string ConfigFilePath, Type ConfigType) {
            return (IConfigInfo)SerializationHelper.Load(ConfigType, ConfigFilePath);
        }

        /// <summary>
        /// 保存配置实例(虚方法需继承)
        /// </summary>
        /// <returns></returns>
        public virtual bool SaveConfig() {
            return true;
        }

        /// <summary>
        /// 保存(序列化)指定路径下的配置文件
        /// </summary>
        /// <param name="ConfigFilePath">指定的配置文件所在的路径(包括文件名)</param>
        /// <param name="ConfigInfo">被保存(序列化)的对象</param>
        /// <returns></returns>
        public bool SaveConfig(string ConfigFilePath, IConfigInfo ConfigInfo) {
            return SerializationHelper.Save(ConfigInfo, ConfigFilePath);
        }
    }
}
