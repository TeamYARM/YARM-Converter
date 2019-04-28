using System.Collections.Generic;
using System.IO;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using YamlDotNet.Core;
using YamlDotNet.Serialization;

namespace Yarm.Converters.Tests
{
    /// <summary>
    /// This represents the test entity for the <see cref="IgnoreXYarmJsonConverter"/> class.
    /// </summary>
    [TestClass]
    public class IgnoreXYarmJsonConverterTests
    {
        private const string xYarmYaml = @"
x-yarm-very-ignored: 3
key1: value
x-yarm:
    shouldBeIgnored: yup
key2: value2
key3:
    x-yarm-not-ignored: mhm";

        [TestMethod]
        public void Given_Yaml_When_Serialised_ShouldNotReturn_XYarm()
        {
            var deserialised = (object)null;

            using (var reader = new StringReader(xYarmYaml))
            {
                var parser = new MergingParser(new Parser(reader));
                deserialised = new Deserializer().Deserialize(parser);
            }

            var written = (string)null;
            var converter = new IgnoreXYarmJsonConverter();

            using (var tw = new StringWriter())
            using (var writer = new JsonTextWriter(tw))
            {
                converter.WriteJson(writer, deserialised, new JsonSerializer());

                written = tw.ToString();
            }

            var dic = (IDictionary<string, JToken>)JsonConvert.DeserializeObject<JObject>(written);
            dic.ContainsKey("x-yarm-very-ignored").Should().BeFalse();
            dic.ContainsKey("x-yarm").Should().BeFalse();
            dic["key1"].Value<string>().Should().Be("value");
            dic["key2"].Value<string>().Should().Be("value2");
            dic["key3"]["x-yarm-not-ignored"].Value<string>().Should().Be("mhm");
        }
    }
}