using System.Text.Json;

namespace GW2SDK.Impl.JsonReaders
{
    public delegate TValue ReadJson<out TValue>(in JsonElement json, in JsonPath path);
}
