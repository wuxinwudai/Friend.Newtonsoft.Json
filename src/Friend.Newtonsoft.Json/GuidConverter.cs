
using System;
using Newtonsoft.Json;

namespace Friend.Newtonsoft.Json
{
    /// <summary>
    /// Guid 格式转换器，修改默认输出格式
    /// 如： ebed0537-02f4-47fc-a1f8-544deabf38a2 为 ebed053702f447fca1f8-544deabf38a2
    /// </summary>
    public class GuidConverter : JsonConverter
    {
        public override bool CanRead => true;
        public override bool CanWrite => true;

        public bool DefaultFormart { get; set; }

        public GuidConverter():this(false)
        {

        }
        public GuidConverter(bool defaultFormat)
        {
            this.DefaultFormart = defaultFormat;
        }
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            writer.WriteValue(DefaultFormart ? value.ToString() : ((Guid)value).ToString("N"));
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            var str = (string)reader.Value;
            if (string.IsNullOrWhiteSpace(str))
            {
                return default(Guid);
            }
            return Guid.Parse(str);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Guid) || objectType == typeof(Guid?);
        }
    }
}