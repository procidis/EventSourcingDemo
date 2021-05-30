using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EventSourcingDemo.Extensions
{
    public static class ObjectExtensions
    {
        public static string ToJson(this object data) => JsonConvert.SerializeObject(data);
    }
}
