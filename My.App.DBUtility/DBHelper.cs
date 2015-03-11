using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.DBUtility.DbProvider;
using System.Collections;
using System.Data;
using System.Reflection;

namespace My.App.DBUtility
{
    /// <summary>
    /// 数据库操作类
    /// </summary>
    public class DBHelper
    {
        public static readonly string ConnectionStr = DBFactory.ConnectionString;

        /// <summary>
        /// 缓存哈希表
        /// </summary>
        private static Hashtable ParamCache = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        /// 执行无结果集的数据的操作命令
        /// </summary>
        /// <param name="ConnString">数据库连接字符串</param>
        /// <param name="Trans">Sql事务对象</param>
        /// <param name="CmdType">执行命令的类型</param>
        /// <param name="CmdText">存储过程或T-SQL语句等</param>
        /// <param name="CommandParameters">执行命令所需的参数数组</param>
        /// <returns>执行数据库操作所影响的行数</returns>
        public static int ExecuteNonQuery(string ConnString, IDbTransaction Trans, CommandType CmdType, string CmdText, params IDbDataParameter[] CommandParameters) {
            IDbCommand cmd = DBFactory.CreateCommand();
            using (IDbConnection conn = String.IsNullOrEmpty(ConnString) ? DBFactory.CreateConnection() : DBFactory.CreateConnection(ConnString, Config.DataBase.DbType.SQLSERVER)) {
                PrepareCommand(cmd, conn, Trans, CmdType, CmdText, CommandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }

        }

        /// <summary>
        /// 执行无结果集的数据的操作命令
        /// </summary>
        /// <param name="CmdType">执行命令的类型</param>
        /// <param name="CmdText">存储过程或T-SQL语句等</param>
        /// <param name="CommandParameters">执行命令所需的参数数组</param>
        /// <returns>执行数据库操作所影响的行数</returns>
        public static int ExecuteNonQuery(CommandType CmdType, string CmdText, params IDbDataParameter[] CommandParameters) {
            return ExecuteNonQuery("", CmdType, CmdText, CommandParameters);
        }

        /// <summary>
        /// 执行无结果集的数据的操作命令
        /// </summary>
        /// <param name="ConnString">数据库连接字符串</param>
        /// <param name="CmdType">执行命令的类型</param>
        /// <param name="CmdText">存储过程或T-SQL语句等</param>
        /// <param name="CommandParameters">执行命令所需的参数数组</param>
        /// <returns>执行数据库操作所影响的行数</returns>
        public static int ExecuteNonQuery(string ConnString, CommandType CmdType, string CmdText, params IDbDataParameter[] CommandParameters) {
            return ExecuteNonQuery(ConnString, null, CmdType, CmdText, CommandParameters);
        }

        /// <summary>
        /// 执行无结果集的数据的操作命令
        /// </summary>
        /// <param name="CmdType">执行命令的类型</param>
        /// <param name="CmdText">存储过程或T-SQL语句等</param>
        /// <returns>执行数据库操作所影响的行数</returns>
        public static int ExecuteNonQuery(CommandType CmdType, string CmdText, string ConnString = "") {
            return ExecuteNonQuery(ConnString, CmdType, CmdText, null);
        }

        /// <summary>
        /// 执行有结果集的数据的操作指令
        /// </summary>
        /// <param name="ConnString">数据库连接字符串</param>
        /// <param name="Trans">Sql事务对象</param>
        /// <param name="CmdType">执行命令的类型</param>
        /// <param name="CmdText">存储过程或T-SQL语句等</param>
        /// <param name="CommandParameters">执行命令所需的参数数组</param>
        /// <returns>结果集</returns>
        public static IDataReader ExecuteReader(string ConnString, IDbTransaction Trans, CommandType CmdType, string CmdText, params IDbDataParameter[] CommandParameters) {
            IDbCommand cmd = DBFactory.CreateCommand();
            IDbConnection conn = String.IsNullOrEmpty(ConnString) ? DBFactory.CreateConnection() : DBFactory.CreateConnection(ConnString, Config.DataBase.DbType.SQLSERVER);
            try {
                PrepareCommand(cmd, conn, Trans, CmdType, CmdText, CommandParameters);
                cmd.CommandTimeout = 500;
                IDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            } catch (Exception ex) {
                conn.Close();
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// 执行有结果集的数据的操作指令
        /// </summary>
        /// <param name="CmdType">执行命令的类型</param>
        /// <param name="CmdText">存储过程或T-SQL语句等</param>
        /// <param name="CommandParameters">执行命令所需的参数数组</param>
        /// <returns>结果集</returns>
        public static IDataReader ExecuteReader(CommandType CmdType, string CmdText, params IDbDataParameter[] CommandParameters) {
            return ExecuteReader("", CmdType, CmdText, CommandParameters);
        }

        /// <summary>
        /// 执行有结果集的数据的操作指令
        /// </summary>
        /// <param name="ConnString">数据库连接字符串</param>
        /// <param name="CmdType">执行命令的类型</param>
        /// <param name="CmdText">存储过程或T-SQL语句等</param>
        /// <param name="CommandParameters">执行命令所需的参数数组</param>
        /// <returns>结果集</returns>
        public static IDataReader ExecuteReader(string ConnString, CommandType CmdType, string CmdText, params IDbDataParameter[] CommandParameters) {
            return ExecuteReader(ConnString, null, CmdType, CmdText, CommandParameters);
        }

        /// <summary>
        /// 执行有结果集的数据的操作指令
        /// </summary>
        /// <param name="ConnString">数据库连接字符串</param>
        /// <param name="CmdType">执行命令的类型</param>
        /// <param name="CmdText">存储过程或T-SQL语句等</param>
        /// <returns>结果集</returns>
        public static IDataReader ExecuteReader(CommandType CmdType, string CmdText, string ConnString = "") {
            return ExecuteReader(ConnString, CmdType, CmdText, null);
        }

        /// <summary>
        /// 查询数据填充到IList<T>中（只支持简单数据类型，效率低慎用）
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="Trans">Sql事务对象</param>
        /// <param name="CmdType">执行命令的类型</param>
        /// <param name="CmdText">存储过程或T-SQL语句等</param>
        /// <param name="CommandParameters">执行命令所需的参数数组</param>
        /// <returns>IList<T></returns>
        public static IList<T> ExecuteIList<T>(IDbTransaction Trans, CommandType CmdType, string CmdText, params IDbDataParameter[] CommandParameters) where T : class {
            return ExecuteIList<T>(null, Trans, CmdType, CmdText, CommandParameters);
        }

        /// <summary>
        /// 查询数据填充到IList<T>中（只支持简单数据类型，效率低慎用）
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="ConnString">数据库连接字符串</param>
        /// <param name="Trans">Sql事务对象</param>
        /// <param name="CmdType">执行命令的类型</param>
        /// <param name="CmdText">存储过程或T-SQL语句等</param>
        /// <param name="CommandParameters">执行命令所需的参数数组</param>
        /// <returns>IList<T></returns>
        public static IList<T> ExecuteIList<T>(string ConnString, IDbTransaction Trans, CommandType CmdType, string CmdText, params IDbDataParameter[] CommandParameters) where T : class {
            IList<T> List = new List<T>();
            using (IDataReader rdr = ExecuteReader(ConnString, Trans, CmdType, CmdText, CommandParameters)) {
                while (rdr.Read()) {
                    T RowInstance = Activator.CreateInstance<T>();
                    foreach (PropertyInfo Property in typeof(T).GetProperties()) {
                        switch (Property.PropertyType.FullName) { 
                            case "System.Int32":
                                Action<int> Set1 = Delegate.CreateDelegate(typeof(Action<int>), RowInstance, Property.GetSetMethod()) as Action<int>;                                
                                Set1(Convert.ToInt32(rdr[Property.Name]));
                                break;
                            case "System.Double":
                                Action<double> Set2 = Delegate.CreateDelegate(typeof(Action<double>), RowInstance, Property.GetSetMethod()) as Action<double>;
                                Set2(Convert.ToDouble(rdr[Property.Name]));
                                break;
                            case "System.String":
                                Action<string> Set3 = Delegate.CreateDelegate(typeof(Action<string>), RowInstance, Property.GetSetMethod()) as Action<string>;
                                Set3(rdr[Property.Name].ToString());
                                break;
                            case "System.DateTime":
                                Action<DateTime> Set4 = Delegate.CreateDelegate(typeof(Action<DateTime>), RowInstance, Property.GetSetMethod()) as Action<DateTime>;
                                Set4(Convert.ToDateTime(rdr[Property.Name]));
                                break;
                            case "System.Boolean":
                                Action<bool> Set5 = Delegate.CreateDelegate(typeof(Action<bool>), RowInstance, Property.GetSetMethod()) as Action<bool>;
                                Set5(Convert.ToBoolean(rdr[Property.Name]));
                                break;
                            case "System.Single":
                                Action<float> Set6 = Delegate.CreateDelegate(typeof(Action<float>), RowInstance, Property.GetSetMethod()) as Action<float>;
                                Set6(Convert.ToSingle(rdr[Property.Name]));
                                break;
                            case "System.Decimal":
                                Action<decimal> Set7 = Delegate.CreateDelegate(typeof(Action<decimal>), RowInstance, Property.GetSetMethod()) as Action<decimal>;
                                Set7(Convert.ToDecimal(rdr[Property.Name]));
                                break;
                            default:
                                break;
                        }
                    }

                    List.Add(RowInstance);
                }
            }

            return List;
        }

        /// <summary>
        /// 查询数据填充到数据集DataSet中
        /// </summary>
        /// <param name="ConnString">数据库连接字符串</param>
        /// <param name="Trans">Sql事务对象</param>
        /// <param name="CmdType">执行命令的类型</param>
        /// <param name="CmdText">存储过程或T-SQL语句等</param>
        /// <param name="CommandParameters">执行命令所需的参数数组</param>
        /// <returns>DataSet数据集</returns>
        public static DataSet ExecuteDataSet(string ConnString, IDbTransaction Trans, CommandType CmdType, string CmdText, params IDbDataParameter[] CommandParameters) {
            DataSet ds = new DataSet();
            IDbCommand cmd = DBFactory.CreateCommand();
            IDbConnection conn = String.IsNullOrEmpty(ConnString) ? DBFactory.CreateConnection() : DBFactory.CreateConnection(ConnString, Config.DataBase.DbType.SQLSERVER);

            try {
                PrepareCommand(cmd, conn, Trans, CmdType, CmdText, CommandParameters);
                IDataAdapter sda = DBFactory.CreateDataAdapter(cmd);
                sda.Fill(ds);
                return ds;
            }
            catch (Exception ex) {
                throw new Exception(ex.ToString());
            }
            finally {
                conn.Close();
                cmd.Dispose();
            }
        }

        /// <summary>
        /// 查询数据填充到数据集DataSet中
        /// </summary>
        /// <param name="CmdType">执行命令的类型</param>
        /// <param name="CmdText">存储过程或T-SQL语句等</param>
        /// <param name="CommandParameters">执行命令所需的参数数组</param>
        /// <returns>DataSet数据集</returns>
        public static DataSet ExecuteDataSet(CommandType CmdType, string CmdText, params IDbDataParameter[] CommandParameters) {
            return ExecuteDataSet(null, CmdType, CmdText, CommandParameters);
        }

        /// <summary>
        /// 查询数据填充到数据集DataSet中
        /// </summary>
        /// <param name="ConnString">数据库连接字符串</param>
        /// <param name="CmdType">执行命令的类型</param>
        /// <param name="CmdText">存储过程或T-SQL语句等</param>
        /// <param name="CommandParameters">执行命令所需的参数数组</param>
        /// <returns>DataSet数据集</returns>
        public static DataSet ExecuteDataSet(string ConnString, CommandType CmdType, string CmdText, params IDbDataParameter[] CommandParameters) {
            return ExecuteDataSet(ConnString, null, CmdType, CmdText, CommandParameters);
        }

        /// <summary>
        /// 查询数据填充到数据集DataSet中
        /// </summary>
        /// <param name="CmdType">执行命令的类型</param>
        /// <param name="CmdText">存储过程或T-SQL语句等</param>
        /// <param name="ConnString">数据库连接字符串</param>
        /// <returns>DataSet数据集</returns>
        public static DataSet ExecuteDataSet(CommandType CmdType, string CmdText, string ConnString = "") {
            return ExecuteDataSet(ConnString, CmdType, CmdText, null);
        }
        
        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列
        /// </summary>
        /// <param name="ConnString">数据库连接字符串</param>
        /// <param name="Trans">Sql事务对象</param>
        /// <param name="CmdType">执行命令的类型</param>
        /// <param name="CmdText">存储过程或T-SQL语句等</param>
        /// <param name="CommandParameters">执行命令所需的参数数组</param>
        /// <returns>结果集中第一行的第一列</returns>
        public static object ExecuteScalar(string ConnString, IDbTransaction Trans, CommandType CmdType, string CmdText, params IDbDataParameter[] CommandParameters) {
            IDbCommand cmd = DBFactory.CreateCommand();
            using (IDbConnection connection = String.IsNullOrEmpty(ConnString) ? DBFactory.CreateConnection() : DBFactory.CreateConnection(ConnString, Config.DataBase.DbType.SQLSERVER)) {
                PrepareCommand(cmd, connection, Trans, CmdType, CmdText, CommandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列
        /// </summary>
        /// <param name="ConnString">数据库连接字符串</param>
        /// <param name="CmdType">执行命令的类型</param>
        /// <param name="CmdText">存储过程或T-SQL语句等</param>
        /// <param name="CommandParameters">执行命令所需的参数数组</param>
        /// <returns>结果集中第一行的第一列</returns>
        public static object ExecuteScalar(CommandType CmdType, string CmdText, params IDbDataParameter[] CommandParameters) {
            return ExecuteScalar(null, CmdType, CmdText, CommandParameters);
        }

        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列
        /// </summary>
        /// <param name="CmdType">执行命令的类型</param>
        /// <param name="CmdText">存储过程或T-SQL语句等</param>
        /// <param name="CommandParameters">执行命令所需的参数数组</param>
        /// <returns>结果集中第一行的第一列</returns>
        public static object ExecuteScalar(string ConnString, CommandType CmdType, string CmdText, params IDbDataParameter[] CommandParameters) {
            return ExecuteScalar(ConnString, null, CmdType, CmdText, CommandParameters);
        }

        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列
        /// </summary>
        /// <param name="CmdType">执行命令的类型</param>
        /// <param name="CmdText">存储过程或T-SQL语句等</param>
        /// <returns>结果集中第一行的第一列</returns>
        public static object ExecuteScalar(CommandType CmdType, string CmdText) {
            return ExecuteScalar(null, CmdType, CmdText, null);
        }

        /// <summary>
        /// 预处理用户提供的命令，数据库连接/事务/命令类型/参数
        /// </summary>
        /// <param name="Cmd">要处理的DbCommand</param>
        /// <param name="Conn">数据库连接</param>
        /// <param name="Trans">一个有效的事务</param>
        /// <param name="CmdType">命令类型</param>
        /// <param name="CmdText">存储过程名或Sql命令文本</param>
        /// <param name="CmdParams">和命令相关的参数数组</param>
        private static void PrepareCommand(IDbCommand Cmd, IDbConnection Conn, IDbTransaction Trans, CommandType CmdType, string CmdText, IDbDataParameter[] CmdParams) {
            if (Cmd == null) {
                throw new ArgumentException("Command");
            }

            if (Conn.State != ConnectionState.Open) {
                Conn.Open();
            }

            Cmd.Connection = Conn;
            Cmd.CommandText = CmdText;

            if (Trans != null) {
                if (Trans.Connection != null) {
                    Cmd.Transaction = Trans;
                }
            }

            Cmd.CommandType = CmdType;

            if (CmdParams != null) {
                AttachParameters(Cmd, CmdParams);
            }
        }

        /// <summary>
        /// 将参数数组分配给Command命令
        /// </summary>
        /// <param name="Command">命令对象</param>
        /// <param name="CmdParams">参数数组</param>
        private static void AttachParameters(IDbCommand Command, IDbDataParameter[] CmdParams) {
            if (Command != null && CmdParams != null) {
                foreach (IDbDataParameter p in CmdParams) {
                    if ((p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Input) && (p.Value == null)) {
                        p.Value = DBNull.Value;
                    }
                    Command.Parameters.Add(p);
                }
            }
        }

        /// <summary>
        /// 参数数组缓存
        /// </summary>
        /// <param name="CacheKey">键</param>
        /// <param name="CommandParameters">需缓存对象</param>
        public static void CacheParameters(string CacheKey, params IDbDataParameter[] CommandParameters) {
            ParamCache[CacheKey] = CommandParameters;
        }

        /// <summary>
        /// 查询缓存参数
        /// </summary>
        /// <param name="CacheKey">使用缓存名称查找值</param>
        /// <param name="DbType">数据库类型</param>
        /// <returns></returns>
        public static IDbDataParameter[] GetCacheParameters(string CacheKey) {
            IDbDataParameter[] CachedParams = (IDbDataParameter[])ParamCache[CacheKey];

            if (CachedParams == null) {
                return null;
            } else {
                return CloneParameters(CachedParams);
            }
        }

        /// <summary>
        /// 参数数组深层拷贝
        /// </summary>
        /// <param name="OriginalParameters">原始参数数组</param>
        /// <param name="DbType">数据库类型</param>
        /// <returns></returns>
        private static IDbDataParameter[] CloneParameters(IDbDataParameter[] OriginalParameters) {
            IDbDataParameter[] ClonedParameters = new IDbDataParameter[OriginalParameters.Length];

            for (int i = 0, j = OriginalParameters.Length; i < j; i++) {
                ClonedParameters[i] = (IDbDataParameter)((ICloneable)OriginalParameters[i]).Clone();
            }

            return ClonedParameters;
        }

        /// <summary>
        /// 添加查询参数
        /// </summary>
        /// <param name="ParamName">参数名称</param>
        /// <param name="DbType">数据类型</param>
        /// <param name="Size">范围</param>
        /// <returns></returns>
        public static IDbDataParameter MakeParam(string ParamName, System.Data.DbType DbType, int Size) {
            return MakeParam(ParamName, DbType, Size, ParameterDirection.Input, null);
        }

        /// <summary>
        /// 添加查询参数
        /// </summary>
        /// <param name="ParamName">参数名称</param>
        /// <param name="DbType">数据类型</param>
        /// <param name="Size">范围</param>
        /// <param name="Value">参数值</param>
        /// <returns></returns>
        public static IDbDataParameter MakeParam(string ParamName, System.Data.DbType DbType, int Size, object Value) {
            return MakeParam(ParamName, DbType, Size, ParameterDirection.Input, Value);
        }

        /// <summary>
        /// 添加查询参数
        /// </summary>
        /// <param name="ParamName">参数名称</param>
        /// <param name="DbType">数据类型</param>
        /// <returns></returns>
        public static IDbDataParameter MakeParam(string ParamName, System.Data.DbType DbType) {
            IDbDataParameter param = DBFactory.CreateParameter(ParamName, DbType);
            param.Direction = ParameterDirection.Input;
            return param;
        }

        /// <summary>
        /// 添加查询参数
        /// </summary>
        /// <param name="ParamName">参数名称</param>
        /// <param name="DbType">数据类型</param>
        /// <param name="Size">范围</param>
        /// <param name="Direction">参数类型</param>
        /// <param name="Value">参数值</param>
        /// <returns></returns>
        public static IDbDataParameter MakeParam(string ParamName, System.Data.DbType DbType, int Size, ParameterDirection Direction, object Value) {
            IDbDataParameter param = DBFactory.CreateParameter(ParamName, DbType, Size);
            
            param.Direction = Direction;
            if (!(Direction == ParameterDirection.Output && Value == null)) {
                param.Value = Value;
            }

            return param;
        }
    }

    /// <summary>
    /// 数据库参数管理类
    /// </summary>
    public class DataParameter { 
        private IDbDataParameter[] dbDataParameter = null;
        private System.Data.SqlClient.SqlParameter[] sqlParameter = null;
        private System.Data.OracleClient.OracleParameter[] oracleParameter = null;
        private System.Data.OleDb.OleDbParameter[] oleDbParameter = null;
        private MySql.Data.MySqlClient.MySqlParameter[] mySqlParameter = null;

        /// <summary>
        /// 数据库参数管理类无参构造函数
        /// </summary>
        internal DataParameter() { }

        /// <summary>
        /// IDbDataParameter参数列表
        /// </summary>
        public IDbDataParameter[] DbDataParameter {
            get { return dbDataParameter; }
            internal set { dbDataParameter = value; }
        }

        /// <summary>
        /// SqlParameter参数列表
        /// </summary>
        public System.Data.SqlClient.SqlParameter[] SqlParameter {
            get { return sqlParameter; }
            internal set { sqlParameter = value; }
        }

        /// <summary>
        /// OracleParameter参数列表
        /// </summary>
        public System.Data.OracleClient.OracleParameter[] OracleParameter {
            get { return oracleParameter; }
            internal set { oracleParameter = value; }
        }

        /// <summary>
        /// OleDbParameter参数列表
        /// </summary>
        public System.Data.OleDb.OleDbParameter[] OleDbParameter {
            get { return oleDbParameter; }
            internal set { oleDbParameter = value; }
        }

        /// <summary>
        /// MySqlParameter参数列表
        /// </summary>
        public MySql.Data.MySqlClient.MySqlParameter[] MySqlParameter {
            get { return mySqlParameter; }
            internal set { mySqlParameter = value; }
        }
    }
}
