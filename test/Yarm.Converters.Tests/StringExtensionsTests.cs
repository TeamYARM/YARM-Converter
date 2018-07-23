using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Yarm.Converters.Tests
{
    /// <summary>
    /// This represents the test entity for the <see cref="StringExtensions"/> class.
    /// </summary>
    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        public void Given_NullParameter_ToYaml_ShouldReturn_Null()
        {
            var result = StringExtensions.ToYaml(null);

            result.Should().BeNullOrWhiteSpace();
        }

        [TestMethod]
        public void Given_InvalidJson_ToYaml_ShouldThrow_Exception()
        {
            var key1 = "key1";
            var value1 = "value1";
            var key2 = "key2";
            var value2 = 2;
            var key3 = "key3";
            var value3 = true;
            var key4 = "key4";
            var value4 = new[] { "value41", "value42" };

            var dic = new Dictionary<string, object>() { { key1, value1 }, { key2, value2 }, { key3, value3 }, { key4, value4 } };
            var json = JsonConvert.SerializeObject(dic).TrimEnd('}');

            Action action = () => StringExtensions.ToYaml(json);

            action.Should().Throw<InvalidJsonException>();
        }

        [TestMethod]
        public void Given_Json_ToYaml_ShouldReturn_Result()
        {
            var key1 = "key1";
            var value1 = "value1";
            var key2 = "key2";
            var value2 = 2;
            var key3 = "key3";
            var value3 = true;
            var key4 = "key4";
            var value4 = new[] { "value41", "value42" };

            var dic = new Dictionary<string, object>() { { key1, value1 }, { key2, value2 }, { key3, value3 }, { key4, value4 } };
            var json = JsonConvert.SerializeObject(dic);

            var result = StringExtensions.ToYaml(json);

            var lines = result.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach (var key in dic.Keys)
            {
                lines.Count(p => p.StartsWith(key)).Should().Be(1);
            }
        }

        [TestMethod]
        public void Given_NullParameter_ToJson_ShouldReturn_Null()
        {
            var result = StringExtensions.ToJson(null);

            result.Should().BeNullOrWhiteSpace();
        }

        [TestMethod]
        public void Given_InvalidYaml_ToJson_ShouldThrow_Exception()
        {
            var key1 = "key1";
            var value1 = "value1";
            var key2 = "key2";
            var value2 = 2;
            var key3 = "key3";
            var value3 = true;
            var key4 = "key4";
            var value41 = "value41";
            var value42 = "value42";

            var sb = new StringBuilder();
            sb.AppendLine($"{key1}: {value1}");
            sb.AppendLine($"{key2}: {value2}");
            sb.AppendLine($"{key3}: {value3.ToString().ToLowerInvariant()}");
            sb.AppendLine($"{key4}:");
            sb.AppendLine($"* {value41}");
            sb.AppendLine($"* {value42}");

            var yaml = sb.ToString();

            Action action = () => StringExtensions.ToJson(yaml);

            action.Should().Throw<InvalidYamlException>();
        }

        [TestMethod]
        public void Given_Yaml_ToJson_ShouldReturn_Result()
        {
            var key1 = "key1";
            var value1 = "value1";
            var key2 = "key2";
            var value2 = 2;
            var key3 = "key3";
            var value3 = true;
            var key4 = "key4";
            var value41 = "value41";
            var value42 = "value42";

            var sb = new StringBuilder();
            sb.AppendLine($"{key1}: {value1}");
            sb.AppendLine($"{key2}: {value2}");
            sb.AppendLine($"{key3}: {value3.ToString().ToLowerInvariant()}");
            sb.AppendLine($"{key4}:");
            sb.AppendLine($"- {value41}");
            sb.AppendLine($"- {value42}");

            var yaml = sb.ToString();

            var result = StringExtensions.ToJson(yaml);

            var dic = JsonConvert.DeserializeObject<JObject>(result);
            dic[key1].Value<string>().Should().Be(value1);
            dic[key2].Value<int>().Should().Be(value2);
            dic[key3].Value<bool>().Should().Be(value3);
            dic[key4].Values<string>().Should().BeEquivalentTo(value41, value42);
        }
    }
}