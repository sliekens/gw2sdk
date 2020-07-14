using System.Linq.Expressions;

namespace GW2SDK.Impl.JsonReaders
{
    internal delegate BlockExpression Process(ParameterExpression jsonPropertyExpression, LabelTarget continueLabel);
}
