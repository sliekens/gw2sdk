using System.Collections.Generic;
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

        public IEnumerable<T> Read(in JsonElement element, in JsonPath path)
        {
            if (element.ValueKind != JsonValueKind.Array)
            {
                throw new JsonException($"Value is not an array.", path.ToString(), null, null);
            }

            var index = 0;
            var result = new List<T>(element.GetArrayLength());
            foreach (var item in element.EnumerateArray())
            {
                result.Add(_itemReader.Read(item, path.AccessArrayIndex(index++)));
            }

            return result;
        }

        public bool CanRead(in JsonElement json) => json.ValueKind == JsonValueKind.Array;
    }
}
