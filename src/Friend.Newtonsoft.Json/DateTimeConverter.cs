using Newtonsoft.Json.Converters;

namespace Friend.Newtonsoft.Json
{
    /// <summary>
    /// 时间日期序列化转换器
    /// </summary>
    public class DateTimeConverter : IsoDateTimeConverter
    {
        /// <summary>
        /// 构造函数，设置时间日期格式化字符串为 yyyy-MM-dd HH:mm:ss
        /// </summary>
        public DateTimeConverter()
        {
            DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        }
    }

    
}
