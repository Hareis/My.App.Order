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
    public class ClientUser : IClientUser
    {
        private const string SQL_SELECT = "SelectClientUser";                                                       //查询会员信息

        private const string PARM_NAME = "@NickName";                                                               //会员昵称
        private const string PARM_AVATAR = "@Avatar";                                                               //会员头像
        private const string PARM_BRITHDAY = "@Birthday";                                                           //会员生日
        private const string PARM_PSW = "@UserPsw";                                                                 //会员密码
        private const string PARM_SEX = "@Sex";                                                                     //会员性别(1:男2:女3：人妖)
        private const string PARM_CREDIT = "@Credit";                                                               //会员信用
        private const string PARM_STATUS = "@Status";                                                               //会员状态(1:正常2:未激活3:删除4:冻结5:监管)
        private const string PARM_PID = "@PfId";                                                                    //来源平台
        private const string PARM_OID = "@OutId";                                                                   //外部会员编号
        private const string PARM_SESSION = "@SessionKey";                                                          //授权码

        public tbClientUser Select(tbClientUser ClientUser, out bool IsInsert) {
            IsInsert = false;
            tbClientUser User = null;
            IDbDataParameter[] parm = GetSelectParam();
            parm[0].Value = ClientUser.NickName;
            parm[1].Value = ClientUser.Avatar;
            parm[2].Value = ClientUser.Birthday;
            parm[3].Value = ClientUser.UserPsw;
            parm[4].Value = ClientUser.Sex;
            parm[5].Value = ClientUser.Credit;
            parm[6].Value = ClientUser.Status;
            parm[7].Value = ClientUser.PfId;
            parm[8].Value = ClientUser.OutId;
            parm[9].Value = ClientUser.SessionKey;

            using (IDataReader MyReader = DBHelper.ExecuteReader(CommandType.StoredProcedure, SQL_SELECT, parm)) {
                if (MyReader.Read()) {
                    User = new tbClientUser() {
                        UserId = MyReader.GetInt32(0),
                        NickName = MyReader.GetString(1),
                        Avatar = MyReader.GetString(2),
                        Birthday = MyReader.GetString(3),
                        UserPsw = MyReader.GetString(4),
                        Sex = MyReader.GetInt32(5),
                        Credit = MyReader.GetString(6),
                        Status = MyReader.GetInt32(7),
                        PfId = MyReader.GetInt32(8),
                        OutId = MyReader.GetInt32(9),
                        SessionKey = MyReader.GetString(10)
                    };
                }
                if (MyReader.NextResult()) {
                    if (MyReader.Read()) {
                        IsInsert = MyReader.GetInt32(0) == 1;
                    }
                }
            }

            return User == null ? new tbClientUser() : User;
        }

        private static IDbDataParameter[] GetSelectParam() {
            IDbDataParameter[] parm = DBHelper.GetCacheParameters(SQL_SELECT);

            if (parm == null) {
                parm = new SqlParameter[] {
                     new SqlParameter(PARM_NAME,SqlDbType.VarChar,50),
                     new SqlParameter(PARM_AVATAR,SqlDbType.VarChar,200),
                     new SqlParameter(PARM_BRITHDAY,SqlDbType.VarChar,50),
                     new SqlParameter(PARM_PSW,SqlDbType.VarChar,50),
                     new SqlParameter(PARM_SEX,SqlDbType.Int),
                     new SqlParameter(PARM_CREDIT,SqlDbType.VarChar,50),
                     new SqlParameter(PARM_STATUS,SqlDbType.Int),
                     new SqlParameter(PARM_PID,SqlDbType.Int),
                     new SqlParameter(PARM_OID,SqlDbType.Int),
                     new SqlParameter(PARM_SESSION,SqlDbType.VarChar,5000)
                };

                DBHelper.CacheParameters(SQL_SELECT, parm);
            }

            return parm;
        }
    }
}