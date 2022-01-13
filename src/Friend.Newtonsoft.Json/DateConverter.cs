using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Converters;

namespace Friend.Newtonsoft.Json
{
    /// <summary>
    /// 日期序列化转换器
    /// </summary>
    public class DateConverter : IsoDateTimeConverter
    {
        /// <summary>
        /// 构造函数，设置时间日期格式化字符串为 yyyy-MM-dd
        /// </summary>
        public DateConverter()
        {
            DateTimeFormat = "yyyy-MM-dd";
        }
    }
}
