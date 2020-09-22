using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using GW2SDK.Impl.JsonReaders.Linq;
using GW2SDK.Impl.JsonReaders.Mappings;
using static System.Linq.Expressions.Expression;

namespace GW2SDK.Impl.JsonReaders
{
    public partial class JsonMappingCompiler<TRootElement> : IJsonMappingVisitor
    {
        public Stack<JsonDescriptor> Nodes { get; } = new Stack<JsonDescriptor>();

        public Expression<ReadJson<TRootElement>> Build(IJsonObjectMapping mapping)
        {
            var inputExpr = Parameter(typeof(JsonElement).MakeByRefType(), "json");
            var pathExpr = Parameter(typeof(JsonPath).MakeByRefType(),     "path");

            mapping.Accept(this);

            var root = (ObjectDescriptor) Nodes.Pop();

            return Lambda<ReadJson<TRootElement>>(
                Block(
                    root.GetVariables(),
                    root.MapElement(inputExpr, pathExpr),
                    root.GetResult()
                ),
                inputExpr,
                pathExpr
            );
        }
    }
}
