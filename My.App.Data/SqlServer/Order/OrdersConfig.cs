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
    public class OrdersConfig : IOrdersConfig
    {
        private const string SQL_SELECT = "SelectOrdersConfigByShopId";                                             //根据店铺编号查询订单获取配置信息
        private const string SQL_SELECT_UID = "SelectOrdersConfigByUserId";                                         //根据用户编号查询订单获取配置信息
        private const string SQL_UPDATE = "UpdateOrdersConfig";                                                     //修改订单获取配置信息

        private const string PARM_ID = "@ConfigId";                                                                 //配置编号
        private const string PARM_SID = "@ShopId";                                                                  //店铺编号
        private const string PARM_PAY = "@JudgePay";                                                                //判断付款
        private const string PARM_ORDER = "@MergerOrder";                                                           //合并订单
        private const string PARM_PRINT = "@RefundPrint";                                                           //退款打印
        private const string PARM_TIME = "@PayTime";                                                                //付款时间
        private const string PARM_FLAG = "@RemarkFlag";                                                             //备注旗帜
        private const string PARM_REMARK = "@Remark";                                                               //备注内容
        private const string PARM_CASH = "@CashDelivery";                                                           //货到付款使用物流
        private const string PARM_DIS = "@LogisticsDis";                                                            //物流自动分配

        private const string PARM_UID = "@UserId";                                                                  //用户编号

        public tbOrdersConfig Select(int ShopId) {
            tbOrdersConfig Config = null;
            IDbDataParameter[] parm = GetSelectBySIdParam();
            parm[0].Value = ShopId;

            using (IDataReader MyReader = DBHelper.ExecuteReader(CommandType.StoredProcedure, SQL_SELECT, parm)) {
                if (MyReader.Read()) {
                    Config = new tbOrdersConfig() {
                        ConfigId = MyReader.GetInt32(0),
                        ShopId = MyReader.GetInt32(1),
                        JudgePay = MyReader.GetBoolean(2),
                        MergerOrder = MyReader.GetBoolean(3),
                        RefundPrint = MyReader.GetBoolean(4),
                        PayTime = MyReader.GetInt32(5),
                        RemarkFlag = MyReader.GetInt32(6),
                        Remark = MyReader.GetString(7),
                        CashDelivery = MyReader.GetString(8),
                        LogisticsDis = MyReader.GetBoolean(9)
                    };
                }
            }

            return Config == null ? new tbOrdersConfig() : Config;
        }

        public tbOrdersConfig SelectByUserId(int UserId) { 
            tbOrdersConfig Config = null;
            IDbDataParameter[] parm = GetSelectByUIdParam();
            parm[0].Value = UserId;

            using (IDataReader MyReader = DBHelper.ExecuteReader(CommandType.StoredProcedure, SQL_SELECT_UID, parm)) {
                if (MyReader.Read()) {
                    Config = new tbOrdersConfig() {
                        ConfigId = MyReader.GetInt32(0),
                        ShopId = MyReader.GetInt32(1),
                        JudgePay = MyReader.GetBoolean(2),
                        MergerOrder = MyReader.GetBoolean(3),
                        RefundPrint = MyReader.GetBoolean(4),
                        PayTime = MyReader.GetInt32(5),
                        RemarkFlag = MyReader.GetInt32(6),
                        Remark = MyReader.GetString(7),
                        CashDelivery = MyReader.GetString(8),
                        LogisticsDis = MyReader.GetBoolean(9)
                    };
                }
            }

            return Config == null ? new tbOrdersConfig() : Config;
        }

        public int Update(tbOrdersConfig OrdersConfig) {
            int Id = 0;
            IDbDataParameter[] parm = GetUpdateParam();
            parm[0].Value = OrdersConfig.ConfigId;
            parm[1].Value = OrdersConfig.ShopId;
            parm[2].Value = OrdersConfig.JudgePay;
            parm[3].Value = OrdersConfig.MergerOrder;
            parm[4].Value = OrdersConfig.RefundPrint;
            parm[5].Value = OrdersConfig.PayTime;
            parm[6].Value = OrdersConfig.RemarkFlag;
            parm[7].Value = OrdersConfig.Remark;
            parm[8].Value = OrdersConfig.CashDelivery;
            parm[9].Value = OrdersConfig.LogisticsDis;

            try {
                using (IDataReader MyReader = DBHelper.ExecuteReader(CommandType.StoredProcedure, SQL_UPDATE, parm)) {
                    if (MyReader.Read()) {
                        Id = MyReader.GetInt32(0);
                    }
                }
            } catch { }

            return Id;
        }

        private static IDbDataParameter[] GetSelectBySIdParam() {
            IDbDataParameter[] parm = DBHelper.GetCacheParameters(SQL_SELECT);

            if (parm == null) {
                parm = new SqlParameter[] {
                     new SqlParameter(PARM_SID,SqlDbType.Int)
                };

                DBHelper.CacheParameters(SQL_SELECT, parm);
            }

            return parm;
        }

        private static IDbDataParameter[] GetSelectByUIdParam() {
            IDbDataParameter[] parm = DBHelper.GetCacheParameters(SQL_SELECT_UID);

            if (parm == null) {
                parm = new SqlParameter[] {
                     new SqlParameter(PARM_UID,SqlDbType.Int)
                };

                DBHelper.CacheParameters(SQL_SELECT_UID, parm);
            }

            return parm;
        }

        private static IDbDataParameter[] GetUpdateParam() {
            IDbDataParameter[] parm = DBHelper.GetCacheParameters(SQL_UPDATE);

            if (parm == null) {
                parm = new SqlParameter[] {
                     new SqlParameter(PARM_ID,SqlDbType.Int),
                     new SqlParameter(PARM_SID,SqlDbType.Int),
                     new SqlParameter(PARM_PAY,SqlDbType.Bit),
                     new SqlParameter(PARM_ORDER,SqlDbType.Bit),
                     new SqlParameter(PARM_PRINT,SqlDbType.Bit),
                     new SqlParameter(PARM_TIME,SqlDbType.Int),
                     new SqlParameter(PARM_FLAG,SqlDbType.Int),
                     new SqlParameter(PARM_REMARK,SqlDbType.VarChar,50),
                     new SqlParameter(PARM_CASH,SqlDbType.VarChar,50),
                     new SqlParameter(PARM_DIS,SqlDbType.Bit)
                };

                DBHelper.CacheParameters(SQL_UPDATE, parm);
            }

            return parm;
        }
    }
}