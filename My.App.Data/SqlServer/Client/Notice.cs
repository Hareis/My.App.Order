using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Interface.Client;
using My.App.Entity.Client;
using System.Data;
using My.App.DBUtility;

namespace My.App.Data.SqlServer.Client
{
    public class Notice : INotice
    {
        private const string SQL_SELECT = "SelectNotice";                                                       //查询通知信息

        public tbNotice Select() {
            tbNotice n = null;

            using (IDataReader MyReader = DBHelper.ExecuteReader(CommandType.StoredProcedure, SQL_SELECT)) {
                if (MyReader.Read()) {
                    n = new tbNotice() {
                        NoticeId = MyReader.GetInt32(0),
                        Content = MyReader.GetString(1),
                        StartDate = MyReader.GetDateTime(2),
                        EndDate = MyReader.GetDateTime(3)
                    };
                }
            }

            return n == null ? new tbNotice() : n;
        }
    }
}
