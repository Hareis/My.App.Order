using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Entity.Shop;
using My.App.Interface.Shop;
using My.App.DALFactory;

namespace My.App.Business.Shop
{
    public class ShopInfo
    {
        private static readonly IShopInfo ShopInfoDal = DataAccess.Create("Shop.ShopInfo") as IShopInfo;

        /// <summary>
        /// 根据用户编号查询店铺信息列表
        /// </summary>
        /// <param name="UserId">用户编号</param>
        /// <returns></returns>
        public static IList<tbShopInfo> Select(int UserId) {
            return ShopInfoDal.Select(UserId);
        }

        /// <summary>
        /// 添加店铺信息
        /// </summary>
        /// <param name="ShopInfo">店铺编号</param>
        /// <returns></returns>
        public static int Insert(tbShopInfo ShopInfo) {
            return ShopInfoDal.Insert(ShopInfo);
        }
    }
}
