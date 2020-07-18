using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace GW2SDK.Impl.JsonReaders
{
    internal sealed class JsonArrayReader<T> : IJsonReader<IEnumerable<T>>
    {
        private readonly IJsonReader<T> _itemReader;

        public JsonArrayReader(IJsonReader<T> itemReader)
        {
            _itemReader = itemReader;
        }

        public IEnumerable<T> Read(in JsonElement json)
        {
            if (json.ValueKind != JsonValueKind.Array)
            {
                throw new JsonException("JSON is not an array.");
            }

            return json.EnumerateArray().Select(item => _itemReader.Read(item)).ToList();
        }

        public bool CanRead(in JsonElement json) => json.ValueKind == JsonValueKind.Array;
    }
}
