using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectDiffFinder
{
    /// <summary>
    /// 
    /// </summary>
    public static class JsonSimpleCompare
    {
       
        public static string Diff(object object1, object object2)
        {
            var options = new System.Text.Json.JsonSerializerOptions();
            
            var json1 = System.Text.Json.JsonSerializer.Serialize(object1, options);
            var json2 = System.Text.Json.JsonSerializer.Serialize(object2, options);

            var dictionary1 = JsonConvert.DeserializeObject<Dictionary<string, object>>(json1);
            var dictionary2 = JsonConvert.DeserializeObject<Dictionary<string, object>>(json2);

            var diff = new StringBuilder();

            foreach (var item in dictionary1)
            {
                var key = item.Key;

                var value1 = item.Value;
                var value2 = dictionary2[key];

                if (value1 != value2)
                {
                    diff.Append($"field={key} > value1={value1} value2={value2} | ");
                }
            }

            return diff.ToString();
        }

    }
}
