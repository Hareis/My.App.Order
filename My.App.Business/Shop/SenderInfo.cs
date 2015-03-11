using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Interface.Shop;
using My.App.DALFactory;
using My.App.Entity.Shop;

namespace My.App.Business.Shop
{
    public class SenderInfo
    {
        private static readonly ISenderInfo SenderInfoDal = DataAccess.Create("Shop.SenderInfo") as ISenderInfo;

        /// <summary>
        /// 根据店铺编号查询寄件人信息
        /// </summary>
        /// <param name="ShopId">店铺编号</param>
        /// <returns></returns>
        public static tbSenderInfo Select(int ShopId) {
            return SenderInfoDal.Select(ShopId);
        }

        /// <summary>
        /// 根据用户编号查询寄件人信息
        /// </summary>
        /// <param name="UserId">用户编号</param>
        /// <returns></returns>
        public static tbSenderInfo SelectByUserId(int UserId) {
            return SenderInfoDal.SelectByUserId(UserId);
        }

        /// <summary>
        /// 修改寄件人信息
        /// </summary>
        /// <param name="Sender">寄件人信息表</param>
        /// <returns></returns>
        public static bool Update(tbSenderInfo Sender) {
            return SenderInfoDal.Update(Sender);
        }
    }
}
