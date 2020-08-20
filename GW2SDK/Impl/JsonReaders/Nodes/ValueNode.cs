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

        public Func<Expression, Expression, Expression> MapExprFactory { get; set; } = (jsonElementExpr, pathExpr) => Empty();

        public Expression MapExpr(Expression jsonElementExpr, Expression pathExpr) => MapExprFactory(jsonElementExpr, pathExpr);

        public override IEnumerable<ParameterExpression> GetVariables()
        {
            if (Mapping.Significance != MappingSignificance.Ignored)
            {
                yield return ActualValueExpr;
            }
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