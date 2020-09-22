using System;
using GW2SDK.Impl.JsonReaders.Mappings;
using static System.Linq.Expressions.Expression;

namespace GW2SDK.Impl.JsonReaders.Linq
{
    public partial class JsonExpressionCompiler
    {
        public void VisitProperty(IJsonPropertyMapping mapping)
        {
            var ctx = Context.Peek();
            
            if (mapping.Significance == MappingSignificance.Ignored)
            {
                Builder.Body.Add(Empty());
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
