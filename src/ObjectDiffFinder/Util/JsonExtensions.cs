using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ObjectDiffFinder.Util
{
    public static class JsonExtensions
    {
        public static void WriteOrSerialize<T>(this JsonConverter<T> converter, 
                                                Utf8JsonWriter writer, 
                                                T value, 
                                                JsonSerializerOptions options)
        {
            if (converter != null)
                converter.Write(writer, value, options);
            else
                JsonSerializer.Serialize(writer, value, options);
        }
    }
}
