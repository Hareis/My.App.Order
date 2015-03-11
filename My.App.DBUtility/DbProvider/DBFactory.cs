using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using My.App.Config.DataBase;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Data.OracleClient;
using System.Data.OleDb;

namespace My.App.DBUtility.DbProvider
{
    /// <summary>
    /// 数据提供类
    /// </summary>
    internal class DBFactory
    {
        private static readonly DataBaseConfig Connection = DBConfigs.GetConfig();
        private static string connectionstring = "";

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static string ConnectionString {
            get { 
                if(String.IsNullOrEmpty(connectionstring)) {
                    connectionstring = Connection.Character.Replace("{DBName}", Connection.DBName).Replace("{Server}", Connection.Server).Replace("{User}", Connection.User).Replace("{PassWord}", Tools.Encrypt.Encrypting.Decode(Connection.PassWord, Tools.Encrypt.EncryptMode.Data));
                }

                return connectionstring;
            }
        }

        /// <summary>
        /// 获取命令参数符号
        /// Sql："@"
        /// Oracle：":"
        /// MySql ： ""
        /// Access ： "@"
        /// </summary>
        /// <returns></returns>
        private static string CreateParmCharacter() {
            string character = string.Empty;
            switch (Connection.DataBaseType) { 
                case Config.DataBase.DbType.SQLSERVER:
                    character = "@";
                    break;
                case Config.DataBase.DbType.ORACLE: ;
                    character = ":";
                    break;
                case Config.DataBase.DbType.MYSQL:
                    character = "";
                    break;
                case Config.DataBase.DbType.ACCESS:
                    character = "@";
                    break;
                default:
                    throw new Exception("不支持的数据库类型");
            }

            return character;
        }

        /// <summary>
        /// 创建数据库连接对象
        /// </summary>
        /// <returns></returns>
        public static IDbConnection CreateConnection() {
            IDbConnection conn = null;
            switch (Connection.DataBaseType) {
                case Config.DataBase.DbType.SQLSERVER:
                    conn = new SqlConnection(ConnectionString);
                    break;
                case Config.DataBase.DbType.ORACLE:
                    conn = new OracleConnection(ConnectionString);
                    break;
                case Config.DataBase.DbType.MYSQL:
                    conn = new MySqlConnection(ConnectionString);
                    break;
                case Config.DataBase.DbType.ACCESS:
                    conn = new OleDbConnection(ConnectionString);
                    break;
                default:
                    throw new Exception("不支持的数据库类型");
            }

            return conn;
        }

        /// <summary>
        /// 创建数据库连接对象
        /// </summary>
        /// <returns></returns>
        public static IDbConnection CreateConnection(string ConnString, Config.DataBase.DbType Type) {
            IDbConnection conn = null;
            switch (Type) {
                case Config.DataBase.DbType.SQLSERVER:
                    conn = new SqlConnection(ConnString);
                    break;
                case Config.DataBase.DbType.ORACLE:
                    conn = new OracleConnection(ConnString);
                    break;
                case Config.DataBase.DbType.MYSQL:
                    conn = new MySqlConnection(ConnString);
                    break;
                case Config.DataBase.DbType.ACCESS:
                    conn = new OleDbConnection(ConnString);
                    break;
                default:
                    throw new Exception("不支持的数据库类型");
            }

            return conn;
        }

        /// <summary>
        /// 创建数据库命令对象
        /// </summary>
        /// <returns></returns>
        public static IDbCommand CreateCommand() {
            IDbCommand cmd = null;
            switch (Connection.DataBaseType) {
                case Config.DataBase.DbType.SQLSERVER:
                    cmd = new SqlCommand();
                    break;
                case Config.DataBase.DbType.ORACLE:
                    cmd = new OracleCommand();
                    break;
                case Config.DataBase.DbType.MYSQL:
                    cmd = new MySqlCommand();
                    break;
                case Config.DataBase.DbType.ACCESS:
                    cmd = new OleDbCommand();
                    break;
                default:
                    throw new Exception("不支持的数据库类型");
            }

            return cmd;
        }

        /// <summary>
        /// 创建数据库命令对象
        /// </summary>
        /// <returns></returns>
        public static IDataAdapter CreateDataAdapter() {
            IDataAdapter cmd = null;
            switch (Connection.DataBaseType) {
                case Config.DataBase.DbType.SQLSERVER:
                    cmd = new SqlDataAdapter();
                    break;
                case Config.DataBase.DbType.ORACLE:
                    cmd = new OracleDataAdapter();
                    break;
                case Config.DataBase.DbType.MYSQL:
                    cmd = new MySqlDataAdapter();
                    break;
                case Config.DataBase.DbType.ACCESS:
                    cmd = new OleDbDataAdapter();
                    break;
                default:
                    throw new Exception("不支持的数据库类型");
            }

            return cmd;
        }

        /// <summary>
        /// 创建数据库适配器对象
        /// </summary>
        /// <param name="cmd">数据库命令对象</param>
        /// <returns></returns>
        public static IDbDataAdapter CreateDataAdapter(IDbCommand cmd) {
            IDbDataAdapter adapter = null;
            switch (Connection.DataBaseType) {
                case Config.DataBase.DbType.SQLSERVER:
                    adapter = new SqlDataAdapter((SqlCommand)cmd);
                    break;
                case Config.DataBase.DbType.ORACLE:
                    adapter = new OracleDataAdapter((OracleCommand)cmd);
                    break;
                case Config.DataBase.DbType.MYSQL:
                    adapter = new MySqlDataAdapter((MySqlCommand)cmd);
                    break;
                case Config.DataBase.DbType.ACCESS:
                    adapter = new OleDbDataAdapter((OleDbCommand)cmd);
                    break;
                default:
                    throw new Exception("不支持的数据库类型");
            }

            return adapter;
        }

        /// <summary>
        /// 创建数据库参数对象
        /// </summary>
        /// <returns></returns>
        public static IDbDataParameter CreateParameter() {
            IDbDataParameter param = null;
            switch (Connection.DataBaseType) {
                case Config.DataBase.DbType.SQLSERVER:
                    param = new SqlParameter();
                    break;
                case Config.DataBase.DbType.ORACLE:
                    param = new OracleParameter();
                    break;
                case Config.DataBase.DbType.MYSQL:
                    param = new MySqlParameter();
                    break;
                case Config.DataBase.DbType.ACCESS:
                    param = new OleDbParameter();
                    break;
                default:
                    throw new Exception("不支持的数据库类型");
            }

            return param;
        }

        /// <summary>
        /// 创建数据库参数对象
        /// </summary>
        /// <param name="ParamName">参数名称</param>
        /// <param name="DbType">参数类型</param>
        /// <returns></returns>
        public static IDbDataParameter CreateParameter(string ParamName, System.Data.DbType DbType) {
            return CreateParameter(ParamName, DbType, 0);
        }
        
        /// <summary>
        /// 创建数据库参数对象
        /// </summary>
        /// <param name="ParamName">参数名称</param>
        /// <param name="DbType">参数类型</param>
        /// <param name="Size">范围</param>
        /// <returns></returns>
        public static IDbDataParameter CreateParameter(string ParamName, System.Data.DbType DbType, int Size) {
            IDbDataParameter param = null;
            switch (Connection.DataBaseType) {
                case Config.DataBase.DbType.SQLSERVER:
                    param = new SqlParameter();
                    break;
                case Config.DataBase.DbType.ORACLE:
                    param = new OracleParameter();
                    break;
                case Config.DataBase.DbType.MYSQL:
                    param = new MySqlParameter();
                    break;
                case Config.DataBase.DbType.ACCESS:
                    param = new OleDbParameter();
                    break;
                default:
                    throw new Exception("不支持的数据库类型");
            }

            param.ParameterName = ParamName;
            param.DbType = DbType;

            if (Size > 0) {
                param.Size = Size;
            }

            return param;
        }

        /// <summary>
        /// 创建数据库事务对象
        /// </summary>
        /// <returns></returns>
        public static IDbTransaction CreateTransaction() {
            IDbConnection conn = CreateConnection();

            if (conn.State == ConnectionState.Closed) {
                conn.Open();
            }

            return conn.BeginTransaction();
        }
    }
}
