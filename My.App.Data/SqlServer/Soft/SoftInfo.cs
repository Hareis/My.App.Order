using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Interface.Soft;
using My.App.Entity.Soft;
using System.Data;
using My.App.DBUtility;
using System.Data.SqlClient;

namespace My.App.Data.SqlServer.Soft
{
    public class SoftInfo : ISoftInfo
    {
        private const string SQL_SELECT = "SelectSoftInfo";                                                 //查询软件信息列表
        private const string SQL_UPDATE_VOTE = "UpdateSoftInfoVote";                                        //修改软件投票信息

        private const string PARM_ID = "@SoftId";                                                           //软件编号
        private const string PARM_UID = "@UserId";                                                          //用户编号
        private const string PARM_STATE = "@State";                                                         //投票类型

        public IList<tbSoftInfo> Select() {
            IList<tbSoftInfo> MyList = new List<tbSoftInfo>();

            using (IDataReader MyReader = DBHelper.ExecuteReader(CommandType.StoredProcedure, SQL_SELECT)) {
                while (MyReader.Read()) {
                    MyList.Add(new tbSoftInfo() {
                        SoftId = MyReader.GetInt32(0),
                        SoftName = MyReader.GetString(1),
                        SoftImg = MyReader.GetString(2),
                        Detail = MyReader.GetString(3),
                        Price = MyReader.GetDecimal(4),
                        Currency = MyReader.GetString(5),
                        Unit = MyReader.GetString(6),
                        GoodNum = MyReader.GetInt32(7),
                        BadNum = MyReader.GetInt32(8),
                        InputDate = MyReader.GetDateTime(9),
                        Status = new tbSoftStatus() { 
                            StatusId = MyReader.GetInt32(10),
                            StatusName = MyReader.GetString(11)
                        }
                    });
                }
            }

            return MyList;
        }

        public bool Update(int SoftId, int UserId, int state) {
            bool IsOk = false;

            IDbDataParameter[] parm = GetUpdateVoteParam();
            parm[0].Value = SoftId;
            parm[1].Value = UserId;
            parm[2].Value = state;

            try {
                DBHelper.ExecuteNonQuery(CommandType.StoredProcedure, SQL_UPDATE_VOTE, parm);
                IsOk = true;
            }
            catch { }

            return IsOk;
        }

        private static IDbDataParameter[] GetUpdateVoteParam() {
            IDbDataParameter[] parm = DBHelper.GetCacheParameters(SQL_UPDATE_VOTE);

            if (parm == null) {
                parm = new SqlParameter[] {
                     new SqlParameter(PARM_ID,SqlDbType.Int,4),
                     new SqlParameter(PARM_UID,SqlDbType.Int,4),
                     new SqlParameter(PARM_STATE,SqlDbType.Int,4)
                };

                DBHelper.CacheParameters(SQL_UPDATE_VOTE, parm);
            }

            return parm;
        }
    }
}
