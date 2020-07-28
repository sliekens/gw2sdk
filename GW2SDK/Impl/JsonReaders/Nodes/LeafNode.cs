using System.Collections.Generic;
using System.Linq.Expressions;
using GW2SDK.Impl.Json;
using GW2SDK.Impl.JsonReaders.Mappings;

namespace GW2SDK.Impl.JsonReaders.Nodes
{
    public class LeafNode : JsonNode
    {
        public ParameterExpression ActualValueExpr { get; set; } = default!;

        public Expression MapExpr(Expression jsonElementExpr)
        {
            // TODO: other types
            switch (Mapping.Significance)
            {
                case MappingSignificance.Required:
                    return Expression.Assign(ActualValueExpr, JsonElementExpr.GetInt32(jsonElementExpr));
                case MappingSignificance.Optional:
                    return Expression.Assign(ActualValueExpr, JsonElementExpr.GetInt32OrNull(jsonElementExpr));
                default:
                    return Expression.Empty();
            }
        }

        public override IEnumerable<ParameterExpression> GetVariables()
        {
            if (Mapping.Significance != MappingSignificance.Ignored)
            {
                yield return ActualValueExpr;
            }
        }

        public override IEnumerable<Expression> GetValidations()
        {
            // validations are done in-line (at least for now)
            yield break;
        }

        public override IEnumerable<MemberBinding> GetBindings()
        {
            if (Mapping.Significance == MappingSignificance.Ignored)
            {
                yield break;
            }

            yield return Expression.Bind(Mapping.Destination, ActualValueExpr);
        }
    }
}