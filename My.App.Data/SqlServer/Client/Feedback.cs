using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Interface.Client;
using My.App.Entity.Client;
using System.Data;
using My.App.DBUtility;
using System.Data.SqlClient;

namespace My.App.Data.SqlServer.Client
{
    public class Feedback : IFeedback
    {
        private const string SQL_INSERT = "InsertFeedback";                                                     //添加用户反馈信息

        private const string SQL_CONTENT = "@Content";                                                          //反馈内容
        private const string SQL_PHONE = "@Phone";                                                              //联系电话
        private const string SQL_EMAIL = "@Email";                                                              //联系邮箱
        private const string SQL_UID = "@UserId";                                                               //用户编号

        public bool Insert(tbFeedback Feedback) {            
            bool IsOk = false;

            IDbDataParameter[] parm = GetInsertParam();
            parm[0].Value = Feedback.Content;
            parm[1].Value = Feedback.Phone;
            parm[2].Value = Feedback.Email;
            parm[3].Value = Feedback.UserId;

            try {
                DBHelper.ExecuteNonQuery(CommandType.StoredProcedure, SQL_INSERT, parm);
                IsOk = true;
            } catch { }

            return IsOk;
        }

        private static IDbDataParameter[] GetInsertParam() {
            IDbDataParameter[] parm = DBHelper.GetCacheParameters(SQL_INSERT);

            if (parm == null) {
                parm = new SqlParameter[] {
                     new SqlParameter(SQL_CONTENT,SqlDbType.Text),
                     new SqlParameter(SQL_PHONE,SqlDbType.VarChar,50),
                     new SqlParameter(SQL_EMAIL,SqlDbType.VarChar,100),
                     new SqlParameter(SQL_UID,SqlDbType.Int)
                };

                DBHelper.CacheParameters(SQL_INSERT, parm);
            }

            return parm;
        }
    }
}
