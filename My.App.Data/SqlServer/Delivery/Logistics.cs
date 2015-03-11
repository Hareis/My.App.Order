using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Interface.Delivery;
using My.App.Entity.Delivery;
using System.Data;
using My.App.DBUtility;
using System.Data.SqlClient;

namespace My.App.Data.SqlServer.Delivery
{
    public class Logistics : ILogistics
    {
        private const string SQL_SELECT = "SelectLogistics";                                                        //查询物流信息列表
        private const string SQL_SELECT_ID = "SelectLogisticsById";                                                 //根据物流编号查询物流信息

        private const string PARM_ID = "@LogisticsId";                                                              //物流编号

        public IList<tbLogistics> Select() {
            IList<tbLogistics> MyList = new List<tbLogistics>();

            using (IDataReader MyReader = DBHelper.ExecuteReader(CommandType.StoredProcedure, SQL_SELECT)) {
                while (MyReader.Read()) {
                    MyList.Add(new tbLogistics() {
                        LogisticsId = MyReader.GetInt32(0),
                        LogisticsCode = MyReader.GetString(1),
                        LogisticsName = MyReader.GetString(2),
                        LogisticsReg = MyReader.GetString(3),
                        LogisticsImg= MyReader.GetString(4),
                        LogisticsWdith = MyReader.GetInt32(5),
                        LogisticsHeight = MyReader.GetInt32(6),
                        Sort = MyReader.GetInt32(7),
                        Detail = MyReader.GetString(8)
                    });
                }
            }

            return MyList;
        }

        public tbLogistics Select(int LogisticsId) {
            tbLogistics Logistics = null;
            IDbDataParameter[] parm = GetSelectIdParam();
            parm[0].Value = LogisticsId;

            using (IDataReader MyReader = DBHelper.ExecuteReader(CommandType.StoredProcedure, SQL_SELECT_ID, parm)) {
                if (MyReader.Read()) {
                    Logistics = new tbLogistics() {
                        LogisticsId = MyReader.GetInt32(0),
                        LogisticsCode = MyReader.GetString(1),
                        LogisticsName = MyReader.GetString(2),
                        LogisticsReg = MyReader.GetString(3),
                        LogisticsImg= MyReader.GetString(4),
                        LogisticsWdith = MyReader.GetInt32(5),
                        LogisticsHeight = MyReader.GetInt32(6),
                        Sort = MyReader.GetInt32(7),
                        Detail = MyReader.GetString(8)
                    };
                }
            }

            return Logistics == null ? new tbLogistics() : Logistics;
        }

        private static IDbDataParameter[] GetSelectIdParam() {
            IDbDataParameter[] parm = DBHelper.GetCacheParameters(SQL_SELECT_ID);

            if (parm == null) {
                parm = new SqlParameter[] {
                     new SqlParameter(PARM_ID,SqlDbType.Int,4)
                };

                DBHelper.CacheParameters(SQL_SELECT_ID, parm);
            }

            return parm;
        }
    }
}
