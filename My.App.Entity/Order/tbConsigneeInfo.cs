using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace My.App.Entity.Order
{
    /// <summary>
    /// 收货人信息表
    /// </summary>
    [Serializable]
    public class tbConsigneeInfo
    {
        /// <summary>
        /// 收货人编号
        /// </summary>
        public int ConsigneeId { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrdersNumber { get; set; }

        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 收货人电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 收货人手机
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 收货人邮编
        /// </summary>
        public string PostCode { get; set; }

        /// <summary>
        /// 收货人所在省
        /// </summary>
        public string Provice { get; set; }

        /// <summary>
        /// 收货人所在市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 收货人所在区
        /// </summary>
        public string District { get; set; }

        /// <summary>
        /// 区域编号
        /// </summary>
        public int ZoneId { get; set; }

        /// <summary>
        /// 省份编号
        /// </summary>
        public int ProviceId { get; set; }

        /// <summary>
        /// 市编号
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// 区编号
        /// </summary>
        public int DistrictId { get; set; }

        /// <summary>
        /// 收货人地址
        /// </summary>
        public string ConsigneeAddress { get; set; }

        /// <summary>
        /// 录入日期
        /// </summary>
        public DateTime InputDate { get; set; }
    }
}
