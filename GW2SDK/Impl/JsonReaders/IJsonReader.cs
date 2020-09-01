using System.Text.Json;

namespace GW2SDK.Impl.JsonReaders
{
    public delegate T ConvertJsonElement<out T>(in JsonElement jsonElement, in JsonPath jsonPath);

    public interface IJsonReader<out T>
    {
        T Read(in string json) => Read(JsonDocument.Parse(json));

        T Read(in JsonDocument json) => Read(json.RootElement, JsonPath.Root);

        T Read(in JsonElement element, in JsonPath path);

        bool CanRead(in JsonElement json);
    }
}
