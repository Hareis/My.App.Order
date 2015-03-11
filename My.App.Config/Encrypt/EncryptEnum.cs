using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace My.App.Config.Encrypt
{
    /// <summary>
    /// 加密方式枚举
    /// </summary>
    public enum EncryptEnum
    {
        /// <summary>
        /// AES加密（解密）模式
        /// </summary>
        AES,
        /// <summary>
        /// DES加密（解密）模式
        /// </summary>
        DES,
        /// <summary>
        /// MD5加密模式
        /// </summary>
        MD5
    }
}
