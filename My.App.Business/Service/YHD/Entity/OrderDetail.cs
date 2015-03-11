using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace My.App.Business.Service.YHD.Entity
{
    /// <summary>
    /// 订单详情
    /// </summary>
    [Serializable]
    public class OrderDetail
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

        [XmlElement("realAmount")]
        public string realAmount { get; set; }

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
        /// 发票抬头
        /// </summary>
        [XmlElement("invoiceTitle")]
        public string invoiceTitle { get; set; }

        /// <summary>
        /// 收货人姓名
        /// </summary>
        [XmlElement("goodReceiverName")]
        public string goodReceiverName { get; set; }

        /// <summary>
        /// 收货人地址
        /// </summary>
        [XmlElement("goodReceiverAddress")]
        public string goodReceiverAddress { get; set; }

        /// <summary>
        /// 收货人省份
        /// </summary>
        [XmlElement("goodReceiverProvince")]
        public string goodReceiverProvince { get; set; }

        /// <summary>
        /// 收货人城市
        /// </summary>
        [XmlElement("goodReceiverCity")]
        public string goodReceiverCity { get; set; }

        /// <summary>
        /// 收货人地区
        /// </summary>
        [XmlElement("goodReceiverCounty")]
        public string goodReceiverCounty { get; set; }

        /// <summary>
        /// 收货人街道地址
        /// </summary>
        [XmlElement("goodReceiverArea")]
        public string goodReceiverArea { get; set; }

        /// <summary>
        /// 收货人邮编
        /// </summary>
        [XmlElement("goodReceiverPostCode")]
        public string goodReceiverPostCode { get; set; }

        /// <summary>
        /// 收货人电话
        /// </summary>
        [XmlElement("goodReceiverPhone")]
        public string goodReceiverPhone { get; set; }

        /// <summary>
        /// 收货人手机号
        /// </summary>
        [XmlElement("goodReceiverMoblie")]
        public string goodReceiverMoblie { get; set; }

        /// <summary>
        /// 发货时间
        /// </summary>
        [XmlElement("deliveryDate")]
        public string deliveryDate { get; set; }

        /// <summary>
        /// 确认收货日期
        /// </summary>
        [XmlElement("receiveDate")]
        public string receiveDate { get; set; }

        /// <summary>
        /// 买家留言
        /// </summary>
        [XmlElement("deliveryRemark")]
        public string deliveryRemark { get; set; }

        /// <summary>
        /// 配送商ID
        /// </summary>
        [XmlElement("deliverySupplierId")]
        public int deliverySupplierId { get; set; }

        /// <summary>
        /// 卖家备注
        /// </summary>
        [XmlElement("merchantRemark")]
        public string merchantRemark { get; set; }

        /// <summary>
        /// 付款确认时间(实际付款时间)
        /// </summary>
        [XmlElement("orderPaymentConfirmDate")]
        public string orderPaymentConfirmDate { get; set; }

        /// <summary>
        /// 订单支付方式
        /// <para>0:账户支付</para>
        /// <para>1:网上支付</para>
        /// <para>2:货到付款</para>
        /// <para>3:邮局汇款</para>
        /// <para>4:银行转账</para>
        /// <para>5:pos机</para>
        /// <para>6:万里通</para>
        /// <para>7:分期付款</para>
        /// <para>8:合同账期</para>
        /// <para>9:货到转账</para>
        /// <para>10:货到付支票</para>
        /// </summary>
        [XmlElement("payServiceType")]
        public int payServiceType { get; set; }

        /// <summary>
        /// 参加促销活动立减金额
        /// </summary>
        [XmlElement("orderPromotionDiscount")]
        public decimal orderPromotionDiscount { get; set; }

        /// <summary>
        /// 配送商送货编号(运单号)
        /// </summary>
        [XmlElement("merchantExpressNbr")]
        public string merchantExpressNbr { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [XmlElement("updateTime")]
        public string updateTime { get; set; }

        /// <summary>
        /// 获取销售平台
        /// </summary>
        [XmlElement("siteType")]
        public string siteType { get; set; }

        /// <summary>
        /// 商家抵用券支付金额
        /// </summary>
        [XmlElement("orderCouponDiscount")]
        public decimal orderCouponDiscount { get; set; }

        /// <summary>
        /// 1mall平台抵用券支付金额
        /// </summary>
        [XmlElement("orderPlatformDiscount")]
        public decimal orderPlatformDiscount { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [XmlElement("endUserId")]
        public string endUserId { get; set; }

        /// <summary>
        /// 货到付款应收金额
        /// </summary>
        [XmlElement("collectOnDeliveryAmount")]
        public string collectOnDeliveryAmount { get; set; }
    }
}
