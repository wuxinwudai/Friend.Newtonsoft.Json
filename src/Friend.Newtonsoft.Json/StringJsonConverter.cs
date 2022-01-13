using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;


namespace Friend.Newtonsoft.Json
{
    /// <summary>
    /// String 属性转为 JSON 对象
    /// </summary>
    public class StringJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var str = (string)value;
            if (str == null)
            {
                writer.WriteRawValue("{}");
            }
            else
            {
                writer.WriteRawValue(str);
            }
        }


        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            object root = null;
            Dictionary<string, object> thisDict = new Dictionary<string, object>();
            List<object> thisArr = new List<object>();
            var dictStack = new Stack<object>();
            var arrStack = new Stack<object>();
            string name = null;//当前键名
            var currentType = reader.TokenType;
            //var preType = JsonToken.Null;
            var arrStart = false;
            if (reader.TokenType == JsonToken.StartObject)
            {
                root = thisDict;
                dictStack.Push(thisDict);
            }
            else if (reader.TokenType == JsonToken.StartArray)
            {
                root = thisArr;
                arrStart = true;
            }
            var flag = true;
            while (flag && reader.Read())
            {
                switch (reader.TokenType)
                {
                    case JsonToken.PropertyName:
                        name = reader.Value?.ToString();//先读取键值
                        reader.Read();
                        switch (reader.TokenType)
                        {
                            case JsonToken.StartObject:
                                currentType = JsonToken.StartObject;
                                thisDict[name] = new Dictionary<string, object>();
                                //if (preType == JsonToken.StartArray)
                                //{
                                //    arrStack.Push(thisDict);
                                //}
                                //else
                                //{
                                dictStack.Push(thisDict[name]);
                                thisDict = (Dictionary<string, object>)thisDict[name];
                                //}                  
                                break;
                            case JsonToken.StartArray:
                                currentType = JsonToken.StartArray;
                                thisDict[name] = thisArr = new List<object>();
                                arrStack.Push(thisArr);
                                break;
                            default://如果是值                                   
                                if (currentType == JsonToken.StartObject)
                                {
                                    thisDict[name] = reader.Value;
                                }
                                else if (currentType == JsonToken.StartArray)
                                {
                                    //if (preType == JsonToken.StartArray)
                                    //{
                                    //    thisDict[name] = reader.Value;
                                    //}
                                    //else
                                    //{
                                    thisArr.Add(reader.Value);
                                    //}                                   
                                }
                                break;
                        }
                        break;
                    case JsonToken.EndObject:
                        if (dictStack.Any())
                        {
                            dictStack.Pop();
                        }
                        if (dictStack.Any())
                        {
                            thisDict = (Dictionary<string, object>)dictStack.Peek();
                        }
                        else if (!arrStack.Any())// && preType != JsonToken.StartArray
                        {
                            flag = false;//结束属性
                        }
                        else//原来没有，为数组类型打的补丁
                        {
                            thisDict = new Dictionary<string, object>();//重新创建加给数组
                            arrStack.Push(thisDict);
                        }
                        break;
                    case JsonToken.EndArray:
                        if (arrStack.Any())
                        {
                            arrStack.Pop();
                        }
                        if (arrStack.Any())
                        {
                            if (arrStart)
                            {
                                thisArr.AddRange(arrStack);
                                flag = false;
                            }
                            else
                            {
                                thisArr = (List<object>)arrStack.Peek();
                            }
                        }
                        else if (!dictStack.Any())
                        {
                            flag = false;
                        }
                        break;
                    default:
                        var v = reader.Value;
                        if (v != null)
                        {
                            thisArr.Add(v);
                        }
                        else if (currentType == JsonToken.StartArray)//数组内存放的是对象会出现这样的情况
                        {
                            //preType = currentType;
                            arrStack.Push(thisDict);
                        }
                        currentType = reader.TokenType;
                        break;
                }
            }


            var res = JsonConvert.SerializeObject(root);
            //return root;
            return res;
        }


        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }
    }
}