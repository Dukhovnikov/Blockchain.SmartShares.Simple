using System;
using Newtonsoft.Json;

namespace SmartShares
{
    public class ByteArrayConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var base64String = Convert.ToBase64String((byte[])value);

            serializer.Serialize(writer, base64String);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(byte[]).IsAssignableFrom(objectType);
        }
        
        public override bool CanRead => false;
    }
}