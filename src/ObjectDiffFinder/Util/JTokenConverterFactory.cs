using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Linq;

namespace ObjectDiffFinder.Util
{
    public class JTokenConverterFactory : JsonConverterFactory
    {
        // In case you need to set FloatParseHandling or DateFormatHandling
        readonly Newtonsoft.Json.JsonSerializerSettings _settings;

        public JTokenConverterFactory() { }

        public JTokenConverterFactory(Newtonsoft.Json.JsonSerializerSettings settings) => this._settings = settings;

        public override bool CanConvert(Type typeToConvert) => typeof(JToken).IsAssignableFrom(typeToConvert);

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            var converterType = typeof(JTokenConverter<>).MakeGenericType(new[] { typeToConvert });
            return (JsonConverter)Activator.CreateInstance(converterType, new object[] { options, _settings });
        }

        class JTokenConverter<TJToken> : JsonConverter<TJToken> where TJToken : JToken
        {
            readonly JsonConverter<bool> _boolConverter;
            readonly JsonConverter<long> _longConverter;
            readonly JsonConverter<double> _doubleConverter;
            readonly JsonConverter<decimal> _decimalConverter;
            readonly JsonConverter<string> _stringConverter;
            readonly JsonConverter<DateTime> _dateTimeConverter;
            readonly Newtonsoft.Json.JsonSerializerSettings _settings;

            public override bool CanConvert(Type typeToConvert) => typeof(TJToken).IsAssignableFrom(typeToConvert);

            public JTokenConverter(JsonSerializerOptions options, Newtonsoft.Json.JsonSerializerSettings settings)
            {
                // Cache some converters for efficiency
                _boolConverter = (JsonConverter<bool>)options.GetConverter(typeof(bool));
                _stringConverter = (JsonConverter<string>)options.GetConverter(typeof(string));
                _longConverter = (JsonConverter<long>)options.GetConverter(typeof(long));
                _decimalConverter = (JsonConverter<decimal>)options.GetConverter(typeof(decimal));
                _doubleConverter = (JsonConverter<double>)options.GetConverter(typeof(double));
                _dateTimeConverter = (JsonConverter<DateTime>)options.GetConverter(typeof(DateTime));
                this._settings = settings;
            }

            public override TJToken Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                // This could be substantially optimized for memory use by creating code to read from a Utf8JsonReader and write to a JsonWriter (specifically a JTokenWriter).
                // We could just write the JsonDocument to a string, but System.Text.Json works more efficiently with UTF8 byte streams so write to one of those instead.
                using var doc = JsonDocument.ParseValue(ref reader);
                using var ms = new MemoryStream();
                using (var writer = new Utf8JsonWriter(ms))
                    doc.WriteTo(writer);
                ms.Position = 0;
                using (var sw = new StreamReader(ms))
                using (var jw = new Newtonsoft.Json.JsonTextReader(sw))
                {
                    return Newtonsoft.Json.JsonSerializer.CreateDefault(_settings).Deserialize<TJToken>(jw);
                }
            }

            public override void Write(Utf8JsonWriter writer, TJToken value, JsonSerializerOptions options) =>
                // Optimize for memory use by descending the JToken hierarchy and writing each one out, rather than formatting to a string, parsing to a `JsonDocument`, then writing that.
                WriteCore(writer, value, options);

            void WriteCore(Utf8JsonWriter writer, JToken value, JsonSerializerOptions options)
            {
                if (value == null || value.Type == JTokenType.Null)
                {
                    writer.WriteNullValue();
                    return;
                }

                switch (value)
                {
                    case JValue jvalue when jvalue.GetType() != typeof(JValue): // JRaw, maybe others
                    default: // etc
                        {
                            // We could just format the JToken to a string, but System.Text.Json works more efficiently with UTF8 byte streams so write to one of those instead.
                            using var ms = new MemoryStream();
                            using (var tw = new StreamWriter(ms, Encoding.UTF8, 4096, true))
                            using (var jw = new Newtonsoft.Json.JsonTextWriter(tw))
                            {
                                value.WriteTo(jw);
                            }
                            ms.Position = 0;
                            using var doc = JsonDocument.Parse(ms);
                            doc.WriteTo(writer);
                        }
                        break;
                    // Hardcode some standard cases for efficiency
                    case JValue jvalue when jvalue.Value is bool v:
                        _boolConverter.WriteOrSerialize(writer, v, options);
                        break;
                    case JValue jvalue when jvalue.Value is string v:
                        _stringConverter.WriteOrSerialize(writer, v, options);
                        break;
                    case JValue jvalue when jvalue.Value is long v:
                        _longConverter.WriteOrSerialize(writer, v, options);
                        break;
                    case JValue jvalue when jvalue.Value is decimal v:
                        _decimalConverter.WriteOrSerialize(writer, v, options);
                        break;
                    case JValue jvalue when jvalue.Value is double v:
                        _doubleConverter.WriteOrSerialize(writer, v, options);
                        break;
                    case JValue jvalue when jvalue.Value is DateTime v:
                        _dateTimeConverter.WriteOrSerialize(writer, v, options);
                        break;
                    case JValue jvalue:
                        JsonSerializer.Serialize(writer, jvalue.Value, options);
                        break;
                    case JArray array:
                        {
                            writer.WriteStartArray();
                            foreach (var item in array)
                                WriteCore(writer, item, options);
                            writer.WriteEndArray();
                        }
                        break;
                    case JObject obj:
                        {
                            writer.WriteStartObject();
                            foreach (var p in obj.Properties())
                            {
                                writer.WritePropertyName(p.Name);
                                WriteCore(writer, p.Value, options);
                            }
                            writer.WriteEndObject();
                        }
                        break;
                }
            }
        }
    }
}
