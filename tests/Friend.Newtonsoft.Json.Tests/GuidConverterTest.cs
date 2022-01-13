using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;

namespace Friend.Newtonsoft.Json.Tests
{    
    public class GuidConverterTest
    {
        
        class Demo1
        {
            [JsonConverter(typeof(GuidConverter))]
            public Guid Id { get; set; }
        }
        class Demo2
        {
            public Guid Id { get; set; }
        }
        [Fact]
        public void Test1()
        {
            var guid = "ebed053702f447fca1f8544deabf38a2";
            var objStr = "{\"Id\": \"ebed053702f447fca1f8544deabf38a2\"}";          
            var demo1 = JsonConvert.DeserializeObject<Demo1>(objStr);
            Assert.Equal(guid, demo1.Id.ToString("N"));
            demo1.Id = Guid.Parse("7717A710-3D9B-4880-8F05-4CF449F41FD8");
            Assert.Equal("{\"Id\":\""+ demo1.Id.ToString("N") +"\"}", JsonConvert.SerializeObject(demo1));
            var demo2 = new Demo2()
            {
                Id = Guid.Parse("ebed053702f447fca1f8544deabf38a2")
        };

            var demo2Str = JsonConvert.SerializeObject(demo2);
            Assert.NotEqual(objStr, demo2Str);
        }
    }
}