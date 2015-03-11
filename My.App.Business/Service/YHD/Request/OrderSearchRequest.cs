using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Business.Service.YHD.Response;
using My.App.Business.Service.YHD.Util;

namespace My.App.Business.Service.YHD.Request
{
    internal class OrderSearchRequest : IRequest<OrderSearchResponse> {
        /// <summary>
        /// 订单状态（逗号分隔）:
        /// <para>ORDER_WAIT_PAY：已下单（货款未全收）</para>
        /// <para>ORDER_PAYED：已下单（货款已收）</para>
        /// <para>ORDER_WAIT_SEND：可以发货（已送仓库）</para>
        /// <para>ORDER_ON_SENDING：已出库（货在途）,已发送物流</para>
        /// <para>ORDER_RECEIVED：货物用户已收到</para>
        /// <para>ORDER_FINISH：订单完成</para>
        /// <para>ORDER_GRT：退换货（用户要求退货,用户要求换货,退货完成,换货完成）</para>
        /// <para>ORDER_CANCEL：订单取消</para>
        /// <para>必填项</para>
        /// </summary>
        public string orderStatusList { get; set; }

        /// <summary>
        /// 日期类型(默认1)
        /// <para>1：订单生成日期</para>
        /// <para>2：订单付款日期</para>
        /// <para>3：订单发货日期</para>
        /// <para>4：订单收货日期</para>
        /// <para>5：订单更新日期</para>
        /// <para>选填项</para>
        /// </summary>
        public int? dateType { get; set; }

        /// <summary>
        /// 查询开始时间
        /// <para>格式(yyyy-MM-dd HH:mm:ss)</para>
        /// <para>必填项</para>
        /// </summary>
        public string startTime { get; set; }

        /// <summary>
        /// 查询结束时间(时间差为15天)
        /// <para>格式(yyyy-MM-dd HH:mm:ss)</para>
        /// <para>必填项</para>
        /// </summary>
        public string endTime { get; set; }

        /// <summary>
        /// 当前页数
        /// <para>默认1</para>
        /// <para>选填项</para>
        /// </summary>
        public long? curPage { get; set; }

        /// <summary>
        /// 每页显示记录数，默认50，最大100
        /// <para>选填项</para>
        /// </summary>
        public long? pageRows { get; set; }

        public string GetApiUrl() {
            return "yhd.orders.get";
        }

        public IDictionary<string, string> GetParameters() {
            ApiDictionary parameters = new ApiDictionary();
            parameters.Add("orderStatusList", this.orderStatusList);
            parameters.Add("dateType", this.dateType);
            parameters.Add("startTime", this.startTime);
            parameters.Add("endTime", this.endTime);
            parameters.Add("curPage", this.curPage);
            parameters.Add("pageRows", this.pageRows);
            return parameters;
        }

        public void Validate() {
            RequestValidator.ValidateRequired("orderStatusList", this.orderStatusList);
            RequestValidator.ValidateRequired("startTime", this.startTime);
            RequestValidator.ValidateRequired("endTime", this.endTime);
        }
    }
}
