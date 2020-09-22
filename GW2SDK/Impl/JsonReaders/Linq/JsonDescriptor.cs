using System.Collections.Generic;
using System.Linq.Expressions;

namespace GW2SDK.Impl.JsonReaders.Linq
{
    public enum JsonDescriptorType
    {
        Undefined,

        Array,

        Object,
        
        Property,
        
        Value,
        
        Discriminator
    }

    public abstract class JsonDescriptor
    {
        public abstract JsonDescriptorType DescriptorType { get; }

        /// <summary>
        /// Gets the variables to declare for storing JSON values.
        /// </summary>
        public abstract IEnumerable<ParameterExpression> GetVariables();

        public abstract Expression MapElement(Expression jsonElementExpr, Expression jsonPathExpr);

        public abstract Expression GetResult();
    }
}