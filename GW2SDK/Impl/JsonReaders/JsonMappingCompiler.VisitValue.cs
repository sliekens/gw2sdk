using System;
using System.Linq.Expressions;
using GW2SDK.Impl.Json;
using GW2SDK.Impl.JsonReaders.Mappings;
using GW2SDK.Impl.JsonReaders.Nodes;
using static System.Linq.Expressions.Expression;

namespace GW2SDK.Impl.JsonReaders
{
    public partial class JsonMappingCompiler<TObject>
    {
        public void VisitValue<TValue>(JsonValueMapping<TValue> mapping)
        {
            ParameterExpression actualValueExpr = Variable(typeof(TValue), $"{mapping.Name}_value");

            Nodes.Push(
                new ValueNode
                {
                    Mapping = mapping,
                    ActualValueExpr = actualValueExpr,
                    MapExprFactory = MapExpr()
                }
            );

            Func<Expression, Expression> MapExpr()
            {
                return jsonElementExpr =>
                {
                    return Assign(
                        actualValueExpr,
                        mapping.Significance switch
                        {
                            MappingSignificance.Required => mapping.ValueKind switch
                            {
                                JsonValueMappingKind.Custom => JsonElementExpr.GetCustom(jsonElementExpr, mapping.JsonReader!),
                                JsonValueMappingKind.String => JsonElementExpr.GetString(jsonElementExpr),
                                JsonValueMappingKind.DateTime => JsonElementExpr.GetDateTime(jsonElementExpr),
                                JsonValueMappingKind.DateTimeOffset => JsonElementExpr.GetDateTimeOffset(jsonElementExpr),
                                JsonValueMappingKind.Guid => JsonElementExpr.GetGuid(jsonElementExpr),
                                JsonValueMappingKind.Boolean => JsonElementExpr.GetBoolean(jsonElementExpr),
                                JsonValueMappingKind.Single => JsonElementExpr.GetSingle(jsonElementExpr),
                                JsonValueMappingKind.Double => JsonElementExpr.GetDouble(jsonElementExpr),
                                JsonValueMappingKind.Decimal => JsonElementExpr.GetDecimal(jsonElementExpr),
                                JsonValueMappingKind.SByte => JsonElementExpr.GetSByte(jsonElementExpr),
                                JsonValueMappingKind.Int16 => JsonElementExpr.GetInt16(jsonElementExpr),
                                JsonValueMappingKind.Int32 => JsonElementExpr.GetInt32(jsonElementExpr),
                                JsonValueMappingKind.Int64 => JsonElementExpr.GetInt64(jsonElementExpr),
                                JsonValueMappingKind.Byte => JsonElementExpr.GetByte(jsonElementExpr),
                                JsonValueMappingKind.UInt16 => JsonElementExpr.GetUInt16(jsonElementExpr),
                                JsonValueMappingKind.UInt32 => JsonElementExpr.GetUInt32(jsonElementExpr),
                                JsonValueMappingKind.UInt64 => JsonElementExpr.GetUInt64(jsonElementExpr),
                                _ => Empty()
                            },
                            MappingSignificance.Optional => mapping.ValueKind switch
                            {
                                JsonValueMappingKind.Custom => JsonElementExpr.GetCustomOrNull(jsonElementExpr, mapping.JsonReader!),
                                JsonValueMappingKind.String => JsonElementExpr.GetStringOrNull(jsonElementExpr),
                                JsonValueMappingKind.DateTime => JsonElementExpr.GetDateTimeOrNull(jsonElementExpr),
                                JsonValueMappingKind.DateTimeOffset => JsonElementExpr.GetDateTimeOffsetOrNull(jsonElementExpr),
                                JsonValueMappingKind.Guid => JsonElementExpr.GetGuidOrNull(jsonElementExpr),
                                JsonValueMappingKind.Boolean => JsonElementExpr.GetBooleanOrNull(jsonElementExpr),
                                JsonValueMappingKind.Single => JsonElementExpr.GetSingleOrNull(jsonElementExpr),
                                JsonValueMappingKind.Double => JsonElementExpr.GetDoubleOrNull(jsonElementExpr),
                                JsonValueMappingKind.Decimal => JsonElementExpr.GetDecimalOrNull(jsonElementExpr),
                                JsonValueMappingKind.SByte => JsonElementExpr.GetSByteOrNull(jsonElementExpr),
                                JsonValueMappingKind.Int16 => JsonElementExpr.GetInt16OrNull(jsonElementExpr),
                                JsonValueMappingKind.Int32 => JsonElementExpr.GetInt32OrNull(jsonElementExpr),
                                JsonValueMappingKind.Int64 => JsonElementExpr.GetInt64OrNull(jsonElementExpr),
                                JsonValueMappingKind.Byte => JsonElementExpr.GetByteOrNull(jsonElementExpr),
                                JsonValueMappingKind.UInt16 => JsonElementExpr.GetUInt16OrNull(jsonElementExpr),
                                JsonValueMappingKind.UInt32 => JsonElementExpr.GetUInt32OrNull(jsonElementExpr),
                                JsonValueMappingKind.UInt64 => JsonElementExpr.GetUInt64OrNull(jsonElementExpr),
                                _ => Empty()
                            },
                            _ => Empty()
                        }
                    );
                };
            }
        }
    }
}
