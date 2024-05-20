using Application.Libraries;
using Utf8Json;
using Utf8Json.Resolvers;

namespace Shared;
public class JsonConverter : IJsonConverter
{
    public T DeserializeObject<T>(string value) => JsonSerializer.Deserialize<T>(value);

    public string SerializeObject(object obj)=> JsonSerializer.ToJsonString(obj, StandardResolver.Default);

}