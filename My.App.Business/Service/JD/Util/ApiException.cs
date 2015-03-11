using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace My.App.Business.Service.JD.Util
{
    internal class ApiException : Exception
    {
        private string errorCode;
        private string errorZhMsg;
        private string errorEnMsg;

        public ApiException() : base() { }

        public ApiException(string message) : base(message) { }

        protected ApiException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public ApiException(string message, Exception innerException) : base(message, innerException) { }

        public ApiException(string errorCode, string errorZhMsg, string errorEnMsg) : base(errorCode + ":" + errorZhMsg + ":" + errorEnMsg) {
            this.errorCode = errorCode;
            this.errorZhMsg = errorZhMsg;
            this.errorEnMsg = errorEnMsg;
        }

        /// <summary>
        /// 错误代码
        /// </summary>
        public string ErrorCode {
            get { return this.errorCode; }
        }

        /// <summary>
        /// 错误描述(中文)
        /// </summary>
        public string ErrorZhMsg {
            get { return this.errorZhMsg; }
        }

        /// <summary>
        /// 错误描述(英文)
        /// </summary>
        public string ErrorEnMsg {
            get { return this.errorEnMsg; }
        }
    }
}
