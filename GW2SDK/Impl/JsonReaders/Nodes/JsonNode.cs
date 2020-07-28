using System.Collections.Generic;
using System.Linq.Expressions;
using GW2SDK.Impl.JsonReaders.Mappings;

namespace GW2SDK.Impl.JsonReaders.Nodes
{
    public abstract class JsonNode
    {
        public JsonMapping Mapping { get; set; } = default!;

        public abstract IEnumerable<ParameterExpression> GetVariables();

        public abstract IEnumerable<Expression> GetValidations();

        public abstract IEnumerable<MemberBinding> GetBindings();
    }
}