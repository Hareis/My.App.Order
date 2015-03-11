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
    public class OrderSync : IOrderSync
    {
        private const string SQL_SELECT = "SelectOrderSyncByUserId";                                            //根据用户编号查询订单同步状态信息
        private const string SQL_INSERT = "InsertOrderSync";                                                    //添加订单同步状态信息
        private const string SQL_UPDATE = "UpdateOrderSync";                                                    //修改订单同步状态信息

        private const string PARM_UID = "@UserId";                                                              //用户编号
        private const string PARM_SYNC = "@Sync";                                                               //是否正在同步
        private const string PARM_ENUM = "@SyncEnum";                                                           //同步类型

        public tbOrderSync Select(int UserId, int SyncEnum) {
            tbOrderSync Sync = null;
            IDbDataParameter[] parm = GetSelecByUIdParam();
            parm[0].Value = UserId;
            parm[1].Value = SyncEnum;

            using (IDataReader MyReader = DBHelper.ExecuteReader(CommandType.StoredProcedure, SQL_SELECT, parm)) {
                if (MyReader.Read()) {
                    Sync = new tbOrderSync() {
                        UserId = MyReader.GetInt32(0),
                        Sync = MyReader.GetBoolean(1),
                        SyncEnum = MyReader.GetInt32(2)
                    };
                }
            }

            return Sync == null ? new tbOrderSync() : Sync;
        }

        public bool Insert(tbOrderSync OrderSync) {
            bool IsOk = false;

            IDbDataParameter[] parm = GetInsertParam();
            parm[0].Value = OrderSync.UserId;
            parm[1].Value = OrderSync.Sync;
            parm[2].Value = OrderSync.SyncEnum;

            try {
                DBHelper.ExecuteNonQuery(CommandType.StoredProcedure, SQL_INSERT, parm);
                IsOk = true;
            }
            catch { }

            return IsOk;
        }

        public bool Update(tbOrderSync OrderSync) {
            bool IsOk = false;

            IDbDataParameter[] parm = GetUpdateParam();
            parm[0].Value = OrderSync.UserId;
            parm[1].Value = OrderSync.Sync;
            parm[2].Value = OrderSync.SyncEnum;

            try {
                DBHelper.ExecuteNonQuery(CommandType.StoredProcedure, SQL_UPDATE, parm);
                IsOk = true;
            }
            catch { }

            return IsOk;
        }

        private static IDbDataParameter[] GetSelecByUIdParam() {
            IDbDataParameter[] parm = DBHelper.GetCacheParameters(SQL_SELECT);

            if (parm == null) {
                parm = new SqlParameter[] {
                     new SqlParameter(PARM_UID,SqlDbType.Int,4),
                     new SqlParameter(PARM_ENUM,SqlDbType.Int,4)
                };

                DBHelper.CacheParameters(SQL_SELECT, parm);
            }

            return parm;
        }

        private static IDbDataParameter[] GetInsertParam() {
            IDbDataParameter[] parm = DBHelper.GetCacheParameters(SQL_INSERT);

            if (parm == null) {
                parm = new SqlParameter[] {
                     new SqlParameter(PARM_UID,SqlDbType.Int,4),
                     new SqlParameter(PARM_SYNC,SqlDbType.Bit),
                     new SqlParameter(PARM_ENUM,SqlDbType.Int,4)
                };

                DBHelper.CacheParameters(SQL_INSERT, parm);
            }

            return parm;
        }

        private static IDbDataParameter[] GetUpdateParam() {
            IDbDataParameter[] parm = DBHelper.GetCacheParameters(SQL_UPDATE);

            if (parm == null) {
                parm = new SqlParameter[] {
                     new SqlParameter(PARM_UID,SqlDbType.Int,4),
                     new SqlParameter(PARM_SYNC,SqlDbType.Bit),
                     new SqlParameter(PARM_ENUM,SqlDbType.Int,4)
                };

                DBHelper.CacheParameters(SQL_UPDATE, parm);
            }

            return parm;
        }
    }
}
