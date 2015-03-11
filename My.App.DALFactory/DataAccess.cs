using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;
using My.App.Interface;
using My.App.Config.DataBase;

namespace My.App.DALFactory
{
    public sealed class DataAccess
    {
        private static readonly DataBaseConfig Config = DBConfigs.GetConfig();

        private static readonly string[] path = (Config.DataBaseType == DbType.ACCESS ? ConfigurationManager.AppSettings["ACCESS"].Split(',') : Config.DataBaseType == DbType.MYSQL ? ConfigurationManager.AppSettings["MYSQL"].Split(',') : Config.DataBaseType == DbType.SQLSERVER ? ConfigurationManager.AppSettings["SQLSERVER"].Split(',') : ConfigurationManager.AppSettings["ORACLE"].Split(','));

        public static BaseInterface Create(string ClsName) {
            string ClassName = path[1] + "." + ClsName;
            return (BaseInterface)Assembly.Load(path[0]).CreateInstance(ClassName);
        }
    }
}
