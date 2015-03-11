using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace My.App.Business.Service.YHD.Entity
{
    /// <summary>
    /// 订单简要信息
    /// </summary>
    [Serializable]
    public class Order
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        [XmlElement("orderId")]
        public long orderId { get; set; }

        /// <summary>
        /// 订单编码
        /// </summary>
        [XmlElement("orderCode")]
        public string orderCode { get; set; }

        /// <summary>
        /// 订单状态
        /// <para>ORDER_WAIT_PAY：已下单（货款未全收） </para>
        /// <para>ORDER_WAIT_PAY：已下单（货款未全收） </para>
        /// <para>ORDER_WAIT_PAY：已下单（货款未全收） </para>
        /// <para>ORDER_CAN_OUT_OF_WH：可出库 </para>
        /// <para>ORDER_OUT_OF_WH：已出库（货在途） </para>
        /// <para>ORDER_SENDED_TO_LOGITSIC：已发送物流</para>
        /// <para>ORDER_RECEIVED：货物用户已收到</para>
        /// <para>ORDER_FINISH：订单完成</para>
        /// <para>ORDER_CUSTOM_CALLTO_RETURN：用户要求退货</para>
        /// <para>ORDER_CUSTOM_CALLTO_CHANGE：用户要求换货</para>
        /// <para>ORDER_RETURNED：退货完成</para>
        /// <para>ORDER_CHANGE_FINISHED：换货完成</para>
        /// <para>ORDER_CANCEL：订单取消</para>
        /// </summary>
        [XmlElement("orderStatus")]
        public string orderStatus { get; set; }

        /// <summary>
        /// 订购金额(实际支付金额,包括运费)
        /// </summary>
        [XmlElement("orderAmount")]
        public decimal orderAmount { get; set; }

        /// <summary>
        /// 产品总额
        /// </summary>
        [XmlElement("productAmount")]
        public decimal productAmount { get; set; }

        /// <summary>
        /// 订单创建日期
        /// </summary>
        [XmlElement("orderCreateTime")]
        public string orderCreateTime { get; set; }

        /// <summary>
        /// 运费
        /// </summary>
        [XmlElement("orderDeliveryFee")]
        public decimal orderDeliveryFee { get; set; }

        /// <summary>
        /// 发票需要情况: 0 不需要，1旧版普通，2新版普通，3增值税发票
        /// </summary>
        [XmlElement("orderNeedInvoice")]
        public int orderNeedInvoice { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [XmlElement("updateTime")]
        public string updateTime { get; set; }
    }
}
