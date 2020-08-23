using System.Collections.Generic;
using System.Linq.Expressions;
using GW2SDK.Impl.JsonReaders.Mappings;

namespace GW2SDK.Impl.JsonReaders.Nodes
{
    public abstract class JsonNode
    {
        public JsonMapping Mapping { get; set; } = default!;

        /// <summary>
        /// Gets the variables to declare for storing JSON values.
        /// </summary>
        public abstract IEnumerable<ParameterExpression> GetVariables();

        public abstract Expression MapNode(Expression jsonNodeExpr, Expression pathExpr);

        public abstract Expression GetResult();
    }
}