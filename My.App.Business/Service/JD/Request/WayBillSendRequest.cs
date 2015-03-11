using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Business.Service.JD.Response;
using My.App.Business.Service.JD.Util;

namespace My.App.Business.Service.JD.Request
{
    internal class WayBillSendRequest : IRequest<WayBillSendResponse>
    {
        /// <summary>
        /// <para>运单号</para>
        /// <para>必填项</para>
        /// </summary>
        public string deliveryId { get; set; }

        /// <summary>
        /// <para>销售平台编码</para>
        /// <para>0010001 京东</para>
        /// <para>0010002 天猫</para>
        /// <para>0010003 苏宁</para>
        /// <para>0010004 亚马逊中国</para>
        /// <para>0020001 ChinaSkin</para>
        /// <para>0030001 其他小型销售平台</para>
        /// <para>必填项</para>
        /// </summary>
        public string salePlat { get; set; }

        /// <summary>
        /// <para>商家店铺编码</para>
        /// <para>必填项</para>
        /// </summary>
        public string customerCode { get; set; }

        /// <summary>
        /// <para>商家订单号</para>
        /// <para>必填项</para>
        /// </summary>
        public string orderId { get; set; }

        /// <summary>
        /// <para>京东订单号</para>
        /// <para>必填项</para>
        /// </summary>
        public string thrOrderId { get; set; }

        /// <summary>
        /// <para>是否客户打印运单</para>
        /// <para>是：1</para>
        /// <para>否：0</para>
        /// <para>不填或者超出范围，默认是1</para>
        /// <para>选填项</para>
        /// </summary>
        public string selfPrintWayBill { get; set; }

        /// <summary>
        /// <para>取件方式</para>
        /// <para>上门收货：1</para>
        /// <para>自送：2</para>
        /// <para>不填或者超出范围，默认是1</para>
        /// <para>选填项</para>
        /// </summary>
        public string pickMethod { get; set; }

        /// <summary>
        /// <para>包装要求</para>
        /// <para>不需包装：1</para>
        /// <para>简单包装：2</para>
        /// <para>特殊包装：3</para>
        /// <para>不填或者超出范围，默认是1</para>
        /// <para>选填项</para>
        /// </summary>
        public string packageRequired { get; set; }

        /// <summary>
        /// <para>寄件人姓名</para>
        /// <para>必填项</para>
        /// </summary>
        public string senderName { get; set; }

        /// <summary>
        /// <para>寄件人地址</para>
        /// <para>必填项</para>
        /// </summary>
        public string senderAddress { get; set; }

        /// <summary>
        /// <para>寄件人电话</para>
        /// <para>选填项</para>
        /// </summary>
        public string senderTel { get; set; }

        /// <summary>
        /// <para>寄件人手机</para>
        /// <para>选填项</para>
        /// <para>寄件人电话、手机至少有一个不为空</para>
        public string senderMobile { get; set; }

        /// <summary>
        /// <para>寄件人邮编</para>
        /// <para>选填项</para>
        public string senderPostcode { get; set; }

        /// <summary>
        /// <para>收件人姓名</para>
        /// <para>必填项</para>
        /// </summary>
        public string receiveName { get; set; }

        /// <summary>
        /// <para>收件人地址</para>
        /// <para>必填项</para>
        /// </summary>
        public string receiveAddress { get; set; }

        /// <summary>
        /// <para>收件人省</para>
        /// <para>选填项</para>
        /// </summary>
        public string province { get; set; }

        /// <summary>
        /// <para>收件人市</para>
        /// <para>选填项</para>
        /// </summary>
        public string city { get; set; }

        /// <summary>
        /// <para>收件人县 </para>
        /// <para>选填项</para>
        /// </summary>
        public string county { get; set; }

        /// <summary>
        /// <para>收件人镇</para>
        /// <para>选填项</para>
        /// </summary>
        public string town { get; set; }

        /// <summary>
        /// <para>收件人电话</para>
        /// <para>选填项</para>
        /// </summary>
        public string receiveTel { get; set; }

        /// <summary>
        /// <para>收件人手机</para>
        /// <para>选填项</para>
        /// <para>收件人手机号(收件人电话、手机至少有一个不为空)</para>
        /// </summary>
        public string receiveMobile { get; set; }

        /// <summary>
        /// <para>收件人邮编</para>
        /// <para>选填项</para>
        /// </summary>
        public string postcode { get; set; }

        /// <summary>
        /// <para>包裹数量</para>
        /// <para>必填项</para>
        /// </summary>
        public string packageCount { get; set; }

        /// <summary>
        /// <para>重量(单位：kg，保留小数点后两位，默认为0 )</para>
        /// <para>必填项</para>
        /// </summary>
        public string weight { get; set; }

        /// <summary>
        /// <para>包裹长(单位：cm,保留小数点后两位)</para>
        /// <para>选填项</para>
        /// </summary>
        public string vloumLong { get; set; }

        /// <summary>
        /// <para>包裹宽(单位：cm，保留小数点后两位)</para>
        /// <para>选填项</para>
        /// </summary>
        public string vloumWidth { get; set; }

        /// <summary>
        /// <para>包裹高(单位：cm，保留小数点后两位)</para>
        /// <para>选填项</para>
        /// </summary>
        public string vloumHeight { get; set; }

        /// <summary>
        /// <para>体积(单位：CM3，保留小数点后两位，默认可传为0 )</para>
        /// <para>必填项</para>
        /// </summary>
        public string vloumn { get; set; }

        /// <summary>
        /// <para>商品描述</para>
        /// <para>选填项</para>
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// <para>是否代收货款</para>
        /// <para>1为代收货款</para>
        /// <para>0为非代收货款</para>
        /// <para>必填项</para>
        /// </summary>
        public string collectionValue { get; set; }

        /// <summary>
        /// <para>代收货款金额</para>
        /// <para>【是否代收货款】为1，则必填</para>
        /// <para>【是否代收货款】为0，则为空</para>
        /// <para>选填项</para>
        /// </summary>
        public string collectionMoney { get; set; }

        /// <summary>
        /// <para>是否保价</para>
        /// <para>是：1</para>
        /// <para>否：0</para>
        /// <para>不填或者超出范围，默认是0</para>
        /// <para>选填项</para>
        /// </summary>
        public string guaranteeValue { get; set; }

        /// <summary>
        /// <para>保价金额</para>
        /// <para>如果有保价，则保价金额(保留小数点后两位) 为必填，后台有校验。</para>
        /// <para>选填项</para>
        /// </summary>
        public string guaranteeValueAmount { get; set; }

        /// <summary>
        /// <para>是否签单返还</para>
        /// <para>是：1</para>
        /// <para>否：0</para>
        /// <para>不填或者超出范围，默认是0</para>
        /// <para>选填项</para>
        /// </summary>
        public string signReturn { get; set; }

        /// <summary>
        /// <para>运单时效</para>
        /// <para>普通：1</para>
        /// <para>工作日：2</para>
        /// <para>非工作日：3</para>
        /// <para>晚间：4</para>
        /// <para>不填或者超出范围，默认是1</para>
        /// <para>选填项</para>
        /// </summary>
        public string aging { get; set; }

        /// <summary>
        /// <para>运输业务类型</para>
        /// <para>陆运：1</para>
        /// <para>航空：2</para>
        /// <para>不填或者超出范围，默认是1</para>
        /// <para>选填项</para>
        /// </summary>
        public string transType { get; set; }

        public string GetApiUrl() {
            return "jingdong.etms.waybill.send";
        }

        public IDictionary<string, string> GetParameters() {
            ApiDictionary parameters = new ApiDictionary();
            parameters.Add("deliveryId", this.deliveryId);
            parameters.Add("salePlat", this.salePlat);
            parameters.Add("customerCode", this.customerCode);
            parameters.Add("orderId", this.orderId);
            parameters.Add("thrOrderId", this.thrOrderId);
            parameters.Add("senderName", this.senderName);
            parameters.Add("selfPrintWayBill", this.selfPrintWayBill);
            parameters.Add("pickMethod", this.pickMethod);
            parameters.Add("packageRequired", this.packageRequired);
            parameters.Add("senderAddress", this.senderAddress);
            parameters.Add("senderTel", this.senderTel);
            parameters.Add("senderMobile", this.senderMobile);
            parameters.Add("senderPostcode", this.senderPostcode);
            parameters.Add("receiveName", this.receiveName);
            parameters.Add("receiveAddress", this.receiveAddress);
            parameters.Add("province", this.province);
            parameters.Add("city", this.city);
            parameters.Add("county", this.county);
            parameters.Add("town", this.town);
            parameters.Add("receiveTel", this.receiveTel);
            parameters.Add("receiveMobile", this.receiveMobile);
            parameters.Add("postcode", this.postcode);
            parameters.Add("packageCount", this.packageCount);
            parameters.Add("weight", this.weight);
            parameters.Add("vloumLong", this.vloumLong);
            parameters.Add("vloumWidth", this.vloumWidth);
            parameters.Add("vloumHeight", this.vloumHeight);
            parameters.Add("vloumn", this.vloumn);
            parameters.Add("description", this.description);
            parameters.Add("collectionValue", this.collectionValue);
            parameters.Add("collectionMoney", this.collectionMoney);
            parameters.Add("guaranteeValue", this.guaranteeValue);
            parameters.Add("guaranteeValueAmount", this.guaranteeValueAmount);
            parameters.Add("signReturn", this.signReturn);
            parameters.Add("aging", this.aging);
            parameters.Add("transType", this.transType);
            return parameters;
        }

        public void Validate() {
            RequestValidator.ValidateRequired("deliveryId", this.deliveryId);
            RequestValidator.ValidateRequired("salePlat", this.salePlat);
            RequestValidator.ValidateRequired("customerCode", this.customerCode);
            RequestValidator.ValidateRequired("orderId", this.orderId);
            RequestValidator.ValidateRequired("thrOrderId", this.thrOrderId);
            RequestValidator.ValidateRequired("senderName", this.senderName);
            RequestValidator.ValidateRequired("senderAddress", this.senderAddress);
            RequestValidator.ValidateRequired("receiveName", this.receiveName);
            RequestValidator.ValidateRequired("receiveAddress", this.receiveAddress);
            RequestValidator.ValidateRequired("packageCount", this.packageCount);
            RequestValidator.ValidateRequired("weight", this.weight);
            RequestValidator.ValidateRequired("vloumn", this.vloumn);
            RequestValidator.ValidateRequired("collectionValue", this.collectionValue);
        }
    }
}
