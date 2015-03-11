using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Interface.Print;
using My.App.Entity.Print;
using System.Data;
using My.App.DBUtility;
using System.Data.SqlClient;

namespace My.App.Data.SqlServer.Print
{
    public class PrintPlaneSingleDetail : IPrintPlaneSingleDetail
    {
        private const string SQL_SELECT_ID = "SelectPrintPlaneSingleDetailByPid";                                       //根据面单模板编号查询打印面单模板明细信息列表
        private const string SQL_INSERT = "InsertPrintPlaneSingleDetail";                                               //添加打印面单模板明细信息
        private const string SQL_DELETE = "DeletePrintPlaneSingleDetail";                                               //删除打印面单模板明细信息

        private const string PARM_PID = "@PlaneId";                                                                     //面单模板编号
        private const string PARM_CID = "@ContentId";                                                                   //打印内容编号
        private const string PARM_TITLE = "@Title";                                                                     //打印内容名称
        private const string PARM_FONE = "@Font";                                                                       //打印字体
        private const string PARM_SIZE = "@FontSize";                                                                   //字体尺寸
        private const string PARM_BOLD = "@Bold";                                                                       //是否加粗
        private const string PARM_SUB = "@SubList";                                                                     //子项列表
        private const string PARM_NUMBER = "@Number";                                                                   //打印条数
        private const string PARM_LEFT = "@Left";                                                                       //居左距离
        private const string PARM_TOP = "@Top";                                                                         //居上距离
        private const string PARM_WIDTH = "@Width";                                                                     //宽度
        private const string PARM_HEIGHT = "@Height";                                                                   //高度
        private const string PARM_ALIGN = "@Align";                                                                     //对齐方式

        public IList<tbPrintPlaneSingleDetail> Select(int PlaneId) {
            IList<tbPrintPlaneSingleDetail> MyList = new List<tbPrintPlaneSingleDetail>();
            IDbDataParameter[] parm = GetSelectParam();
            parm[0].Value = PlaneId;

            using (IDataReader MyReader = DBHelper.ExecuteReader(CommandType.StoredProcedure, SQL_SELECT_ID, parm)) {
                while (MyReader.Read()) {
                    MyList.Add(new tbPrintPlaneSingleDetail() {
                        DetailId = MyReader.GetInt32(0),
                        PlaneId = MyReader.GetInt32(1),
                        ContentId = MyReader.GetInt32(2),
                        Title = MyReader.GetString(3),
                        Font = MyReader.GetInt32(4),
                        FontSize = MyReader.GetInt32(5),
                        Bold = MyReader.GetBoolean(6),
                        SubList = MyReader.GetString(7),
                        Number = MyReader.GetInt32(8),
                        Left = MyReader.GetInt32(9),
                        Top = MyReader.GetInt32(10),
                        Width = MyReader.GetInt32(11),
                        Height = MyReader.GetInt32(12),
                        Align = MyReader.GetInt32(13)
                    });
                }
            }

            return MyList;
        }

        public bool Insert(tbPrintPlaneSingleDetail PrintPlaneSingleDetail) {
            bool IsOk = false;

            IDbDataParameter[] parm = GetInsertParam();
            parm[0].Value = PrintPlaneSingleDetail.PlaneId;
            parm[1].Value = PrintPlaneSingleDetail.ContentId;
            parm[2].Value = PrintPlaneSingleDetail.Title;
            parm[3].Value = PrintPlaneSingleDetail.Font;
            parm[4].Value = PrintPlaneSingleDetail.FontSize;
            parm[5].Value = PrintPlaneSingleDetail.Bold;
            parm[6].Value = PrintPlaneSingleDetail.SubList;
            parm[7].Value = PrintPlaneSingleDetail.Number;
            parm[8].Value = PrintPlaneSingleDetail.Left;
            parm[9].Value = PrintPlaneSingleDetail.Top;
            parm[10].Value = PrintPlaneSingleDetail.Width;
            parm[11].Value = PrintPlaneSingleDetail.Height;
            parm[12].Value = PrintPlaneSingleDetail.Align;

            try {
                DBHelper.ExecuteNonQuery(CommandType.StoredProcedure, SQL_INSERT, parm);
                IsOk = true;
            } catch { }

            return IsOk;
        }

        public bool Delete(int PlaneId) {
            bool IsOk = false;

            IDbDataParameter[] parm = GetDeleteParam();
            parm[0].Value = PlaneId;

            try {
                DBHelper.ExecuteNonQuery(CommandType.StoredProcedure, SQL_DELETE, parm);
                IsOk = true;
            } catch { }

            return IsOk;
        }

        private static IDbDataParameter[] GetSelectParam() {
            IDbDataParameter[] parm = DBHelper.GetCacheParameters(SQL_SELECT_ID);

            if (parm == null) {
                parm = new SqlParameter[] {
                     new SqlParameter(PARM_PID,SqlDbType.Int)
                };

                DBHelper.CacheParameters(SQL_SELECT_ID, parm);
            }

            return parm;
        }

        private static IDbDataParameter[] GetInsertParam() {
            IDbDataParameter[] parm = DBHelper.GetCacheParameters(SQL_INSERT);

            if (parm == null) {
                parm = new SqlParameter[] {
                     new SqlParameter(PARM_PID,SqlDbType.Int),
                     new SqlParameter(PARM_CID,SqlDbType.Int),
                     new SqlParameter(PARM_TITLE,SqlDbType.VarChar,50),
                     new SqlParameter(PARM_FONE,SqlDbType.Int),
                     new SqlParameter(PARM_SIZE,SqlDbType.Int),
                     new SqlParameter(PARM_BOLD,SqlDbType.Bit),
                     new SqlParameter(PARM_SUB,SqlDbType.VarChar,200),
                     new SqlParameter(PARM_NUMBER,SqlDbType.Int),
                     new SqlParameter(PARM_LEFT,SqlDbType.Int),
                     new SqlParameter(PARM_TOP,SqlDbType.Int),
                     new SqlParameter(PARM_WIDTH,SqlDbType.Int),
                     new SqlParameter(PARM_HEIGHT,SqlDbType.Int),
                     new SqlParameter(PARM_ALIGN,SqlDbType.Int)
                };

                DBHelper.CacheParameters(SQL_INSERT, parm);
            }

            return parm;
        }

        private static IDbDataParameter[] GetDeleteParam() {
            IDbDataParameter[] parm = DBHelper.GetCacheParameters(SQL_DELETE);

            if (parm == null) {
                parm = new SqlParameter[] {
                     new SqlParameter(PARM_PID,SqlDbType.Int)
                };

                DBHelper.CacheParameters(SQL_DELETE, parm);
            }

            return parm;
        }
    }
}