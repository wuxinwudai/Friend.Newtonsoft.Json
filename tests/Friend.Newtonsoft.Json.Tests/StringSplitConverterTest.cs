using Xunit;
using Xunit.Abstractions;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace Friend.Newtonsoft.Json.Tests
{
    public class StringSplitConverterTest
    {
        class ObjectDemo
        {
            public List<object> Array { get; set; }
        }
        class StringDemo
        {
            public List<string> Array { get; set; }
        }
        class DecimalDemo
        {
            public List<decimal> Array { get; set; }
        }
        class JsonDemo
        {
            public string ArrayString { get; set; } = "1;2;3";
        }
        protected readonly ITestOutputHelper output;
        public StringSplitConverterTest(ITestOutputHelper outputHelper)
        {
            output = outputHelper;
        }

        [Fact]
        public void Test1()
        {
            var user = new JsonDemo() { ArrayString = "a.jpg;b.jpg" };
            var jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.Converters.Add(new StringSplitConverter(';'));
            var serializedUser = JsonConvert.SerializeObject(user, jsonSerializerSettings);
            var jo = JObject.Parse(serializedUser);
            Assert.Equal(2, jo["ArrayString"].Values<string>().Count());
        }
        [Fact]
        public void Test2()
        {
            var user = new JsonDemo() { ArrayString = "1,2,11111111111111111113" };
            var jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.Converters.Add(new StringSplitConverter(',',StringSplitOption.DecimalArray));
            var serializedUser = JsonConvert.SerializeObject(user, jsonSerializerSettings);
            var jo = JObject.Parse(serializedUser);
            Assert.Equal(3, jo["ArrayString"].Values<decimal>().Count());
        }
        [Fact]
        public void Test3()
        {
            var user = new JsonDemo() { ArrayString = "1,2,3.14159" };
            var jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.Converters.Add(new StringSplitConverter(',',StringSplitOption.DoubleArray));
            var serializedUser = JsonConvert.SerializeObject(user, jsonSerializerSettings);
            var jo = JObject.Parse(serializedUser);
            Assert.Equal(3, jo["ArrayString"].Values<double>().Count());
        }
        [Fact]
        public void Test4()
        {
            var user = new JsonDemo() { ArrayString = $"1,2,{byte.MaxValue}" };
            var jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.Converters.Add(new StringSplitConverter(',', StringSplitOption.ByteArray));
            var serializedUser = JsonConvert.SerializeObject(user, jsonSerializerSettings);
            var jo = JObject.Parse(serializedUser);
            Assert.Equal(3, jo["ArrayString"].Values<byte>().Count());
        }
        [Fact]
        public void Test5()
        {
            var user = new JsonDemo() { ArrayString = $"1,2,{long.MaxValue}" };
            var jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.Converters.Add(new StringSplitConverter(StringSplitOption.LongArray));
            var serializedUser = JsonConvert.SerializeObject(user, jsonSerializerSettings);
            var jo = JObject.Parse(serializedUser);
            Assert.Equal(3, jo["ArrayString"].Values<long>().Count());
        }
        [Fact]
        public void Test6()
        {
            var user = new JsonDemo() { ArrayString = "" };
            var jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.Converters.Add(new StringSplitConverter(';'));
            var serializedUser = JsonConvert.SerializeObject(user, jsonSerializerSettings);
            var jo = JObject.Parse(serializedUser);
            Assert.Equal($"{{\"ArrayString\":[]}}",serializedUser);
            Assert.Equal(0, jo["ArrayString"].Values<string>().Count());

        }
    }
}