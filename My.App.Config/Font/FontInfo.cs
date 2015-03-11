using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace My.App.Config.Font
{
    /// <summary>
    /// 字体信息类
    /// </summary>
    [Serializable]
    public class FontInfo
    {
        /// <summary>
        /// 字体编号
        /// </summary>
        public int FontId { get; set; }

        /// <summary>
        /// 字体名称
        /// </summary>
        public string FontName { get; set; }
    }
}
