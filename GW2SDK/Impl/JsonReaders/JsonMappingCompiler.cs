﻿using System.Collections.Generic;
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

        public Expression<ReadJson<TObject>> Build(JsonObjectMapping<TObject> mapping)
        {
            var inputExpr = Parameter(typeof(JsonElement).MakeByRefType(), "json");
            var pathExpr = Parameter(typeof(JsonPath).MakeByRefType(),     "path");

            mapping.Accept(this);

            var root = (ObjectNode) Nodes.Pop();

            return Lambda<ReadJson<TObject>>(
                Block(
                    root.GetVariables(),
                    root.MapNode(inputExpr, pathExpr),
                    root.GetResult()
                ),
                inputExpr,
                pathExpr
            );
        }
    }
}
