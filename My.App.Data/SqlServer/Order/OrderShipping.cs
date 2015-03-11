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
    public class OrderShipping : IOrderShipping
    {
        private const string SQL_SELECT = "SelectOrderShippingByOrderNumber";                           //根据订单编号查询发货单信息列表
        private const string SQL_INSERT = "InsertOrderShipping";                                        //添加发货单信息

        private const string PARM_NUMBER = "@OrdersNumber";                                             //订单编号
        private const string PARM_LID = "@TransportModeId";                                             //配送方式编号
        private const string PARM_LNUMBER = "@LogisticsNumber";                                         //物流单号
        private const string PARM_OPEA = "@Operator";                                                   //后台审核员
        private const string PARM_DATE = "@ShippingDate";                                               //发货时间
        private const string PARM_SUCC = "@IsSuccess";                                                  //是否成功
        private const string PARM_DETAIL = "@ShippingDetail";                                           //备注信息

        public IList<tbOrderShipping> Select(string OrdersNumber) {
            IList<tbOrderShipping> MyList = new List<tbOrderShipping>();
            IDbDataParameter[] parm = GetSelectNumberParam();
            parm[0].Value = OrdersNumber;

            using (IDataReader MyReader = DBHelper.ExecuteReader(CommandType.StoredProcedure, SQL_SELECT, parm)) {
                while (MyReader.Read()) {
                    MyList.Add(new tbOrderShipping() {
                        ShippingId = MyReader.GetInt32(0),
                        OrdersNumber = MyReader.GetString(1),
                        LogisticsNumber = MyReader.GetString(2),
                        Operator = MyReader.GetString(3),
                        ShippingDate = MyReader.GetDateTime(4),
                        IsSuccess = MyReader.GetBoolean(5),
                        ShippingDetail = MyReader.GetString(6),
                        Logistics = new Entity.Delivery.tbLogistics() { 
                            LogisticsId = MyReader.GetInt32(7),
                            LogisticsName = MyReader.GetString(8)
                        }
                    });
                }
            }

            return MyList;
        }

        public bool Insert(tbOrderShipping OrderShipping) {
            bool IsOk = false;

            IDbDataParameter[] parm = GetInsertParam();
            parm[0].Value = OrderShipping.OrdersNumber;
            parm[1].Value = OrderShipping.Logistics.LogisticsId;
            parm[2].Value = OrderShipping.LogisticsNumber;
            parm[3].Value = OrderShipping.Operator;
            parm[4].Value = OrderShipping.ShippingDate;
            parm[5].Value = OrderShipping.IsSuccess;
            parm[6].Value = OrderShipping.ShippingDetail;

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
                     new SqlParameter(PARM_LID,SqlDbType.Int,4),
                     new SqlParameter(PARM_LNUMBER,SqlDbType.VarChar,50),
                     new SqlParameter(PARM_OPEA,SqlDbType.VarChar,50),
                     new SqlParameter(PARM_DATE,SqlDbType.DateTime),
                     new SqlParameter(PARM_SUCC,SqlDbType.Bit),
                     new SqlParameter(PARM_DETAIL,SqlDbType.VarChar,500)
                };

                DBHelper.CacheParameters(SQL_INSERT, parm);
            }

            return parm;
        }
    }
}
