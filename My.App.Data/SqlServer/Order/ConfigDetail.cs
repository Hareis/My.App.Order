using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Interface.Order;
using My.App.Entity.Order;
using System.Data;
using My.App.DBUtility;
using System.Data.SqlClient;

namespace My.App.Data.SqlServer.Order
{
    public class ConfigDetail : IConfigDetail
    {
        private const string SQL_SEELCT = "SelectConfigDetail";                                                 //根据配置编号查询订单获取配置详情信息列表
        private const string SQL_INSERT = "InsertConfigDetail";                                                 //添加订单获取配置详情信息
        private const string SQL_DELETE = "DeleteConfigDetail";                                                 //根据配置编号删除订单获取配置详情信息

        private const string PARM_CID = "@ConfigId";                                                            //配置编号
        private const string PARM_EID = "@EnumId";                                                              //枚举编号
        private const string PARM_KEY = "@Key";                                                                 //键
        private const string PARM_VALUE = "@Value";                                                             //值

        public IList<tbConfigDetail> Select(int ConfigId) {
            IList<tbConfigDetail> MyList = new List<tbConfigDetail>();
            IDbDataParameter[] parm = GetSelectParam();
            parm[0].Value = ConfigId;

            using (IDataReader MyReader = DBHelper.ExecuteReader(CommandType.StoredProcedure, SQL_SEELCT, parm)) {
                while (MyReader.Read()) {
                    MyList.Add(new tbConfigDetail() {
                        DetailId = MyReader.GetInt32(0),
                        ConfigId = MyReader.GetInt32(1),
                        EnumId = MyReader.GetInt32(2),
                        Key = MyReader.GetString(3),
                        Value = MyReader.GetString(4)

                    });
                }
            }

            return MyList;
        }

        public bool Insert(tbConfigDetail ConfigDetail) {            
            bool IsOk = false;

            IDbDataParameter[] parm = GetInsertParam();
            parm[0].Value = ConfigDetail.ConfigId;
            parm[1].Value = ConfigDetail.EnumId;
            parm[2].Value = ConfigDetail.Key;
            parm[3].Value = ConfigDetail.Value;

            try {
                DBHelper.ExecuteNonQuery(CommandType.StoredProcedure, SQL_INSERT, parm);
                IsOk = true;
            } catch { }

            return IsOk;
        }

        public bool Delete(int ConfigId) {
            bool IsOk = false;

            IDbDataParameter[] parm = GetDeleteParam();
            parm[0].Value = ConfigId;

            try {
                DBHelper.ExecuteNonQuery(CommandType.StoredProcedure, SQL_DELETE, parm);
                IsOk = true;
            } catch { }

            return IsOk;
        }

        private static IDbDataParameter[] GetSelectParam() {
            IDbDataParameter[] parm = DBHelper.GetCacheParameters(SQL_SEELCT);

            if (parm == null) {
                parm = new SqlParameter[] {
                     new SqlParameter(PARM_CID,SqlDbType.Int)
                };

                DBHelper.CacheParameters(SQL_SEELCT, parm);
            }

            return parm;
        }

        private static IDbDataParameter[] GetInsertParam() {
            IDbDataParameter[] parm = DBHelper.GetCacheParameters(SQL_INSERT);

            if (parm == null) {
                parm = new SqlParameter[] {
                     new SqlParameter(PARM_CID,SqlDbType.Int),
                     new SqlParameter(PARM_EID,SqlDbType.Int),
                     new SqlParameter(PARM_KEY,SqlDbType.VarChar,50),
                     new SqlParameter(PARM_VALUE,SqlDbType.VarChar,50)
                };

                DBHelper.CacheParameters(SQL_INSERT, parm);
            }

            return parm;
        }

        private static IDbDataParameter[] GetDeleteParam() {
            IDbDataParameter[] parm = DBHelper.GetCacheParameters(SQL_DELETE);

            if (parm == null) {
                parm = new SqlParameter[] {
                     new SqlParameter(PARM_CID,SqlDbType.Int)
                };

                DBHelper.CacheParameters(SQL_DELETE, parm);
            }

            return parm;
        }
    }
}