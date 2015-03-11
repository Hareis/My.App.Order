using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Entity.Shop;
using My.App.Interface.Shop;
using System.Data;
using My.App.DBUtility;
using System.Data.SqlClient;

namespace My.App.Data.SqlServer.Shop
{
    public class SenderInfo : ISenderInfo
    {
        private const string SQL_SELECT_ID = "SelectSenderInfoBySId";                                                   //根据店铺编号查询寄件人信息
        private const string SQL_SELECT_UID = "SelectSenderInfoByUserId";                                               //根据用户编号查询寄件人信息
        private const string SQL_UPDATE = "UpdateSenderInfo";                                                           //修改寄件人信息

        private const string PARM_ID = "@SenderId";                                                                     //寄件人编号
        private const string PARM_SID = "@ShopId";                                                                      //店铺编号
        private const string PARM_SNAME = "@ShopName";                                                                  //店铺名称
        private const string PARM_NAME = "@SenderName";                                                                 //寄件人名称
        private const string PARM_TEL = "@SenderTel";                                                                   //寄件人电话
        private const string PARM_MOBILE = "@SenderMobile";                                                             //寄件人手机
        private const string PARM_ADD = "@SenderAdd";                                                                   //寄件人地址
        private const string PARM_CNAME = "@CompanyName";                                                               //公司名称
        private const string PARM_PROVINCE = "@Province";                                                               //省
        private const string PARM_CITY = "@City";                                                                       //市
        private const string PARM_DIS = "@District";                                                                    //区/县
        private const string PARM_CODE = "@PostCode";                                                                   //邮政编码
        private const string PARM_SCODE = "@ShopCode";                                                                  //商家编码

        private const string PARM_UID = "@UserId";                                                                      //用户编号

        public tbSenderInfo Select(int ShopId) {
            tbSenderInfo Sender = null;
            IDbDataParameter[] parm = GetSelectBySIdParam();
            parm[0].Value = ShopId;

            using (IDataReader MyReader = DBHelper.ExecuteReader(CommandType.StoredProcedure, SQL_SELECT_ID, parm)) {
                if (MyReader.Read()) {
                    Sender = new tbSenderInfo() {
                        SenderId = MyReader.GetInt32(0),
                        ShopId = MyReader.GetInt32(1),
                        ShopName = MyReader.GetString(2),
                        SenderName = MyReader.GetString(3),
                        SenderTel = MyReader.GetString(4),
                        SenderMobile = MyReader.GetString(5),
                        SenderAdd = MyReader.GetString(6),
                        CompanyName = MyReader.GetString(7),
                        Province = MyReader.GetString(8),
                        City = MyReader.GetString(9),
                        District = MyReader.GetString(10),
                        PostCode = MyReader.GetString(11),
                        ShopCode = MyReader.GetString(12)
                    };
                }
            }

            return Sender == null ? new tbSenderInfo() : Sender;
        }

        public tbSenderInfo SelectByUserId(int UserId) { 
            tbSenderInfo Sender = null;
            IDbDataParameter[] parm = GetSelectByUIdParam();
            parm[0].Value = UserId;

            using (IDataReader MyReader = DBHelper.ExecuteReader(CommandType.StoredProcedure, SQL_SELECT_UID, parm)) {
                if (MyReader.Read()) {
                    Sender = new tbSenderInfo() {
                        SenderId = MyReader.GetInt32(0),
                        ShopId = MyReader.GetInt32(1),
                        ShopName = MyReader.GetString(2),
                        SenderName = MyReader.GetString(3),
                        SenderTel = MyReader.GetString(4),
                        SenderMobile = MyReader.GetString(5),
                        SenderAdd = MyReader.GetString(6),
                        CompanyName = MyReader.GetString(7),
                        Province = MyReader.GetString(8),
                        City = MyReader.GetString(9),
                        District = MyReader.GetString(10),
                        PostCode = MyReader.GetString(11),
                        ShopCode = MyReader.GetString(12)
                    };
                }
            }

            return Sender == null ? new tbSenderInfo() : Sender;
        }

        public bool Update(tbSenderInfo Sender) {
            bool IsOk = false;

            IDbDataParameter[] parm = GetUpdateParam();
            parm[0].Value = Sender.SenderId;
            parm[1].Value = Sender.ShopId;
            parm[2].Value = Sender.ShopName;
            parm[3].Value = Sender.SenderName;
            parm[4].Value = Sender.SenderTel;
            parm[5].Value = Sender.SenderMobile;
            parm[6].Value = Sender.SenderAdd;
            parm[7].Value = Sender.CompanyName;
            parm[8].Value = Sender.Province;
            parm[9].Value = Sender.City;
            parm[10].Value = Sender.District;
            parm[11].Value = Sender.PostCode;
            parm[12].Value = Sender.ShopCode;

            try {
                DBHelper.ExecuteNonQuery(CommandType.StoredProcedure, SQL_UPDATE, parm);
                IsOk = true;
            } catch { }

            return IsOk;
        }

        private static IDbDataParameter[] GetSelectBySIdParam() {
            IDbDataParameter[] parm = DBHelper.GetCacheParameters(SQL_SELECT_ID);

            if (parm == null) {
                parm = new SqlParameter[] {
                     new SqlParameter(PARM_SID,SqlDbType.Int)
                };

                DBHelper.CacheParameters(SQL_SELECT_ID, parm);
            }

            return parm;
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

        private static IDbDataParameter[] GetUpdateParam() {
            IDbDataParameter[] parm = DBHelper.GetCacheParameters(SQL_UPDATE);

            if (parm == null) {
                parm = new SqlParameter[] {
                     new SqlParameter(PARM_ID,SqlDbType.Int),
                     new SqlParameter(PARM_SID,SqlDbType.Int),
                     new SqlParameter(PARM_SNAME,SqlDbType.VarChar,50),
                     new SqlParameter(PARM_NAME,SqlDbType.VarChar,50),
                     new SqlParameter(PARM_TEL,SqlDbType.VarChar,20),
                     new SqlParameter(PARM_MOBILE,SqlDbType.VarChar,20),
                     new SqlParameter(PARM_ADD,SqlDbType.VarChar,500),
                     new SqlParameter(PARM_CNAME,SqlDbType.VarChar,100),
                     new SqlParameter(PARM_PROVINCE,SqlDbType.VarChar,50),
                     new SqlParameter(PARM_CITY,SqlDbType.VarChar,50),
                     new SqlParameter(PARM_DIS,SqlDbType.VarChar,50),
                     new SqlParameter(PARM_CODE,SqlDbType.VarChar,50),
                     new SqlParameter(PARM_SCODE,SqlDbType.VarChar,50)
                };

                DBHelper.CacheParameters(SQL_UPDATE, parm);
            }

            return parm;
        }
    }
}