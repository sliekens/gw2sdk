using GW2SDK.Impl.JsonReaders.Mappings;
using GW2SDK.Impl.JsonReaders.Nodes;
using static System.Linq.Expressions.Expression;

namespace GW2SDK.Impl.JsonReaders
{
    public partial class JsonMappingCompiler<TObject>
    {
        public void VisitValue<TValue>(JsonValueMapping<TValue> mapping)
        {
            Nodes.Push(
                new ValueNode
                {
                    Mapping = mapping,
                    ActualValueExpr = Variable(typeof(TValue), $"{mapping.Name}_value"),
                    ValueExpressionMapper = new ValueExpressionMapper<TValue>(mapping)
                }
            );
        }
    }
}
