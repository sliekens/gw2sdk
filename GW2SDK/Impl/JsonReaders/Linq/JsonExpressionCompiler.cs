using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using GW2SDK.Impl.JsonReaders.Mappings;

namespace GW2SDK.Impl.JsonReaders.Linq
{
    public partial class JsonExpressionCompiler : IJsonMappingVisitor
    {
        private Stack<JsonContext> Context { get; } = new Stack<JsonContext>();

        public BlockBuilder Builder { get; private set; } = new BlockBuilder();

        public ReadJson<TValue> Compile<TValue>(IJsonMapping descriptor)
        {
            var inputExpr = Expression.Parameter(typeof(JsonElement).MakeByRefType(), "json");
            var pathExpr = Expression.Parameter(typeof(JsonPath).MakeByRefType(),     "path");
            Context.Push(new JsonContext(inputExpr, pathExpr));
            descriptor.Accept(this);
            Context.Pop();

            var func = Expression.Lambda<ReadJson<TValue>>(Builder.ToExpression(), inputExpr, pathExpr);
            return func.Compile();
        }

        private sealed class JsonContext
        {
            public JsonContext(Expression jsonNodeExpression, Expression jsonPathExpression)
            {
                JsonNodeExpression = jsonNodeExpression;
                JsonPathExpression = jsonPathExpression;
            }

            /// <summary>A <see cref="JsonElement"/> or <see cref="JsonProperty"/> expression.</summary>
            public Expression JsonNodeExpression { get; }

            /// <summary>A <see cref="JsonPath"/>.</summary>
            public Expression JsonPathExpression { get; }
        }
    }
}
