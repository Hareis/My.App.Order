using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Interface.Shop;
using My.App.Entity.Shop;
using System.Data;
using My.App.DBUtility;
using System.Data.SqlClient;

namespace My.App.Data.SqlServer.Shop
{
    public class ShopInfo : IShopInfo
    {
        private const string SQL_SELECT_UID = "SelectShopInfoByUserId";                                         //根据用户编号查询店铺信息列表
        private const string SQL_INSERT = "InsertShopInfo";                                                     //添加店铺信息

        private const string PARM_PFID = "@PfId";                                                               //平台编号
        private const string PARM_VID = "@VenderId";                                                            //商家编号
        private const string PARM_SID = "@OutShopId";                                                           //外部店铺编号
        private const string PARM_SNAME = "@ShopName";                                                          //店铺名称
        private const string PARM_NNAME = "@NickName";                                                          //卖家昵称
        private const string PARM_DESC = "@ShopDesc";                                                           //店铺描述
        private const string PARM_BULL = "@Bulletin";                                                           //店铺公告
        private const string PARM_CATEID = "@CategoryId";                                                       //主营类目编号
        private const string PARM_CATENAME = "@CategoryName";                                                   //主营类目名称
        private const string PARM_URL = "@LogoUrl";                                                             //店标地址
        private const string PARM_TIME = "@CreateTime";                                                         //开店日期
        private const string PARM_DATE = "@InputDate";                                                          //录入日期
        private const string PARM_UID = "@UserId";                                                              //用户编号

        public IList<tbShopInfo> Select(int UserId) {
            IList<tbShopInfo> MyList = new List<tbShopInfo>();
            IDbDataParameter[] parm = GetSelectByUIdParam();
            parm[0].Value = UserId;

            using (IDataReader MyReader = DBHelper.ExecuteReader(CommandType.StoredProcedure, SQL_SELECT_UID, parm)) {
                while (MyReader.Read()) {
                    MyList.Add(new tbShopInfo() {
                        ShopId = MyReader.GetInt32(0),
                        PfId = MyReader.GetInt32(1),
                        VenderId = MyReader.GetString(2),
                        OutShopId = MyReader.GetInt32(3),
                        ShopName = MyReader.GetString(4),
                        NickName = MyReader.GetString(5),
                        ShopDesc = MyReader.GetString(6),
                        Bulletin = MyReader.GetString(7),
                        CategoryId = MyReader.GetInt32(8),
                        CategoryName = MyReader.GetString(9),
                        LogoUrl = MyReader.GetString(10),
                        CreateTime = MyReader.GetString(11),
                        InputDate = MyReader.GetDateTime(12),
                        UserId = MyReader.GetInt32(13)
                    });
                }
            }

            return MyList;
        }

        public int Insert(tbShopInfo ShopInfo) {
            int Id = 0;

            IDbDataParameter[] parm = GetInsertParam();
            parm[0].Value = ShopInfo.PfId;
            parm[1].Value = ShopInfo.VenderId;
            parm[2].Value = ShopInfo.OutShopId;
            parm[3].Value = ShopInfo.ShopName;
            parm[4].Value = ShopInfo.NickName;
            parm[5].Value = ShopInfo.ShopDesc;
            parm[6].Value = ShopInfo.Bulletin;
            parm[7].Value = ShopInfo.CategoryId;
            parm[8].Value = ShopInfo.CategoryName;
            parm[9].Value = ShopInfo.LogoUrl;
            parm[10].Value = ShopInfo.CreateTime;
            parm[11].Value = ShopInfo.InputDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
            parm[12].Value = ShopInfo.UserId;

            try {
                using (IDataReader MyReader = DBHelper.ExecuteReader(CommandType.StoredProcedure, SQL_INSERT, parm)) {
                    if (MyReader.Read()) {
                        Id = MyReader.GetInt32(0);
                    }
                }
            } catch { }

            return Id;
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

        private static IDbDataParameter[] GetInsertParam() {
            IDbDataParameter[] parm = DBHelper.GetCacheParameters(SQL_INSERT);

            if (parm == null) {
                parm = new SqlParameter[] {
                     new SqlParameter(PARM_PFID,SqlDbType.Int),
                     new SqlParameter(PARM_VID,SqlDbType.VarChar,50),
                     new SqlParameter(PARM_SID,SqlDbType.Int),
                     new SqlParameter(PARM_SNAME,SqlDbType.VarChar,50),
                     new SqlParameter(PARM_NNAME,SqlDbType.VarChar,50),
                     new SqlParameter(PARM_DESC,SqlDbType.VarChar,5000),
                     new SqlParameter(PARM_BULL,SqlDbType.Text),
                     new SqlParameter(PARM_CATEID,SqlDbType.Int),
                     new SqlParameter(PARM_CATENAME,SqlDbType.VarChar,50),
                     new SqlParameter(PARM_URL,SqlDbType.VarChar,500),
                     new SqlParameter(PARM_TIME,SqlDbType.VarChar,50),
                     new SqlParameter(PARM_DATE,SqlDbType.DateTime),
                     new SqlParameter(PARM_UID,SqlDbType.Int)
                };

                DBHelper.CacheParameters(SQL_INSERT, parm);
            }

            return parm;
        }
    }
}