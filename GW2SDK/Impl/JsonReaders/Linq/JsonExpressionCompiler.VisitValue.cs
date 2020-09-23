using System.Linq.Expressions;
using GW2SDK.Impl.Json;
using GW2SDK.Impl.JsonReaders.Mappings;

namespace GW2SDK.Impl.JsonReaders.Linq
{
    public partial class JsonExpressionCompiler
    {
        public void VisitValue(IJsonValueMapping mapping)
        {
            if (mapping.Significance == MappingSignificance.Ignored)
            {
                return;
            }

            var ctx = Context.Peek();
            Builder.Then
            (
                mapping.Significance switch
                {
                    MappingSignificance.Required => mapping.ValueKind switch
                    {
                        JsonValueMappingKind.Any => JsonElementExpr.GetAny
                        (
                            mapping.ValueType!,
                            ctx.JsonNodeExpression,
                            ctx.JsonPathExpression,
                            mapping.ConvertJsonElement!
                        ),
                        JsonValueMappingKind.String => JsonElementExpr.GetString
                            (ctx.JsonNodeExpression, ctx.JsonPathExpression),
                        JsonValueMappingKind.DateTime => JsonElementExpr.GetDateTime
                            (ctx.JsonNodeExpression, ctx.JsonPathExpression),
                        JsonValueMappingKind.DateTimeOffset => JsonElementExpr.GetDateTimeOffset
                            (ctx.JsonNodeExpression, ctx.JsonPathExpression),
                        JsonValueMappingKind.Guid => JsonElementExpr.GetGuid
                            (ctx.JsonNodeExpression, ctx.JsonPathExpression),
                        JsonValueMappingKind.Boolean => JsonElementExpr.GetBoolean
                            (ctx.JsonNodeExpression, ctx.JsonPathExpression),
                        JsonValueMappingKind.Single => JsonElementExpr.GetSingle
                            (ctx.JsonNodeExpression, ctx.JsonPathExpression),
                        JsonValueMappingKind.Double => JsonElementExpr.GetDouble
                            (ctx.JsonNodeExpression, ctx.JsonPathExpression),
                        JsonValueMappingKind.Decimal => JsonElementExpr.GetDecimal
                            (ctx.JsonNodeExpression, ctx.JsonPathExpression),
                        JsonValueMappingKind.SByte => JsonElementExpr.GetSByte
                            (ctx.JsonNodeExpression, ctx.JsonPathExpression),
                        JsonValueMappingKind.Int16 => JsonElementExpr.GetInt16
                            (ctx.JsonNodeExpression, ctx.JsonPathExpression),
                        JsonValueMappingKind.Int32 => JsonElementExpr.GetInt32
                            (ctx.JsonNodeExpression, ctx.JsonPathExpression),
                        JsonValueMappingKind.Int64 => JsonElementExpr.GetInt64
                            (ctx.JsonNodeExpression, ctx.JsonPathExpression),
                        JsonValueMappingKind.Byte => JsonElementExpr.GetByte
                            (ctx.JsonNodeExpression, ctx.JsonPathExpression),
                        JsonValueMappingKind.UInt16 => JsonElementExpr.GetUInt16
                            (ctx.JsonNodeExpression, ctx.JsonPathExpression),
                        JsonValueMappingKind.UInt32 => JsonElementExpr.GetUInt32
                            (ctx.JsonNodeExpression, ctx.JsonPathExpression),
                        JsonValueMappingKind.UInt64 => JsonElementExpr.GetUInt64
                            (ctx.JsonNodeExpression, ctx.JsonPathExpression),
                        _ => Expression.Empty()
                    },
                    MappingSignificance.Optional => mapping.ValueKind switch
                    {
                        JsonValueMappingKind.Any => JsonElementExpr.GetAnyOrNull
                        (
                            mapping.ValueType!,
                            ctx.JsonNodeExpression,
                            ctx.JsonPathExpression,
                            mapping.ConvertJsonElement!
                        ),
                        JsonValueMappingKind.String => JsonElementExpr.GetStringOrNull
                            (ctx.JsonNodeExpression, ctx.JsonPathExpression),
                        JsonValueMappingKind.DateTime => JsonElementExpr.GetDateTimeOrNull
                            (ctx.JsonNodeExpression, ctx.JsonPathExpression),
                        JsonValueMappingKind.DateTimeOffset => JsonElementExpr
                            .GetDateTimeOffsetOrNull
                                (ctx.JsonNodeExpression, ctx.JsonPathExpression),
                        JsonValueMappingKind.Guid => JsonElementExpr.GetGuidOrNull
                            (ctx.JsonNodeExpression, ctx.JsonPathExpression),
                        JsonValueMappingKind.Boolean => JsonElementExpr.GetBooleanOrNull
                            (ctx.JsonNodeExpression, ctx.JsonPathExpression),
                        JsonValueMappingKind.Single => JsonElementExpr.GetSingleOrNull
                            (ctx.JsonNodeExpression, ctx.JsonPathExpression),
                        JsonValueMappingKind.Double => JsonElementExpr.GetDoubleOrNull
                            (ctx.JsonNodeExpression, ctx.JsonPathExpression),
                        JsonValueMappingKind.Decimal => JsonElementExpr.GetDecimalOrNull
                            (ctx.JsonNodeExpression, ctx.JsonPathExpression),
                        JsonValueMappingKind.SByte => JsonElementExpr.GetSByteOrNull
                            (ctx.JsonNodeExpression, ctx.JsonPathExpression),
                        JsonValueMappingKind.Int16 => JsonElementExpr.GetInt16OrNull
                            (ctx.JsonNodeExpression, ctx.JsonPathExpression),
                        JsonValueMappingKind.Int32 => JsonElementExpr.GetInt32OrNull
                            (ctx.JsonNodeExpression, ctx.JsonPathExpression),
                        JsonValueMappingKind.Int64 => JsonElementExpr.GetInt64OrNull
                            (ctx.JsonNodeExpression, ctx.JsonPathExpression),
                        JsonValueMappingKind.Byte => JsonElementExpr.GetByteOrNull
                            (ctx.JsonNodeExpression, ctx.JsonPathExpression),
                        JsonValueMappingKind.UInt16 => JsonElementExpr.GetUInt16OrNull
                            (ctx.JsonNodeExpression, ctx.JsonPathExpression),
                        JsonValueMappingKind.UInt32 => JsonElementExpr.GetUInt32OrNull
                            (ctx.JsonNodeExpression, ctx.JsonPathExpression),
                        JsonValueMappingKind.UInt64 => JsonElementExpr.GetUInt64OrNull
                            (ctx.JsonNodeExpression, ctx.JsonPathExpression),
                        _ => Expression.Empty()
                    },
                    _ => Expression.Empty()
                }
            );
        }
    }
}
