using System;
using System.Linq.Expressions;
using System.Text.Json;
using GW2SDK.Impl.JsonReaders.Mappings;

namespace GW2SDK.Impl.JsonReaders
{
    public class JsonObjectReader<TObject> : IJsonReader<TObject>
    {
        private readonly JsonObjectMapping<TObject> _mapping = new JsonObjectMapping<TObject>
        {
            Name = "root_node"
        };

        private Expression<ReadJson<TObject>>? _source;

        private ReadJson<TObject>? _read;

        public void Configure(Action<JsonObjectMapping<TObject>> configure)
        {
            var compiler = new JsonMappingCompiler<TObject>();
            configure(_mapping);
            _source = compiler.Build(_mapping);
            _read = _source.Compile();
        }

        public TObject Read(in JsonElement element, in JsonPath path)
        {
            if (_read is null) throw new InvalidOperationException("Call Configure before attempting to Read.");
            return _read(element, path);
        }

        public bool CanRead(in JsonElement json) => json.ValueKind == JsonValueKind.Object;
    }
}
