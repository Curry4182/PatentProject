using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Patent.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Patent.Services
{
    public static class JSonConfig
    {
        public static Dictionary<string, string> JSonDic { get;}

        static JSonConfig()
        {
            string json = File.ReadAllText(@"appsettings.json");

            var dic = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(json);
        }

        public static Dictionary<string, string> JSonToDic(string json)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }
    }
}
