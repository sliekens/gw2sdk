using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using GW2SDK.Impl.JsonReaders.Mappings;
using GW2SDK.Impl.JsonReaders.Nodes;
using static System.Linq.Expressions.Expression;

namespace GW2SDK.Impl.JsonReaders
{
    public partial class JsonMappingCompiler<TObject> : IJsonMappingVisitor
    {
        public Stack<JsonNode> Nodes { get; } = new Stack<JsonNode>();

        public Expression<ReadJson<TObject>>? Source { get; set; }

        public ReadJson<TObject> Compile(JsonObjectMapping<TObject> mapping)
        {
            var inputExpr = Parameter(typeof(JsonElement).MakeByRefType(), "json");

            mapping.Accept(this);

            var root = (ObjectNode) Nodes.Pop();

            var reader = Lambda<ReadJson<TObject>>(root.CreateExpr(inputExpr), inputExpr);
            Source = reader;
            return reader.Compile();
        }
    }
}
