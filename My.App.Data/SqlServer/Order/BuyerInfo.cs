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
    public class BuyerInfo : IBuyerInfo
    {
        private const string SQL_SELECT = "SelectBuyerByOrdersNumber";                                      //根据订单编号查询买家信息
        private const string SQL_INSERT = "InsertBuyerInfo";                                                //添加买家信息

        private const string PARM_NUMBER = "@OrdersNumber";                                                 //订单编号
        private const string PARM_NNAME = "@NickName";                                                      //买家昵称
        private const string PARM_NAME = "@BuyerName";                                                      //买家姓名
        private const string PARM_MOBILE = "@Mobile";                                                       //买家手机
        private const string PARM_PHONE = "@Phone";                                                         //买家电话
        private const string PARM_EMAIL = "@BuyerEmail";                                                    //买家邮箱

        public tbBuyerInfo Select(string OrdersNumber) {
            tbBuyerInfo Buyer = null;
            IDbDataParameter[] parm = GetSelectNumberParam();
            parm[0].Value = OrdersNumber;

            using (IDataReader MyReader = DBHelper.ExecuteReader(CommandType.StoredProcedure, SQL_SELECT, parm)) {
                if (MyReader.Read()) {
                    Buyer = new tbBuyerInfo() {
                        BuyerId = MyReader.GetInt32(0),
                        OrdersNumber = MyReader.GetString(1),
                        NickName = MyReader.GetString(2),
                        BuyerName = MyReader.GetString(3),
                        Mobile = MyReader.GetString(4),
                        Phone = MyReader.GetString(5),
                        BuyerEmail = MyReader.GetString(6)
                    };
                }
            }

            return Buyer == null ? new tbBuyerInfo() : Buyer;
        }

        public bool Insert(tbBuyerInfo Buyer) {
            bool IsOk = false;

            IDbDataParameter[] parm = GetInsertParam();
            parm[0].Value = Buyer.OrdersNumber;
            parm[1].Value = Buyer.NickName;
            parm[2].Value = Buyer.BuyerName;
            parm[3].Value = Buyer.Mobile;
            parm[4].Value = Buyer.Phone;
            parm[5].Value = Buyer.BuyerEmail;

            try {
                DBHelper.ExecuteNonQuery(CommandType.StoredProcedure, SQL_INSERT, parm);
                IsOk = true;
            }
            catch { }

            return IsOk;
        }

        private static IDbDataParameter[] GetSelectNumberParam() {
            IDbDataParameter[] parm = DBHelper.GetCacheParameters(SQL_SELECT);

            if (parm == null) {
                parm = new SqlParameter[] {
                     new SqlParameter(PARM_NUMBER,SqlDbType.VarChar,50)
                };

                DBHelper.CacheParameters(SQL_SELECT, parm);
            }

            return parm;
        }

        private static IDbDataParameter[] GetInsertParam() {
            IDbDataParameter[] parm = DBHelper.GetCacheParameters(SQL_INSERT);

            if (parm == null) {
                parm = new SqlParameter[] {
                     new SqlParameter(PARM_NUMBER,SqlDbType.VarChar,50),
                     new SqlParameter(PARM_NNAME,SqlDbType.VarChar,50),
                     new SqlParameter(PARM_NAME,SqlDbType.VarChar,50),
                     new SqlParameter(PARM_MOBILE,SqlDbType.VarChar,50),
                     new SqlParameter(PARM_PHONE,SqlDbType.VarChar,50),
                     new SqlParameter(PARM_EMAIL,SqlDbType.VarChar,50)
                };

                DBHelper.CacheParameters(SQL_INSERT, parm);
            }

            return parm;
        }
    }
}
