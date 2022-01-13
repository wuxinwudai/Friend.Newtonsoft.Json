using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Friend.Newtonsoft.Json
{
    /// <summary>
    /// Uri 地址转换器，地址前加 host
    /// </summary>
    public class UriConverter : JsonConverter
    {
        /// <summary>
        /// 全局默认配置的 Host，修改后
        /// </summary>
        public static string DefaultHostName;
        private string _host;
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public UriConverter(): this(string.Empty)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="host">主机名</param>
        public UriConverter(string host)
        {
            _host = host;
        }
        public override bool CanRead => true;
        public override bool CanWrite => true;
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            var str = (string)value;
            if (String.IsNullOrWhiteSpace(str))
            {
                writer.WriteValue(str);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(_host))
                {
                    _host = DefaultHostName;
                }
                if (!_host.StartsWith("http") && !_host.StartsWith("//"))
                {
                    _host = "http://" + _host;
                }
                if ( str.StartsWith("http") || str.StartsWith("//"))
                {
                    writer.WriteValue(str);
                }
                else
                {
                    writer.WriteValue(_host + (str.StartsWith("/") ? str : ("/" + str)));
                }                
            }

        }

        public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            return reader.Value;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }
    }
}