using System;
using System.Dynamic;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using YamlDotNet.Serialization;

using Serializer = SharpYaml.Serialization.Serializer;

namespace Yarm.Converters
{
    /// <summary>
    /// This represents the string extension entity to convert between JSON and YAML.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Converts the JSON string to YAML string.
        /// </summary>
        /// <param name="json">JSON string.</param>
        /// <returns>Returns YAML string converted.</returns>
        public static string ToYaml(this string json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                return null;
            }

            var converter = new ExpandoObjectConverter();
            ExpandoObject deserialised;

            try
            {
                deserialised = JsonConvert.DeserializeObject<ExpandoObject>(json, converter);
            }
            catch (Exception ex)
            {
                throw new InvalidJsonException(ex.Message);
            }

            var yaml = new SerializerBuilder().Build().Serialize(deserialised);

            return yaml;
        }

        /// <summary>
        /// Converts the YAML string to JSON string.
        /// </summary>
        /// <param name="yaml">YAML string.</param>
        /// <param name="skipXYarm">If set to 'true', ignores top level properties starting with 'x-yarm'.</param>
        /// <returns>Returns JSON string converted.</returns>
        public static string ToJson(this string yaml, bool skipXYarm = true)
        {
            if (string.IsNullOrWhiteSpace(yaml))
            {
                return null;
            }

            object deserialised;
            try
            {
                deserialised = new Serializer().Deserialize(yaml);
            }
            catch (Exception ex)
            {
                throw new InvalidYamlException(ex.Message);
            }

            var json = skipXYarm
                ? JsonConvert.SerializeObject(deserialised, Formatting.Indented, new IgnoreXYarmJsonConverter())
                : JsonConvert.SerializeObject(deserialised, Formatting.Indented);

            return json;
        }
    }
}