namespace Application.Libraries;
public interface IJsonConverter
{
    T DeserializeObject<T>(string value);
    string SerializeObject(object obj);
}
