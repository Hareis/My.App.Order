using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.DALFactory;
using My.App.Interface.Soft;
using My.App.Entity.Soft;

namespace My.App.Business.Soft
{
    public class SoftInfo
    {
        private static readonly ISoftInfo SoftInfoDal = DataAccess.Create("Soft.SoftInfo") as ISoftInfo;

        /// <summary>
        /// 查询软件信息列表
        /// </summary>
        /// <returns></returns>
        public static IList<tbSoftInfo> Select() {
            return SoftInfoDal.Select();
        }

        /// <summary>
        /// 修改软件投票信息
        /// </summary>
        /// <param name="SoftId">软件编号</param>
        /// <param name="UserId">用户编号</param>
        /// <param name="state">投票类型</param>
        /// <returns></returns>
        public static bool Update(int SoftId, int UserId, int state) {
            return SoftInfoDal.Update(SoftId, UserId, state);
        }
    }
}
