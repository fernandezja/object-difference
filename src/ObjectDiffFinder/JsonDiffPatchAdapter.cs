using JsonDiffPatchDotNet;
using KellermanSoftware.CompareNetObjects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectDiffFinder
{
    /// <summary>
    /// https://github.com/wbish/jsondiffpatch.net
    /// https://www.nuget.org/packages/JsonDiffPatch.Net/
    /// </summary>
    public static class JsonDiffPatchAdapter
    {
        /// <summary>
        /// With Newtonsoft Serialize
        /// </summary>
        /// <param name="object1"></param>
        /// <param name="object2"></param>
        /// <returns></returns>
        public static string Diff(object object1, object object2)
        {
            var jsonDiffPatch = new JsonDiffPatch();

            var json1 = JsonConvert.SerializeObject(object1);
            var json2 = JsonConvert.SerializeObject(object2);


            var left = JToken.Parse(json1);
            var right = JToken.Parse(json2);

            JToken patch = jsonDiffPatch.Diff(left, right);

            return patch.ToString();
        }

        /*
        /// <summary>
        /// System.Text.Json Serialize (Without Newtonsoft)
        /// </summary>
        /// <param name="object1"></param>
        /// <param name="object2"></param>
        /// <returns></returns>
        public static string Diff2(object object1, object object2)
        {
            var jsonDiffPatch = new JsonDiffPatch();

            var options = new System.Text.Json.JsonSerializerOptions
            {
                Converters = { new Util.JTokenConverterFactory() },
            };
            
            var json1 = System.Text.Json.JsonSerializer.Serialize(object1, options);
            var json2 = System.Text.Json.JsonSerializer.Serialize(object2, options);

            JToken patch = jsonDiffPatch.Diff(left, right);

            return patch.ToString();
        }
        */

    }
}
