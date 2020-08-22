using System.Linq.Expressions;

namespace GW2SDK.Impl.JsonReaders
{
    public interface IValueExpressionMapper
    {
        Expression MapValueExpr(
            Expression jsonElementExpr,
            Expression jsonPathExpr);
    }
}
