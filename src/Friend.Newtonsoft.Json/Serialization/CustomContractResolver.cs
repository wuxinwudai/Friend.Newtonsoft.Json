using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Friend.Newtonsoft.Json.Serialization
{
    public class CustomContractResolver : CamelCasePropertyNamesContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            return type.GetProperties()              
                .Select(p =>
                {
                    var jp = base.CreateProperty(p, memberSerialization);
                    if (p.PropertyType == typeof(string))
                    {
                        jp.ValueProvider = new NullToEmptyStringValueProvider(p);
                    }                    
                    return jp;
                }).ToList();
        }

    }
    //protected override JsonContract CreateContract(Type objectType)
    //    {
    //        var contract = base.CreateContract(objectType);
    //        if (objectType == typeof(Guid))
    //        {
    //            contract.Converter = new GuidConverter();
    //        }
    //        return contract;
    //    }

    //}
}
