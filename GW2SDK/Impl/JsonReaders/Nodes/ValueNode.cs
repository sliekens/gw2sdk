using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GW2SDK.Impl.JsonReaders.Mappings;
using static System.Linq.Expressions.Expression;

namespace GW2SDK.Impl.JsonReaders.Nodes
{
    public class ValueNode : JsonNode
    {
        public ParameterExpression ActualValueExpr { get; set; } = default!;

        public IValueExpressionMapper ValueExpressionMapper { get; set; } = default!;

        public override IEnumerable<ParameterExpression> GetVariables()
        {
            if (Mapping.Significance != MappingSignificance.Ignored)
            {
                yield return ActualValueExpr;
            }
        }

        public Expression MapExpr(Expression jsonElementExpr, Expression pathExpr)
        {
            if (Mapping.Significance == MappingSignificance.Ignored)
            {
                return Empty();
            }

            var mapValueExpr = ValueExpressionMapper.MapValueExpr(jsonElementExpr, pathExpr);
            return Assign(ActualValueExpr, mapValueExpr);
        }

        public override IEnumerable<Expression> GetValidations(Type targetType)
        {
            // validations are done in-line (at least for now)
            yield break;
        }

        public override IEnumerable<MemberBinding> GetBindings()
        {
            yield break;
        }
    }
}