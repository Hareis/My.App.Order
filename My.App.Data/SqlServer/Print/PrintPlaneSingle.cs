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
    public class PrintPlaneSingle : IPrintPlaneSingle
    {
        private const string SQL_SELECT_IDS = "SelectPrintPlaneSingleByIds";                                        //根据物流编号和用户编号查询打印面单模板信息
        private const string SQL_SELECT_STATE = "SelectPrintPlaneSingleByIdAndState";                               //根据用户编号和模板状态查询打印面单模板信息
        private const string SQL_INSERT = "InsertPrintPlaneSingle";                                                 //添加打印面单模板信息
        private const string SQL_UPDATE = "UpdatePrintPlaneSingle";                                                 //修改打印面单模板信息
        private const string SQL_DELETE = "DeletePrintPlaneSingle";                                                 //根据打印面单模板编号删除打印单面模板信息

        private const string PARM_ID = "@PlaneId";                                                                  //面单模板编号
        private const string PARM_LID = "@LogisticsId";                                                             //物流编号
        private const string PARM_UID = "@UserId";                                                                  //用户编号
        private const string PARM_IMAGE = "@Image";                                                                 //模板图片
        private const string PARM_WIDTH = "@Width";                                                                 //模板宽度(毫米)
        private const string PARM_HEIGHT = "@Height";                                                               //模板高度(毫米)
        private const string PARM_SIGNLE = "@IsSingle";                                                             //是否面单模板

        public tbPrintPlaneSingle Select(int LogisticsId, int UserId) {
            tbPrintPlaneSingle Print = null;
            IDbDataParameter[] parm = GetSelectByIdsParam();
            parm[0].Value = LogisticsId;
            parm[1].Value = UserId;

            using (IDataReader MyReader = DBHelper.ExecuteReader(CommandType.StoredProcedure, SQL_SELECT_IDS, parm)) {
                if (MyReader.Read()) {
                    Print = new tbPrintPlaneSingle() {
                        PlaneId = MyReader.GetInt32(0),
                        LogisticsId = MyReader.GetInt32(1),
                        UserId = MyReader.GetInt32(2),
                        Image = MyReader.GetString(3),
                        Width = MyReader.GetInt32(4),
                        Height = MyReader.GetInt32(5),
                        IsSingle = MyReader.GetBoolean(6)
                    };
                }
            }

            return Print == null ? new tbPrintPlaneSingle() : Print;
        }

        public tbPrintPlaneSingle Select(int UserId, bool IsSingle) { 
            tbPrintPlaneSingle Print = null;
            IDbDataParameter[] parm = GetSelectByIdAndStateParam();
            parm[0].Value = UserId;
            parm[1].Value = IsSingle;

            using (IDataReader MyReader = DBHelper.ExecuteReader(CommandType.StoredProcedure, SQL_SELECT_STATE, parm)) {
                if (MyReader.Read()) {
                    Print = new tbPrintPlaneSingle() {
                        PlaneId = MyReader.GetInt32(0),
                        LogisticsId = MyReader.GetInt32(1),
                        UserId = MyReader.GetInt32(2),
                        Image = MyReader.GetString(3),
                        Width = MyReader.GetInt32(4),
                        Height = MyReader.GetInt32(5),
                        IsSingle = MyReader.GetBoolean(6)
                    };
                }
            }

            return Print == null ? new tbPrintPlaneSingle() : Print;
        }

        public int Insert(tbPrintPlaneSingle PrintPlaneSingle) {
            int Id = 0;
            IDbDataParameter[] parm = GetInsertParam();
            parm[0].Value = PrintPlaneSingle.LogisticsId;
            parm[1].Value = PrintPlaneSingle.UserId;
            parm[2].Value = PrintPlaneSingle.Image;
            parm[3].Value = PrintPlaneSingle.Width;
            parm[4].Value = PrintPlaneSingle.Height;
            parm[5].Value = PrintPlaneSingle.IsSingle;

            try {
                using (IDataReader MyReader = DBHelper.ExecuteReader(CommandType.StoredProcedure, SQL_INSERT, parm)) {
                    if (MyReader.Read()) {
                        Id = MyReader.GetInt32(0);
                    }
                }
            } catch { }

            return Id;
        }

        public bool Update(tbPrintPlaneSingle PrintPlaneSingle) {
            bool IsOk = false;

            IDbDataParameter[] parm = GetUpdateParam();
            parm[0].Value = PrintPlaneSingle.PlaneId;
            parm[1].Value = PrintPlaneSingle.LogisticsId;
            parm[2].Value = PrintPlaneSingle.UserId;
            parm[3].Value = PrintPlaneSingle.Image;
            parm[4].Value = PrintPlaneSingle.Width;
            parm[5].Value = PrintPlaneSingle.Height;
            parm[6].Value = PrintPlaneSingle.IsSingle;

            try {
                DBHelper.ExecuteNonQuery(CommandType.StoredProcedure, SQL_UPDATE, parm);
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

        private static IDbDataParameter[] GetSelectByIdsParam() {
            IDbDataParameter[] parm = DBHelper.GetCacheParameters(SQL_SELECT_IDS);

            if (parm == null) {
                parm = new SqlParameter[] {
                     new SqlParameter(PARM_LID,SqlDbType.Int),
                     new SqlParameter(PARM_UID,SqlDbType.Int)
                };

                DBHelper.CacheParameters(SQL_SELECT_IDS, parm);
            }

            return parm;
        }

        private static IDbDataParameter[] GetSelectByIdAndStateParam() {
            IDbDataParameter[] parm = DBHelper.GetCacheParameters(SQL_SELECT_STATE);

            if (parm == null) {
                parm = new SqlParameter[] {
                     new SqlParameter(PARM_UID,SqlDbType.Int),
                     new SqlParameter(PARM_SIGNLE,SqlDbType.Bit)
                };

                DBHelper.CacheParameters(SQL_SELECT_STATE, parm);
            }

            return parm;
        }

        private static IDbDataParameter[] GetInsertParam() {
            IDbDataParameter[] parm = DBHelper.GetCacheParameters(SQL_INSERT);

            if (parm == null) {
                parm = new SqlParameter[] {
                     new SqlParameter(PARM_LID,SqlDbType.Int),
                     new SqlParameter(PARM_UID,SqlDbType.Int),
                     new SqlParameter(PARM_IMAGE,SqlDbType.VarChar,200),
                     new SqlParameter(PARM_WIDTH,SqlDbType.Int),
                     new SqlParameter(PARM_HEIGHT,SqlDbType.Int),
                     new SqlParameter(PARM_SIGNLE,SqlDbType.Bit)
                };

                DBHelper.CacheParameters(SQL_INSERT, parm);
            }

            return parm;
        }

        private static IDbDataParameter[] GetUpdateParam() {
            IDbDataParameter[] parm = DBHelper.GetCacheParameters(SQL_UPDATE);

            if (parm == null) {
                parm = new SqlParameter[] {
                     new SqlParameter(PARM_ID,SqlDbType.Int),
                     new SqlParameter(PARM_LID,SqlDbType.Int),
                     new SqlParameter(PARM_UID,SqlDbType.Int),
                     new SqlParameter(PARM_IMAGE,SqlDbType.VarChar,200),
                     new SqlParameter(PARM_WIDTH,SqlDbType.Int),
                     new SqlParameter(PARM_HEIGHT,SqlDbType.Int),
                     new SqlParameter(PARM_SIGNLE,SqlDbType.Bit)
                };

                DBHelper.CacheParameters(SQL_UPDATE, parm);
            }

            return parm;
        }

        private static IDbDataParameter[] GetDeleteParam() {
            IDbDataParameter[] parm = DBHelper.GetCacheParameters(SQL_DELETE);

            if (parm == null) {
                parm = new SqlParameter[] {
                     new SqlParameter(PARM_ID,SqlDbType.Int)
                };

                DBHelper.CacheParameters(SQL_DELETE, parm);
            }

            return parm;
        }
    }
}
