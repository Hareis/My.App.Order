using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace My.App.Config.Encrypt
{
    /// <summary>
    /// 加密方式类
    /// </summary>
    [Serializable]
    public class EncryptType
    {
        /// <summary>
        /// 加密方式枚举
        /// </summary>
        public EncryptEnum Type { get; set; }

        /// <summary>
        /// 密钥
        /// </summary>
        public string Key { get; set; }
    }
}
