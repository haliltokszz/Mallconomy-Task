using MongoDB.Bson;
using Newtonsoft.Json;

namespace Core.Utilities;

public class ObjectIdConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        var objectId = (ObjectId)value;
        writer.WriteValue(objectId.ToString());
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var value = (string)reader.Value;
        return new ObjectId(value);
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(ObjectId);
    }
}