using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Interface.Product;
using My.App.Entity.Product;
using System.Data;
using My.App.DBUtility;
using System.Data.SqlClient;

namespace My.App.Data.SqlServer.Product
{
    public class ProductAbb : IProductAbb
    {
        private const string SQL_PAGE_SELECT = "PageCurrent";                                               //查询商品简称信息列表（分页）
        private const string SQL_SELECT_SOID = "SelectProductAbbBySIdAndOId";                               //根据店铺编号和商品外部编号查询商品简称
        private const string SQL_SELECT_WHERE = "SelectProductAbbByWhere";                                  //根据查询条件获取商品简称汇总信息
        private const string SQL_INSERT = "InsertProductAbb";                                               //添加商品简称信息
        private const string SQL_UPDATE_PID = "UpdateProductAbbByPid";                                      //修改商品简称
        private const string SQL_UPDATE_DISPLAY = "UpdateProductAbbDisplay";                                //根据店铺编号修改商品简称显示状态
        private const string SQL_DELETE_DISPLAY = "DeleteProductAbbDisplay";                                //根据店铺编号和显示状态删除商品简称信息

        private const string PARM_TABLENAME = "@TableName";                                                 //表名
        private const string PARM_FIELD = "@Fields";                                                        //字段名(全部字段为*)
        private const string PARM_ORDERFIELD = "@OrderField";                                               //排序字段(必须!支持多字段)
        private const string PARM_WHERE = "@sqlWhere";                                                      //条件语句(不用加where)
        private const string PARM_PAGESIZE = "@pageSize";                                                   //每页多少条记录
        private const string PARM_PAGEINDEX = "@pageIndex";                                                 //指定当前为第几页
        private const string PARM_DISTINCT = "@distinct";                                                   //去除重复值，注意只能是一个字段
        private const string PARM_TOP = "@top";                                                             //查询TOP,不传为全部

        private const string PARM_ID = "@ProductId";                                                        //商品编号
        private const string PARM_SID = "@ShopId";                                                          //店铺编号
        private const string PARM_OID = "@OutId";                                                           //商品外部编号
        private const string PARM_NAME = "@ProductName";                                                    //商品名称
        private const string PARM_IMG = "@ProductImg";                                                      //商品图片
        private const string PARM_URL = "@ProductUrl";                                                      //商品地址
        private const string PARM_PRICE = "@ProductPrice";                                                  //商品价格
        private const string PARM_NUMBER = "@ProductNumber";                                                //商品数量
        private const string PARM_ABB = "@Abbreviation";                                                    //商品简称
        private const string PARM_DATE = "@InputDate";                                                      //录入日期
        private const string PARM_DISPLAY = "@Display";                                                     //是否显示

        public IList<tbProductAbb> Select(int PageCount, int pageSize, string Where, string Order, out int MaxRow, out int MaxPage) {
            IList<tbProductAbb> MyList = new List<tbProductAbb>();
            IDbDataParameter[] parm = GetPageSelectParam();
            parm[0].Value = "tbProductAbb";
            parm[1].Value = "ProductId,ShopId,OutId,ProductName,ProductImg,ProductUrl,ProductPrice,ProductNumber,Abbreviation,InputDate,Display";
            parm[2].Value = String.IsNullOrEmpty(Order) ? " InputDate desc" : Order.Trim();
            parm[3].Value = " Display='true' " + Where;
            parm[4].Value = pageSize == 0 ? 20 : pageSize;
            parm[5].Value = PageCount;
            parm[6].Value = "";
            parm[7].Value = null;

            using (IDataReader MyReader = DBHelper.ExecuteReader(CommandType.StoredProcedure, SQL_PAGE_SELECT, parm)) {
                if (MyReader.Read()) {
                    MaxRow = MyReader.GetInt32(0);
                    MaxPage = MyReader.GetInt32(1);

                    if (MyReader.NextResult()) {
                        while (MyReader.Read()) {
                            MyList.Add(new tbProductAbb() {
                                ProductId = MyReader.GetInt32(0),
                                ShopId = MyReader.GetInt32(1),
                                OutId = MyReader.GetString(2),
                                ProductName = MyReader.GetString(3),
                                ProductImg = MyReader.GetString(4),
                                ProductUrl = MyReader.GetString(5),
                                ProductPrice = MyReader.GetDecimal(6),
                                ProductNumber = MyReader.GetInt32(7),
                                Abbreviation = MyReader.GetString(8),
                                InputDate = MyReader.GetDateTime(9),
                                Display = MyReader.GetBoolean(10)
                            });
                        }
                    }
                } else {
                    MaxRow = 0;
                    MaxPage = 0;
                }
            }

            return MyList;
        }

        public string Select(int ShopId, string OutId) {
            string Abbreviation = "";

            IDbDataParameter[] parm = GetSelectBySOIdParam();
            parm[0].Value = ShopId;
            parm[1].Value = OutId;

            try {
                using (IDataReader MyReader = DBHelper.ExecuteReader(CommandType.StoredProcedure, SQL_SELECT_SOID, parm)) {
                    if (MyReader.Read()) {
                        Abbreviation = MyReader.GetString(0);
                    }
                }
            }
            catch { }

            return Abbreviation;
        }

        public IList<int> Select(string where) {
            IList<int> List = new List<int>();

            IDbDataParameter[] parm = GetSelectWhereParam();
            parm[0].Value = where;

            try {
                using (IDataReader MyReader = DBHelper.ExecuteReader(CommandType.StoredProcedure, SQL_SELECT_WHERE, parm)) {
                    int Y = 0; int N = 0;
                    while (MyReader.Read()) {
                        Y = MyReader.GetInt32(0);
                        N = MyReader.GetInt32(1);
                    }
                    List.Add(Y); List.Add(N); List.Add(Y + N);
                }
            }
            catch { }

            return List;
        }

        public bool Insert(tbProductAbb ProductAbb) {
            bool IsOk = false;

            IDbDataParameter[] parm = GetInsertParam();
            parm[0].Value = ProductAbb.ShopId;
            parm[1].Value = ProductAbb.OutId;
            parm[2].Value = ProductAbb.ProductName;
            parm[3].Value = ProductAbb.ProductImg;
            parm[4].Value = ProductAbb.ProductUrl;
            parm[5].Value = ProductAbb.ProductPrice;
            parm[6].Value = ProductAbb.ProductNumber;
            parm[7].Value = ProductAbb.Abbreviation;
            parm[8].Value = ProductAbb.InputDate;
            parm[9].Value = ProductAbb.Display;

            try {
                DBHelper.ExecuteNonQuery(CommandType.StoredProcedure, SQL_INSERT, parm);
                IsOk = true;
            }
            catch { }

            return IsOk;
        }

        public bool Update(int ProductId, string Abbreviation) {
            bool IsOk = false;

            IDbDataParameter[] parm = GetUpdateByPIdParam();
            parm[0].Value = ProductId;
            parm[1].Value = Abbreviation;

            try {
                DBHelper.ExecuteNonQuery(CommandType.StoredProcedure, SQL_UPDATE_PID, parm);
                IsOk = true;
            }
            catch { }

            return IsOk;
        }

        public bool Update(int ShopId, bool Display) {
            bool IsOk = false;

            IDbDataParameter[] parm = GetUpdateDisplayParam();
            parm[0].Value = ShopId;
            parm[1].Value = Display;

            try {
                DBHelper.ExecuteNonQuery(CommandType.StoredProcedure, SQL_UPDATE_DISPLAY, parm);
                IsOk = true;
            }
            catch { }

            return IsOk;
        }

        public bool Delete(int ShopId, bool Display) {
            bool IsOk = false;

            IDbDataParameter[] parm = GetDeleteDisplayParam();
            parm[0].Value = ShopId;
            parm[1].Value = Display;

            try {
                DBHelper.ExecuteNonQuery(CommandType.StoredProcedure, SQL_DELETE_DISPLAY, parm);
                IsOk = true;
            }
            catch { }

            return IsOk;
        }

        

        private static IDbDataParameter[] GetPageSelectParam() {
            IDbDataParameter[] parm = DBHelper.GetCacheParameters(SQL_PAGE_SELECT);

            if (parm == null) {
                parm = new SqlParameter[] {
                     new SqlParameter(PARM_TABLENAME,SqlDbType.VarChar,5000),
                     new SqlParameter(PARM_FIELD,SqlDbType.VarChar,5000),
                     new SqlParameter(PARM_ORDERFIELD,SqlDbType.VarChar,5000),
                     new SqlParameter(PARM_WHERE,SqlDbType.VarChar,5000),
                     new SqlParameter(PARM_PAGESIZE,SqlDbType.Int,4),
                     new SqlParameter(PARM_PAGEINDEX,SqlDbType.Int,4),
                     new SqlParameter(PARM_DISTINCT,SqlDbType.VarChar,50),
                     new SqlParameter(PARM_TOP,SqlDbType.Int,4)
                };

                DBHelper.CacheParameters(SQL_PAGE_SELECT, parm);
            }

            return parm;
        }

        private static IDbDataParameter[] GetSelectBySOIdParam() {
            IDbDataParameter[] parm = DBHelper.GetCacheParameters(SQL_SELECT_SOID);

            if (parm == null) {
                parm = new SqlParameter[] {
                     new SqlParameter(PARM_SID,SqlDbType.Int,4),
                     new SqlParameter(PARM_OID,SqlDbType.VarChar,50)
                };

                DBHelper.CacheParameters(SQL_SELECT_SOID, parm);
            }

            return parm;
        }

        private static IDbDataParameter[] GetSelectWhereParam() {
            IDbDataParameter[] parm = DBHelper.GetCacheParameters(SQL_SELECT_WHERE);

            if (parm == null) {
                parm = new SqlParameter[] {
                     new SqlParameter(PARM_WHERE,SqlDbType.VarChar,5000)
                };

                DBHelper.CacheParameters(SQL_SELECT_WHERE, parm);
            }

            return parm;
        }

        private static IDbDataParameter[] GetInsertParam() {
            IDbDataParameter[] parm = DBHelper.GetCacheParameters(SQL_INSERT);

            if (parm == null) {
                parm = new SqlParameter[] {
                     new SqlParameter(PARM_SID,SqlDbType.Int,4),
                     new SqlParameter(PARM_OID,SqlDbType.VarChar,50),
                     new SqlParameter(PARM_NAME,SqlDbType.VarChar,100),
                     new SqlParameter(PARM_IMG,SqlDbType.VarChar,200),
                     new SqlParameter(PARM_URL,SqlDbType.VarChar,200),
                     new SqlParameter(PARM_PRICE,SqlDbType.Decimal),
                     new SqlParameter(PARM_NUMBER,SqlDbType.Int,4),
                     new SqlParameter(PARM_ABB,SqlDbType.VarChar,100),
                     new SqlParameter(PARM_DATE,SqlDbType.DateTime),
                     new SqlParameter(PARM_DISPLAY,SqlDbType.Bit)
                };

                DBHelper.CacheParameters(SQL_INSERT, parm);
            }

            return parm;
        }

        private static IDbDataParameter[] GetUpdateByPIdParam() {
            IDbDataParameter[] parm = DBHelper.GetCacheParameters(SQL_UPDATE_PID);

            if (parm == null) {
                parm = new SqlParameter[] {
                     new SqlParameter(PARM_ID,SqlDbType.Int,4),
                     new SqlParameter(PARM_ABB,SqlDbType.VarChar,100)
                };

                DBHelper.CacheParameters(SQL_UPDATE_PID, parm);
            }

            return parm;
        }

        private static IDbDataParameter[] GetUpdateDisplayParam() {
            IDbDataParameter[] parm = DBHelper.GetCacheParameters(SQL_UPDATE_DISPLAY);

            if (parm == null) {
                parm = new SqlParameter[] {
                     new SqlParameter(PARM_SID,SqlDbType.Int,4),
                     new SqlParameter(PARM_DISPLAY,SqlDbType.Bit)
                };

                DBHelper.CacheParameters(SQL_UPDATE_DISPLAY, parm);
            }

            return parm;
        }

        private static IDbDataParameter[] GetDeleteDisplayParam() {
            IDbDataParameter[] parm = DBHelper.GetCacheParameters(SQL_DELETE_DISPLAY);

            if (parm == null) {
                parm = new SqlParameter[] {
                     new SqlParameter(PARM_SID,SqlDbType.Int,4),
                     new SqlParameter(PARM_DISPLAY,SqlDbType.Bit)
                };

                DBHelper.CacheParameters(SQL_DELETE_DISPLAY, parm);
            }

            return parm;
        }
    }
}