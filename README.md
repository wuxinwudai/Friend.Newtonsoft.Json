# Friend.Newtonsoft.Json

Friend.Newtonsoft.Json 是对 [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json) 的一个扩展

## 安装 / Installation

- [Package Manager](https://www.nuget.org/packages/Friend.Newtonsoft.Json)

```powershell
Install-Package Friend.Newtonsoft.Json
```

- [.NET CLI](https://www.nuget.org/packages/Friend.Newtonsoft.Json)

```powershell
dotnet add package Friend.Newtonsoft.Json
```
## 例子 / Examples

``` csharp
public class User
{   
    [JsonConverter(typeof(GuidConverter))]
    public Guid Id { get;set; }
    [JsonConverter(typeof(DateConverter))]
    public DateTime Birthday { get; set; }
    [JsonConverter(typeof(UriConverter),"https://static.test.cn")]
    public DateTime Avatar { get; set; }
    [JsonConverter(typeof(StringSplitConverter))]
    public string ProfileImages { get; set;}
}
```

## 点赞 / Support Me

请给仓库点个赞。

## 持续中 / Continue

仓库的下一个版本将支持将js数组对象直接序列化存储到属性内。
如 
``` js
var obj= {
    images : [{
        fileName: '1.jpg',
        ext: '.jpg'
    }]
}
```
序列化存储给
``` csharp
public class Obj{
    [JsonConvert(typeof(StringJsonConverter))]
    public string Images {get; set;}
}
```