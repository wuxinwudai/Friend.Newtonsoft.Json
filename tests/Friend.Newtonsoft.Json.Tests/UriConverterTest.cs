using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;

namespace Friend.Newtonsoft.Json.Tests
{
    public class User
    {
        [JsonConverter(typeof(UriConverter))]
        public string Avatar1 { get; set; }
        [JsonConverter(typeof(UriConverter), "https://static.test.cn")]
        public string Avatar2 { get; set; }
        [JsonConverter(typeof(DateConverter))]
        public DateTime Birthday { get; set; }
       
    }
    public class UriConverterTest
    {
        [Fact]
        public void Test1()
        {
            UriConverter.DefaultHostName = "localhost";
            var user = new User
            { 
                Avatar1 = "1.jpg",//没有配置 host，使用全局配置
                Avatar2 = "1.jpg" 
            };
            var json = JsonConvert.SerializeObject(user);
            var deUser = JsonConvert.DeserializeObject<User>(json);
            Assert.Equal("http://localhost/1.jpg", deUser.Avatar1);
            Assert.Equal("https://static.test.cn/1.jpg", deUser.Avatar2);
        }
    }
}