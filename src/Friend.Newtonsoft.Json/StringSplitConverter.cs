using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Friend.Newtonsoft.Json
{
    /// <summary>
    /// 将逗号分隔符字符串转为 数组
    /// </summary>
    public class StringSplitConverter : JsonConverter
    {
        private const char DefaultSeperator = ',';
        protected char Separator;
        protected StringSplitOption Option;
        /// <summary>
        /// 构造函数
        /// </summary>
        public StringSplitConverter() : this(DefaultSeperator)
        {

        }
        public StringSplitConverter(StringSplitOption option,char separator = DefaultSeperator)
        {
            Separator = separator;
            Option = option;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="separator">分隔符</param>
        /// <param name="option">拆分选项</param>
        public StringSplitConverter(char separator, StringSplitOption option = StringSplitOption.StringArray)
        {
            Separator = separator;
            Option = option;
        }
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            var str = value as string;
            if (string.IsNullOrWhiteSpace(str))
            {
                writer.WriteRawValue("[]");
            }
            else
            {
                switch (Option)
                {
                    case StringSplitOption.StringArray:
                        writer.WriteRawValue(JsonConvert.SerializeObject(str.Split(Separator)));
                        break;
                    case StringSplitOption.DecimalArray:
                        writer.WriteRawValue(JsonConvert.SerializeObject(str.Trim(Separator).Split(Separator).Select(decimal.Parse)));
                        break;
                    case StringSplitOption.ByteArray:
                        writer.WriteRawValue(JsonConvert.SerializeObject(str.Trim(Separator).Split(Separator).Select(byte.Parse)));
                        break;
                    case StringSplitOption.IntArray:
                        writer.WriteRawValue(JsonConvert.SerializeObject(str.Trim(Separator).Split(Separator).Select(int.Parse)));
                        break;
                    case StringSplitOption.LongArray:
                        writer.WriteRawValue(JsonConvert.SerializeObject(str.Trim(Separator).Split(Separator).Select(long.Parse)));
                        break;
                    case StringSplitOption.DoubleArray:
                        writer.WriteRawValue(JsonConvert.SerializeObject(str.Trim(Separator).Split(Separator).Select(double.Parse)));
                        break;

                }

            }
        }



        public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            var sb = new StringBuilder();
            switch (Option)
            {
                case StringSplitOption.StringArray:
                    ReadString(reader, sb);
                    break;
                case StringSplitOption.ByteArray:
                    ReadByte(reader, sb);
                    break;
                case StringSplitOption.IntArray:
                    ReadInt(reader, sb);
                    break;
                case StringSplitOption.LongArray:
                    ReadLong(reader, sb);
                    break;
                case StringSplitOption.DecimalArray:
                    ReadDecimal(reader, sb);
                    break;
                case StringSplitOption.DoubleArray:
                    ReadDouble(reader, sb);
                    break;
                default:
                    break;
            }
           
            return sb.ToString().Trim(Separator);
        }

        private void ReadString(JsonReader reader, StringBuilder sb)
        {
            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.StartArray)
                {
                    continue;
                }
                if (reader.TokenType == JsonToken.EndArray)
                {
                    break;
                }
                sb.Append(reader.Value?.ToString() + Separator);
            }
        }
        private void ReadDecimal(JsonReader reader, StringBuilder sb)
        {
            decimal? s;
            while ((s = reader.ReadAsDecimal()) != null)
            {
                sb.Append(s + Separator);
            }
        }
        private void ReadLong(JsonReader reader, StringBuilder sb)
        {
            decimal? s;
            while ((s = reader.ReadAsDecimal()) != null)
            {
                sb.Append((long)s + Separator);
            }
        }
        private void ReadInt(JsonReader reader, StringBuilder sb)
        {
            int? s;
            while ((s = reader.ReadAsInt32()) != null)
            {
                sb.Append(s + Separator);
            }
        }
        private void ReadByte(JsonReader reader, StringBuilder sb)
        {
            int? s;
            unchecked
            {
                while ((s = reader.ReadAsInt32()) != null)
                {
                    sb.Append((byte)s + Separator);
                }
            }           
        }
        private void ReadDouble(JsonReader reader, StringBuilder sb)
        {
            double? s;
            while ((s = reader.ReadAsDouble()) != null)
            {
                sb.Append(s + Separator);
            }
        }
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }
    }
}