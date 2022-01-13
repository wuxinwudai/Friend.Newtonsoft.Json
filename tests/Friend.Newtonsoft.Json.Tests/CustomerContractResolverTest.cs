using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Newtonsoft.Json;
using Friend.Newtonsoft.Json.Serialization;

namespace Friend.Newtonsoft.Json.Tests
{
    public class CustomerContractResolverTest
    {
        class Demo
        {
            public string Name { get; set; }
        }

        [Fact]
        public void Test()
        {
            var js = new JsonSerializerSettings()
            {
                ContractResolver = new CustomContractResolver(),
                DateFormatString = "yyyy-MM-dd HH:mm:ss",
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };            
            var demo = new Demo();
            //var df = JsonConvert.SerializeObject(demo);//{"Name":null}
            Assert.Equal("{\"name\":\"\"}", JsonConvert.SerializeObject(demo,js));
        }
    }
}