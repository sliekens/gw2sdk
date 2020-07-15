using System.Text.Json;

namespace GW2SDK.Impl.JsonReaders
{
    public interface IJsonReader<out T>
    {
        T Read(in JsonDocument json) => Read(json.RootElement);

        T Read(in JsonElement json);
    }
}
