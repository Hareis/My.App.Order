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
    public class OtherConfig : IOtherConfig
    {
        private const string SQL_SELECT = "SelectOtherConfig";                                                  //查询系统其他设置信息
        private const string SQL_UPDATE = "UpdateOtherConfig";                                                  //保存系统其他设置信息

        private const string PARM_UID = "@UserId";                                                              //用户编号
        private const string PARM_INV = "@Invoice";                                                             //是否打印配货单
        private const string PARM_AUTO = "@AutoHeight";                                                         //是否自动计算发货单高度

        public tbOtherConfig Select(int UserId) {
            tbOtherConfig config = null;
            IDbDataParameter[] parm = GetSelectParam();
            parm[0].Value = UserId;

            using (IDataReader MyReader = DBHelper.ExecuteReader(CommandType.StoredProcedure, SQL_SELECT, parm)) {
                if (MyReader.Read()) {
                    config = new tbOtherConfig() {
                        UserId = MyReader.GetInt32(0),
                        Invoice = MyReader.GetBoolean(1),
                        AutoHeight = MyReader.GetBoolean(2)
                    };
                }
            }

            return config == null ? new tbOtherConfig() : config;
        }

        public bool Save(tbOtherConfig OtherConfig) {
            bool IsOk = false;

            IDbDataParameter[] parm = GetUpdateParam();
            parm[0].Value = OtherConfig.UserId;
            parm[1].Value = OtherConfig.Invoice;
            parm[2].Value = OtherConfig.AutoHeight;

            try {
                IsOk = DBHelper.ExecuteNonQuery(CommandType.StoredProcedure, SQL_UPDATE, parm)>0;
            }
            catch { }

            return IsOk;
        }

        private static IDbDataParameter[] GetSelectParam() {
            IDbDataParameter[] parm = DBHelper.GetCacheParameters(SQL_SELECT);

            if (parm == null) {
                parm = new SqlParameter[] {
                     new SqlParameter(PARM_UID,SqlDbType.VarChar,50)
                };

                DBHelper.CacheParameters(SQL_SELECT, parm);
            }

            return parm;
        }

        private static IDbDataParameter[] GetUpdateParam() {
            IDbDataParameter[] parm = DBHelper.GetCacheParameters(SQL_UPDATE);

            if (parm == null) {
                parm = new SqlParameter[] {
                     new SqlParameter(PARM_UID,SqlDbType.Int,4),
                     new SqlParameter(PARM_INV,SqlDbType.Bit),
                     new SqlParameter(PARM_AUTO,SqlDbType.Bit)
                };

                DBHelper.CacheParameters(SQL_UPDATE, parm);
            }

            return parm;
        }
    }
}
