using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace Yarm.Converters
{
    internal class IgnoreXYarmJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            JToken t = JToken.FromObject(value);

            if (t.Type != JTokenType.Object)
            {
                t.WriteTo(writer);
            }
            else
            {
                JObject o = (JObject)t;

                var propsToRemove = o.Properties().Where(x => x.Name == "x-yarm" || x.Name.StartsWith("x-yarm-")).Select(x => x.Name).ToList();

                foreach(var prop in propsToRemove)
                {
                    o.Remove(prop);
                }

                o.WriteTo(writer);
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException("Unnecessary because CanRead is false. The type will skip the converter.");
        }

        public override bool CanRead
        {
            get { return false; }
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }
    }
}