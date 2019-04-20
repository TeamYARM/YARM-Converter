using System;
using System.Dynamic;
using System.IO;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using YamlDotNet.Core;
using YamlDotNet.Serialization;

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
        /// <returns>Returns JSON string converted.</returns>
        public static string ToJson(this string yaml)
        {
            if (string.IsNullOrWhiteSpace(yaml))
            {
                return null;
            }

            object deserialized;
            try
            {
                var reader = new MergingParser(new Parser(new StringReader(yaml)));

                deserialized = new Deserializer().Deserialize(reader);
            }
            catch (Exception ex)
            {
                throw new InvalidYamlException(ex.Message);
            }

            var json = JsonConvert.SerializeObject(deserialized, Formatting.Indented);
            return json;
        }
    }
}