using System;
using Newtonsoft.Json;
namespace Friend.Newtonsoft.Json
{
    /// <summary>
    /// 
    /// </summary>
    public class StringNullConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var str = (string)value;
            if (string.IsNullOrEmpty(str))
            {
                writer.WriteValue("");
            }
            else
            {
                writer.WriteValue((string)value);
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return reader.Value;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }
    }
}