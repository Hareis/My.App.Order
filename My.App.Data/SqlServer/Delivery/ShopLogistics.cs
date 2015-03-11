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
    public class ShopLogistics : IShopLogistics
    {
        private const string SQL_SELECT_SID = "SelectShopLogisticsByShopId";                                                //根据店铺编号查询店铺物流关系信息列表
        private const string SQL_SELECT_IDLIST = "SelectShopLogisticsByIdList";                                             //根据店铺编号列表查询店铺物流编号列表
        private const string SQL_SELECT_INFO = "SelectShopLogisticsInfo";                                                   //根据店铺编号查询店铺物流关系信息和物流信息列表
        private const string SQL_SELECT_UID = "SelectShopLogisticsByUserId";                                                //根据用户编号查询店铺物流关系信息和物流信息列表
        private const string SQL_INSERT = "InsertShopLogistics";                                                            //添加店铺物流关系信息
        private const string SQL_DELETE_SID = "DeleteShopLogisticsByShopId";                                                //根据店铺编号删除店铺物流关系信息

        private const string PARM_SID = "@ShopId";                                                                          //店铺编号
        private const string PARM_LID = "@LogisticsId";                                                                     //物流编号
        private const string PARM_SORT = "@Sort";                                                                           //排序字段
        private const string PARM_NUMBER = "@Number";                                                                       //打印数量
        private const string PARM_UID = "@UserId";                                                                          //用户编号
        private const string PARM_IDLIST = "@IdList";                                                                       //店铺编号列表

        public IList<tbShopLogistics> Select(int ShopId) {
            IList<tbShopLogistics> MyList = new List<tbShopLogistics>();
            IDbDataParameter[] parm = GetSelectBySIdParam();
            parm[0].Value = ShopId;

            using (IDataReader MyReader = DBHelper.ExecuteReader(CommandType.StoredProcedure, SQL_SELECT_SID, parm)) {
                while (MyReader.Read()) {
                    MyList.Add(new tbShopLogistics() {
                        SLID = MyReader.GetInt32(0),
                        ShopId = MyReader.GetInt32(1),
                        LogisticsId = MyReader.GetInt32(2),
                        Sort = MyReader.GetInt32(3),
                        Number = MyReader.GetInt32(4)
                    });
                }
            }

            return MyList;
        }

        public string Select(string IdList) {
            List<string> MyList = new List<string>();
            IDbDataParameter[] parm = GetSelectByIdListParam();
            parm[0].Value = IdList;

            using (IDataReader MyReader = DBHelper.ExecuteReader(CommandType.StoredProcedure, SQL_SELECT_IDLIST, parm)) {
                while (MyReader.Read()) {
                    MyList.Add(MyReader.GetInt32(0).ToString());
                }
            }

            return String.Join(",", MyList);
        }

        public IList<tbShopLogistics> SelectInfo(int ShopId) { 
            IList<tbShopLogistics> MyList = new List<tbShopLogistics>();
            IDbDataParameter[] parm = GetSelectInfoParam();
            parm[0].Value = ShopId;

            using (IDataReader MyReader = DBHelper.ExecuteReader(CommandType.StoredProcedure, SQL_SELECT_INFO, parm)) {
                while (MyReader.Read()) {
                    MyList.Add(new tbShopLogistics() {
                        SLID = MyReader.GetInt32(0),
                        ShopId = MyReader.GetInt32(1),
                        Logistics = new tbLogistics() {
                            LogisticsId = MyReader.GetInt32(2),
                            LogisticsName = MyReader.GetString(3),
                            LogisticsCode = MyReader.GetString(4),
                            LogisticsImg = MyReader.GetString(5),
                            LogisticsWdith = MyReader.GetInt32(6),
                            LogisticsHeight = MyReader.GetInt32(7)
                        },
                        Sort = MyReader.GetInt32(8),
                        Number = MyReader.GetInt32(9)
                    });
                }
            }

            return MyList;
        }

        public IList<tbShopLogistics> SelectByUser(int UserId) { 
            IList<tbShopLogistics> MyList = new List<tbShopLogistics>();
            IDbDataParameter[] parm = GetSelectByUserIdParam();
            parm[0].Value = UserId;

            using (IDataReader MyReader = DBHelper.ExecuteReader(CommandType.StoredProcedure, SQL_SELECT_UID, parm)) {
                while (MyReader.Read()) {
                    MyList.Add(new tbShopLogistics() {
                        SLID = MyReader.GetInt32(0),
                        ShopId = MyReader.GetInt32(1),
                        Logistics = new tbLogistics() {
                            LogisticsId = MyReader.GetInt32(2),
                            LogisticsName = MyReader.GetString(3),
                            LogisticsCode = MyReader.GetString(4),
                            LogisticsImg = MyReader.GetString(5),
                            LogisticsWdith = MyReader.GetInt32(6),
                            LogisticsHeight = MyReader.GetInt32(7)
                        },
                        Sort = MyReader.GetInt32(8),
                        Number = MyReader.GetInt32(9)
                    });
                }
            }

            return MyList;
        }

        public bool Insert(tbShopLogistics ShopLogistics) {
            bool IsOk = false;

            IDbDataParameter[] parm = GetInsertParam();
            parm[0].Value = ShopLogistics.ShopId;
            parm[1].Value = ShopLogistics.LogisticsId;
            parm[2].Value = ShopLogistics.Sort;
            parm[3].Value = ShopLogistics.Number;

            try {
                DBHelper.ExecuteNonQuery(CommandType.StoredProcedure, SQL_INSERT, parm);
                IsOk = true;
            } catch { }

            return IsOk;
        }

        public bool Delete(int ShopId) {
            bool IsOk = false;

            IDbDataParameter[] parm = GetDeleteBySIdParam();
            parm[0].Value = ShopId;

            try {
                DBHelper.ExecuteNonQuery(CommandType.StoredProcedure, SQL_DELETE_SID, parm);
                IsOk = true;
            } catch { }

            return IsOk;
        }

        private static IDbDataParameter[] GetSelectBySIdParam() {
            IDbDataParameter[] parm = DBHelper.GetCacheParameters(SQL_SELECT_SID);

            if (parm == null) {
                parm = new SqlParameter[] {
                     new SqlParameter(PARM_SID,SqlDbType.Int)
                };

                DBHelper.CacheParameters(SQL_SELECT_SID, parm);
            }

            return parm;
        }

        private static IDbDataParameter[] GetSelectByIdListParam() {
            IDbDataParameter[] parm = DBHelper.GetCacheParameters(SQL_SELECT_IDLIST);

            if (parm == null) {
                parm = new SqlParameter[] {
                     new SqlParameter(PARM_IDLIST,SqlDbType.VarChar,5000)
                };

                DBHelper.CacheParameters(SQL_SELECT_IDLIST, parm);
            }

            return parm;
        }

        private static IDbDataParameter[] GetSelectInfoParam() {
            IDbDataParameter[] parm = DBHelper.GetCacheParameters(SQL_SELECT_INFO);

            if (parm == null) {
                parm = new SqlParameter[] {
                     new SqlParameter(PARM_SID,SqlDbType.Int)
                };

                DBHelper.CacheParameters(SQL_SELECT_INFO, parm);
            }

            return parm;
        }

        private static IDbDataParameter[] GetSelectByUserIdParam() {
            IDbDataParameter[] parm = DBHelper.GetCacheParameters(SQL_SELECT_UID);

            if (parm == null) {
                parm = new SqlParameter[] {
                     new SqlParameter(PARM_UID,SqlDbType.Int)
                };

                DBHelper.CacheParameters(SQL_SELECT_UID, parm);
            }

            return parm;
        }

        private static IDbDataParameter[] GetInsertParam() {
            IDbDataParameter[] parm = DBHelper.GetCacheParameters(SQL_INSERT);

            if (parm == null) {
                parm = new SqlParameter[] {
                     new SqlParameter(PARM_SID,SqlDbType.Int),
                     new SqlParameter(PARM_LID,SqlDbType.Int),
                     new SqlParameter(PARM_SORT,SqlDbType.Int),
                     new SqlParameter(PARM_NUMBER,SqlDbType.Int)
                };

                DBHelper.CacheParameters(SQL_INSERT, parm);
            }

            return parm;
        }

        private static IDbDataParameter[] GetDeleteBySIdParam() {
            IDbDataParameter[] parm = DBHelper.GetCacheParameters(SQL_DELETE_SID);

            if (parm == null) {
                parm = new SqlParameter[] {
                     new SqlParameter(PARM_SID,SqlDbType.Int)
                };

                DBHelper.CacheParameters(SQL_DELETE_SID, parm);
            }

            return parm;
        }
    }
}
