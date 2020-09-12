using System.Collections.Generic;
using System.Linq.Expressions;
using GW2SDK.Impl.JsonReaders.Mappings;

namespace GW2SDK.Impl.JsonReaders.Nodes
{
    public abstract class JsonNode
    {
        /// <summary>
        /// Gets the variables to declare for storing JSON values.
        /// </summary>
        public abstract IEnumerable<ParameterExpression> GetVariables();

        public abstract Expression MapNode(Expression jsonElementExpr, Expression jsonPathExpr);

        public abstract Expression GetResult();
    }
}