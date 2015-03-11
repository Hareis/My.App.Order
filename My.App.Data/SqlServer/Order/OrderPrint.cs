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
    public class OrderPrint : IOrderPrint
    {
        private const string SQL_SELECT = "SelectOrderPrint";                                               //根据订单编号查询订单打印信息列表
        private const string SQL_INSERT = "InsertOrderPrint";                                               //添加订单打印信息

        private const string PARM_NUMBER = "@OrdersNumber";                                                 //订单编号
        private const string PARM_DATE = "@OrderPrintDate";                                                 //打印日期
        private const string PARM_OPEA = "@Operator";                                                       //操作员
        private const string PARM_DETAIL = "@Detail";                                                       //备注信息

        public IList<tbOrderPrint> Select(string OrdersNumber) {
            IList<tbOrderPrint> MyList = new List<tbOrderPrint>();
            IDbDataParameter[] parm = GetSelectNumberParam();
            parm[0].Value = OrdersNumber;

            using (IDataReader MyReader = DBHelper.ExecuteReader(CommandType.StoredProcedure, SQL_SELECT, parm)) {
                while (MyReader.Read()) {
                    MyList.Add(new tbOrderPrint() {
                        OrderPrintId = MyReader.GetInt32(0),
                        OrdersNumber=MyReader.GetString(1),
                        OrderPrintDate=MyReader.GetDateTime(2),
                        Operator = MyReader.GetString(3),
                        Detail = MyReader.GetString(4)

                    });
                }
            }

            return MyList;
        }

        public bool Insert(tbOrderPrint OrderPrint) {
            bool IsOk = false;

            IDbDataParameter[] parm = GetInsertParam();
            parm[0].Value = OrderPrint.OrdersNumber;
            parm[1].Value = OrderPrint.OrderPrintDate;
            parm[2].Value = OrderPrint.Operator;
            parm[3].Value = OrderPrint.Detail;

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
                     new SqlParameter(PARM_DATE,SqlDbType.DateTime),
                     new SqlParameter(PARM_OPEA,SqlDbType.VarChar,50),
                     new SqlParameter(PARM_DETAIL,SqlDbType.VarChar,500)
                };

                DBHelper.CacheParameters(SQL_INSERT, parm);
            }

            return parm;
        }
    }
}
