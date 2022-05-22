using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedClass.ViewModels
{
    public static class StringJsonExtension
    {
        public static T ConvertJsonTo<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
