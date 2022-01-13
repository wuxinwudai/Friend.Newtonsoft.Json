using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
namespace Friend.Newtonsoft.Json.Tests
{

    public class StringJsonConverterTest
    {
        public class Config
        {
            public string Name { get; set; }
            public List<string> Paths { get; set; }
            public Dictionary<string,string> Dict { get; set; }
        }

        public class Demo1
        {
            [JsonConverter(typeof(StringJsonConverter))]
            public string Config { get; set; }
        }
        [JsonArray]
        public class Demo2
        {
            [JsonConverter(typeof(StringJsonConverter))]
            public string Configs { get; set; }
        }

        [Fact]
        public void Test()
        {
            var cfg = new Config()
            {
                Name = "MyConfig",
                Paths = new List<string>() { "C:\\","D:\\"},
                Dict = new Dictionary<string, string>()
                {
                    {"AboutMe","Hello" },
                    {"Help","Help Info" }
                }
            };
            var configObjStr = JsonConvert.SerializeObject(cfg);
            var objHasConfigStr = $"{{\"Config\":{configObjStr}}}";
            var demo = JsonConvert.DeserializeObject<Demo1>(objHasConfigStr);
            Assert.Equal(configObjStr, demo.Config);

            //var cfgs = new[] { cfg };
            //var arrConfigObjStr = JsonConvert.SerializeObject(cfg);
            //var objConfigsStr = $"[{{\"Configs\":{configObjStr}}}]";
            //var demo2 = JsonConvert.DeserializeObject<Demo2>(objConfigsStr);
            //Assert.Equal(arrConfigObjStr, demo2.Configs);
        }
    }
}