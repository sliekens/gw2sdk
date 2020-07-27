using System;
using System.Text.Json;

namespace GW2SDK.Impl.JsonReaders
{
    public class JsonStringEnumReader<TEnum> : IJsonReader<TEnum> where TEnum : struct
    {
        private readonly bool _ignoreCase;

        public JsonStringEnumReader(bool ignoreCase = false) => _ignoreCase = ignoreCase;

        public TEnum Read(in JsonElement json) => Enum.Parse<TEnum>(json.GetString(), _ignoreCase);

        public bool CanRead(in JsonElement json) => json.ValueKind == System.Text.Json.JsonValueKind.String && Enum.IsDefined(typeof(TEnum), json.GetString());
    }
}
