using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Interface.Order;
using My.App.Entity.Order;
using System.Data;
using My.App.DBUtility;

namespace My.App.Data.SqlServer.Order
{
    public class OrdersStatus : IOrdersStatus
    {
        private const string SQL_SELECT = "SelectOrdersStatus";                                                         //查询订单状态信息列表

        public IList<tbOrdersStatus> Select() {
            IList<tbOrdersStatus> MyList = new List<tbOrdersStatus>();

            using (IDataReader MyReader = DBHelper.ExecuteReader(CommandType.StoredProcedure, SQL_SELECT)) {
                while (MyReader.Read()) {
                    MyList.Add(new tbOrdersStatus() {
                        OrdersStatusId = MyReader.GetInt32(0),
                        OrdersStatusCode = MyReader.GetString(1),
                        OrdersStatusName = MyReader.GetString(2),
                        Detail = MyReader.GetString(3)
                    });
                }
            }

            return MyList;
        }
    }
}
