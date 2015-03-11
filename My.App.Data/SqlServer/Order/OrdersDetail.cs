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
    public class OrdersDetail : IOrdersDetail
    {
        private const string SQL_SELECT = "SelectOrdersDetail";                                         //根据订单编号查询订单详情信息列表
        private const string SQL_INSERT = "InsertOrdersDetail";                                         //添加订单详情信息

        private const string PARM_ID = "@OrdersDetailId";                                               //订单明细编号  
        private const string PARM_NUMBER = "@OrdersNumber";                                             //订单编号
        private const string PARM_PID = "@ProductId";                                                   //商品编号
        private const string PARM_PROID = "@ProductProId";                                              //商品属性编号
        private const string PARM_SCID = "@SalesCommissionId";                                          //销售提成编号
        private const string PARM_ENCODING = "@ProductEncoding";                                        //商家编码
        private const string PARM_IMG = "@ProductImg";                                                  //商品图片
        private const string PARM_NAME = "@ProductName";                                                //商品名称
        private const string PARM_PNUMBER = "@ProductNumber";                                           //商品数量
        private const string PARM_PRICE = "@ProductPrice";                                              //商品单价
        private const string PARM_SKU = "@ProductSKU";                                                  //商品属性
        private const string PARM_TOTAL = "@ProductTotal";                                              //商品总价
        private const string PARM_FUND = "@IsProductReFund";                                            //是否有退款
        private const string PARM_CANCEL = "@IsCanceled";                                               //是否已取消
        private const string PARM_FUNDID = "@ReFundStatusId";                                           //退款状态编号(预留)
        private const string PARM_FUNDNAME = "@ReFundStatusName";                                       //退款状态名称(预留)
        private const string PARM_FUNDNUMBER = "@ReFundNumber";                                         //退款编号
        private const string PARM_PACKNAME = "@PackageName";                                            //商品套餐名称
        private const string PARM_DETAIL = "@Details";                                                  //备注
        private const string PARM_DATE = "@InputDate";                                                  //录入日期
        private const string PARM_SUB_NUMBER = "@SubOrderNumber";                                       //子订单编号
        private const string PARM_NUM_IID = "@OutNumberIId";                                            //商品外部编号
        private const string PARM_PROD_COST = "@ProductCost";                                           //商品成本
        private const string PARM_DISCOUNT = "@OrdersDiscount";                                         //优惠金额
        private const string PARM_ADJUST = "@OrdersAdjust";                                             //手动调整金额
        private const string PARM_ACCOUNT = "@OrdersAccounts";                                          //应付金额                            

        public IList<tbOrdersDetail> Select(string OrdersNumber) {
            IList<tbOrdersDetail> MyList = new List<tbOrdersDetail>();
            IDbDataParameter[] parm = GetSelectNumberParam();
            parm[0].Value = OrdersNumber;

            using (IDataReader MyReader = DBHelper.ExecuteReader(CommandType.StoredProcedure, SQL_SELECT, parm)) {
                while (MyReader.Read()) {
                    MyList.Add(new tbOrdersDetail() { 
                        OrdersDetailId = MyReader.GetInt32(0),
                        OrdersNumber = MyReader.GetString(1),
                        ProductId = MyReader.GetInt32(2),
                        ProductProId = MyReader.GetInt32(3),
                        SalesCommissionId = MyReader.GetInt32(4),
                        ProductEncoding = MyReader.GetString(5),
                        ProductImg = MyReader.GetString(6),
                        ProductName = MyReader.GetString(7),
                        ProductNumber = MyReader.GetInt32(8),
                        ProductPrice = MyReader.GetDecimal(9),
                        ProductSku = MyReader.GetString(10),
                        ProductTotal = MyReader.GetDecimal(11),
                        IsProductReFund = MyReader.GetBoolean(12),
                        IsCanceled = MyReader.GetBoolean(13),
                        ReFundStatusId = MyReader.GetInt32(14),
                        ReFundStatusName = MyReader.GetString(15),
                        ReFundNumber = MyReader.GetString(16),
                        PackageName = MyReader.GetString(17),
                        Details = MyReader.GetString(18),
                        InputDate = MyReader.GetDateTime(19),
                        SubOrderNumber = MyReader.GetString(20),
                        OutNumberIId = MyReader.GetString(21),
                        ProductCost = MyReader.GetDecimal(22),
                        OrdersDiscount = MyReader.GetDecimal(23),
                        OrdersAdjust = MyReader.GetDecimal(24),
                        OrdersAccounts = MyReader.GetDecimal(25)
                    });
                }
            }

            return MyList;
        }

        public bool Insert(tbOrdersDetail OrdersDetail) {
            bool IsOk = false;

            IDbDataParameter[] parm = GetInsertParam();
            parm[0].Value = OrdersDetail.OrdersNumber;
            parm[1].Value = OrdersDetail.ProductId;
            parm[2].Value = OrdersDetail.ProductProId;
            parm[3].Value = OrdersDetail.SalesCommissionId;
            parm[4].Value = OrdersDetail.ProductEncoding;
            parm[5].Value = OrdersDetail.ProductImg;
            parm[6].Value = OrdersDetail.ProductName;
            parm[7].Value = OrdersDetail.ProductNumber;
            parm[8].Value = OrdersDetail.ProductPrice;
            parm[9].Value = OrdersDetail.ProductSku;
            parm[10].Value = OrdersDetail.ProductTotal;
            parm[11].Value = OrdersDetail.IsProductReFund;
            parm[12].Value = OrdersDetail.IsCanceled;
            parm[13].Value = OrdersDetail.ReFundStatusId;
            parm[14].Value = OrdersDetail.ReFundStatusName;
            parm[15].Value = OrdersDetail.ReFundNumber;
            parm[16].Value = OrdersDetail.PackageName;
            parm[17].Value = OrdersDetail.Details;
            parm[18].Value = OrdersDetail.InputDate;
            parm[19].Value = OrdersDetail.SubOrderNumber;
            parm[20].Value = OrdersDetail.OutNumberIId;
            parm[21].Value = OrdersDetail.ProductCost;
            parm[22].Value = OrdersDetail.OrdersDiscount;
            parm[23].Value = OrdersDetail.OrdersAdjust;
            parm[24].Value = OrdersDetail.OrdersAccounts;

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
                     new SqlParameter(PARM_PID,SqlDbType.Int,4),
                     new SqlParameter(PARM_PROID,SqlDbType.Int,4),
                     new SqlParameter(PARM_SCID,SqlDbType.Int,4),
                     new SqlParameter(PARM_ENCODING,SqlDbType.VarChar,60),
                     new SqlParameter(PARM_IMG,SqlDbType.VarChar,200),
                     new SqlParameter(PARM_NAME,SqlDbType.VarChar,200),
                     new SqlParameter(PARM_PNUMBER,SqlDbType.Int,4),
                     new SqlParameter(PARM_PRICE,SqlDbType.Decimal),
                     new SqlParameter(PARM_SKU,SqlDbType.VarChar,200),
                     new SqlParameter(PARM_TOTAL,SqlDbType.Decimal),
                     new SqlParameter(PARM_FUND,SqlDbType.Bit),
                     new SqlParameter(PARM_CANCEL,SqlDbType.Bit),
                     new SqlParameter(PARM_FUNDID,SqlDbType.Int,4),
                     new SqlParameter(PARM_FUNDNAME,SqlDbType.VarChar,50),
                     new SqlParameter(PARM_FUNDNUMBER,SqlDbType.VarChar,50),
                     new SqlParameter(PARM_PACKNAME,SqlDbType.VarChar,50),
                     new SqlParameter(PARM_DETAIL,SqlDbType.VarChar,500),
                     new SqlParameter(PARM_DATE,SqlDbType.DateTime),
                     new SqlParameter(PARM_SUB_NUMBER,SqlDbType.VarChar,50),
                     new SqlParameter(PARM_NUM_IID,SqlDbType.VarChar,50),
                     new SqlParameter(PARM_PROD_COST,SqlDbType.Decimal),
                     new SqlParameter(PARM_DISCOUNT,SqlDbType.Decimal),
                     new SqlParameter(PARM_ADJUST,SqlDbType.Decimal),
                     new SqlParameter(PARM_ACCOUNT,SqlDbType.Decimal)
                };

                DBHelper.CacheParameters(SQL_INSERT, parm);
            }

            return parm;
        }
    }
}