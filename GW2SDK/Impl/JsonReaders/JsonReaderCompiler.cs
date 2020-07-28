using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using GW2SDK.Impl.JsonReaders.Mappings;
using GW2SDK.Impl.JsonReaders.Nodes;
using static System.Linq.Expressions.Expression;

namespace GW2SDK.Impl.JsonReaders
{
    public partial class JsonReaderCompiler<TObject> : IJsonMappingVisitor
    {
        public Stack<JsonNode> Nodes { get; } = new Stack<JsonNode>();

        public Expression<ReadJson<TObject>>? Source { get; set; }

        public ReadJson<TObject> Compile(JsonAggregateMapping<TObject> mapping)
        {
            var inputExpr = Parameter(typeof(JsonElement).MakeByRefType(), "json");

            mapping.Accept(this);

            var root = (RootNode) Nodes.Pop();

            Compile(root);

            var source = new List<Expression>();
            source.Add(root.MapExpr(inputExpr));
            source.AddRange(root.GetValidations());
            source.Add(
                MemberInit(
                    New(typeof(TObject)),
                    root.GetBindings()
                )
            );

            var body = Block(
                root.GetVariables(),
                source
            );

            var reader = Lambda<ReadJson<TObject>>(body, inputExpr);
            Source = reader;
            return reader.Compile();
        }

        private void Compile(RootNode root)
        {
        }
    }
}
