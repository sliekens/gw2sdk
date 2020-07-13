using System.Text.Json;

namespace GW2SDK.Impl.JsonReaders
{
    internal interface IJsonReader<out T>
    {
        T Read(in JsonElement json);
    }
}