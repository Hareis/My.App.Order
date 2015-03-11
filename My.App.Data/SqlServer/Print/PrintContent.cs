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
    public class PrintContent : IPrintContent
    {
        private const string SQL_SELECT = "SelectPrintContent";                                                         //查询打印内容信息列表
        private const string SQL_SELECT_DISPLAY = "SelectPrintContentByDisplay";                                        //根据显示状态查询打印内容信息列表
        private const string SQL_SELECT_PID = "SelectPrintContentByPId";                                                //根据父类编号查询打印内容信息列表
        private const string SQL_SELECT_PIDANDDIS = "SelectPrintContentByPIdAndDis";                                    //根据父类编号和显示状态查询打印内容信息列表
        private const string SQL_SELECT_IDLIST = "SelectPrintContentByIdList";                                          //根据打印编号列表查询打印内容信息列表

        private const string PARM_ID = "@ContentId";                                                                    //打印内容编号
        private const string PARM_NAME = "@Name";                                                                       //打印内容名称
        private const string PARM_PID = "@ParentId";                                                                    //父类编号
        private const string PARM_SORT = "@Sort";                                                                       //排序字段
        private const string PARM_DISPLAY = "@Display";                                                                 //是否显示
        private const string PARM_IDLIST = "@IdList";                                                                   //打印内容编号列表

        public IList<tbPrintContent> Select() {
            IList<tbPrintContent> MyList = new List<tbPrintContent>();

            using (IDataReader MyReader = DBHelper.ExecuteReader(CommandType.StoredProcedure, SQL_SELECT)) {
                while (MyReader.Read()) {
                    MyList.Add(new tbPrintContent() {
                        ContentId = MyReader.GetInt32(0),
                        Name = MyReader.GetString(1),
                        ParentId = MyReader.GetInt32(2),
                        Sort = MyReader.GetInt32(3),
                        Display = MyReader.GetBoolean(4)
                    });
                }
            }

            return MyList;
        }

        public IList<tbPrintContent> SelectByDisplay(bool Display) {
            IList<tbPrintContent> MyList = new List<tbPrintContent>();
            IDbDataParameter[] parm = GetSelectDisplayParam();
            parm[0].Value = Display;

            using (IDataReader MyReader = DBHelper.ExecuteReader(CommandType.StoredProcedure, SQL_SELECT_DISPLAY, parm)) {
                while (MyReader.Read()) {
                    MyList.Add(new tbPrintContent() {
                        ContentId = MyReader.GetInt32(0),
                        Name = MyReader.GetString(1),
                        ParentId = MyReader.GetInt32(2),
                        Sort = MyReader.GetInt32(3),
                        Display = MyReader.GetBoolean(4)
                    });
                }
            }

            return MyList;
        }

        public IList<tbPrintContent> SelectByParentId(int ParentId) { 
            IList<tbPrintContent> MyList = new List<tbPrintContent>();
            IDbDataParameter[] parm = GetSelectParentIdParam();
            parm[0].Value = ParentId;

            using (IDataReader MyReader = DBHelper.ExecuteReader(CommandType.StoredProcedure, SQL_SELECT_PID, parm)) {
                while (MyReader.Read()) {
                    MyList.Add(new tbPrintContent() {
                        ContentId = MyReader.GetInt32(0),
                        Name = MyReader.GetString(1),
                        ParentId = MyReader.GetInt32(2),
                        Sort = MyReader.GetInt32(3),
                        Display = MyReader.GetBoolean(4)
                    });
                }
            }

            return MyList;
        }

        public IList<tbPrintContent> SelectByParentIdAndDisplay(int ParentId, bool Display) { 
            IList<tbPrintContent> MyList = new List<tbPrintContent>();
            IDbDataParameter[] parm = GetSelectPIdAndDisplayParam();
            parm[0].Value = ParentId;
            parm[1].Value = Display;

            using (IDataReader MyReader = DBHelper.ExecuteReader(CommandType.StoredProcedure, SQL_SELECT_PIDANDDIS, parm)) {
                while (MyReader.Read()) {
                    MyList.Add(new tbPrintContent() {
                        ContentId = MyReader.GetInt32(0),
                        Name = MyReader.GetString(1),
                        ParentId = MyReader.GetInt32(2),
                        Sort = MyReader.GetInt32(3),
                        Display = MyReader.GetBoolean(4)
                    });
                }
            }

            return MyList;
        }

        public IList<tbPrintContent> Select(string IdList) { 
            IList<tbPrintContent> MyList = new List<tbPrintContent>();
            IDbDataParameter[] parm = GetSelectIdListParam();
            parm[0].Value = IdList;

            using (IDataReader MyReader = DBHelper.ExecuteReader(CommandType.StoredProcedure, SQL_SELECT_IDLIST, parm)) {
                while (MyReader.Read()) {
                    MyList.Add(new tbPrintContent() {
                        ContentId = MyReader.GetInt32(0),
                        Name = MyReader.GetString(1),
                        ParentId = MyReader.GetInt32(2),
                        Sort = MyReader.GetInt32(3),
                        Display = MyReader.GetBoolean(4)
                    });
                }
            }

            return MyList;
        }

        private static IDbDataParameter[] GetSelectDisplayParam() {
            IDbDataParameter[] parm = DBHelper.GetCacheParameters(SQL_SELECT_DISPLAY);

            if (parm == null) {
                parm = new SqlParameter[] {
                     new SqlParameter(PARM_DISPLAY,SqlDbType.Bit)
                };

                DBHelper.CacheParameters(SQL_SELECT_DISPLAY, parm);
            }

            return parm;
        }

        private static IDbDataParameter[] GetSelectParentIdParam() {
            IDbDataParameter[] parm = DBHelper.GetCacheParameters(SQL_SELECT_PID);

            if (parm == null) {
                parm = new SqlParameter[] {
                     new SqlParameter(PARM_PID,SqlDbType.Int)
                };

                DBHelper.CacheParameters(SQL_SELECT_PID, parm);
            }

            return parm;
        }

        private static IDbDataParameter[] GetSelectPIdAndDisplayParam() {
            IDbDataParameter[] parm = DBHelper.GetCacheParameters(SQL_SELECT_PIDANDDIS);

            if (parm == null) {
                parm = new SqlParameter[] {
                     new SqlParameter(PARM_PID,SqlDbType.Int),
                     new SqlParameter(PARM_DISPLAY,SqlDbType.Bit)
                };

                DBHelper.CacheParameters(SQL_SELECT_PIDANDDIS, parm);
            }

            return parm;
        }

        private static IDbDataParameter[] GetSelectIdListParam() {
            IDbDataParameter[] parm = DBHelper.GetCacheParameters(SQL_SELECT_IDLIST);

            if (parm == null) {
                parm = new SqlParameter[] {
                     new SqlParameter(PARM_IDLIST,SqlDbType.VarChar,5000)
                };

                DBHelper.CacheParameters(SQL_SELECT_IDLIST, parm);
            }

            return parm;
        }
    }
}
