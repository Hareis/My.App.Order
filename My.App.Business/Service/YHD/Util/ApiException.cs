using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace My.App.Business.Service.YHD.Util
{
    internal class ApiException : Exception
    {
        private string errorCode;
        private string errorMsg;
        public ApiException() : base() { }

        public ApiException(string message) : base(message) { }

        protected ApiException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public ApiException(string message, Exception innerException) : base(message, innerException) { }

        public ApiException(string errorCode, string errorMsg) : base(errorCode + ":" + errorMsg) {
            this.errorCode = errorCode;
            this.errorMsg = errorMsg;
        }

        /// <summary>
        /// 错误代码
        /// </summary>
        public string ErrorCode {
            get { return this.errorCode; }
        }

        /// <summary>
        /// 错误描述
        /// </summary>
        public string ErrorMsg {
            get { return this.errorMsg; }
        }
    }
}
