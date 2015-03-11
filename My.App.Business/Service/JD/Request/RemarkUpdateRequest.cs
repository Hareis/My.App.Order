using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using My.App.Business.Service.JD.Response;
using My.App.Business.Service.JD.Util;

namespace My.App.Business.Service.JD.Request
{
    internal class RemarkUpdateRequest : IRequest<RemarkUpdateResponse>
    {
        /// <summary>
        /// 订单编号
        /// <para>必填项</para>
        /// </summary>
        public string order_id { get; set; }

        /// <summary>
        /// 订单备注（可为空备注信息）
        /// <para>必填项</para>
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 流水号，不能重复的随机数
        /// <para>选填项</para>
        /// </summary>
        public string trade_no { get; set; }

        /// <summary>
        /// 商家备注提示文字颜色值 
        /// <para> GRAY 0</para>
        /// <para> RED 1</para>
        /// <para> YELLOW 2</para>
        /// <para> GREEN 3</para>
        /// <para> BLUE 4</para>
        /// <para> PURPLE 5</para>
        /// <para>选填项(默认值为0)</para>
        /// </summary>
        public string flag { get; set; }

        public string GetApiUrl() {
            return "360buy.order.vender.remark.update";
        }

        public IDictionary<string, string> GetParameters() {
            ApiDictionary parameters = new ApiDictionary();
            parameters.Add("order_id", this.order_id);
            parameters.Add("remark", this.remark);
            parameters.Add("trade_no", this.trade_no);
            parameters.Add("flag", this.flag);
            return parameters;
        }

        public void Validate() {
            RequestValidator.ValidateRequired("order_id", this.order_id);
        }
    }
}
