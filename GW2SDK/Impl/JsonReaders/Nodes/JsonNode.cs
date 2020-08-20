using System;
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

        /// <summary>
        /// Gets expressions to run after reading a JsonElement.
        /// </summary>
        /// <param name="targetType"></param>
        public abstract IEnumerable<Expression> GetValidations(Type targetType);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<MemberBinding> GetBindings();
    }
}