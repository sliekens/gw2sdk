using System;
using System.Linq.Expressions;
using GW2SDK.Impl.JsonReaders.Mappings;
using GW2SDK.Impl.JsonReaders.Nodes;
using static System.Linq.Expressions.Expression;
using static GW2SDK.Impl.Json.JsonElementExpr;

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

            Func<Expression, Expression, Expression> MapExpr()
            {
                return (jsonElementExpr, jsonPathExpr) =>
                {
                    return Assign(
                        actualValueExpr,
                        mapping.Significance switch
                        {
                            MappingSignificance.Required => mapping.ValueKind switch
                            {
                                JsonValueMappingKind.Custom => GetCustom(jsonElementExpr, jsonPathExpr, mapping.JsonReader!),
                                JsonValueMappingKind.String => GetString(jsonElementExpr, jsonPathExpr),
                                JsonValueMappingKind.DateTime => GetDateTime(jsonElementExpr, jsonPathExpr),
                                JsonValueMappingKind.DateTimeOffset => GetDateTimeOffset(jsonElementExpr, jsonPathExpr),
                                JsonValueMappingKind.Guid => GetGuid(jsonElementExpr, jsonPathExpr),
                                JsonValueMappingKind.Boolean => GetBoolean(jsonElementExpr, jsonPathExpr),
                                JsonValueMappingKind.Single => GetSingle(jsonElementExpr, jsonPathExpr),
                                JsonValueMappingKind.Double => GetDouble(jsonElementExpr, jsonPathExpr),
                                JsonValueMappingKind.Decimal => GetDecimal(jsonElementExpr, jsonPathExpr),
                                JsonValueMappingKind.SByte => GetSByte(jsonElementExpr, jsonPathExpr),
                                JsonValueMappingKind.Int16 => GetInt16(jsonElementExpr, jsonPathExpr),
                                JsonValueMappingKind.Int32 => GetInt32(jsonElementExpr, jsonPathExpr),
                                JsonValueMappingKind.Int64 => GetInt64(jsonElementExpr, jsonPathExpr),
                                JsonValueMappingKind.Byte => GetByte(jsonElementExpr, jsonPathExpr),
                                JsonValueMappingKind.UInt16 => GetUInt16(jsonElementExpr, jsonPathExpr),
                                JsonValueMappingKind.UInt32 => GetUInt32(jsonElementExpr, jsonPathExpr),
                                JsonValueMappingKind.UInt64 => GetUInt64(jsonElementExpr, jsonPathExpr),
                                _ => Empty()
                            },
                            MappingSignificance.Optional => mapping.ValueKind switch
                            {
                                JsonValueMappingKind.Custom => GetCustomOrNull(jsonElementExpr, jsonPathExpr, mapping.JsonReader!),
                                JsonValueMappingKind.String => GetStringOrNull(jsonElementExpr, jsonPathExpr),
                                JsonValueMappingKind.DateTime => GetDateTimeOrNull(jsonElementExpr, jsonPathExpr),
                                JsonValueMappingKind.DateTimeOffset => GetDateTimeOffsetOrNull(jsonElementExpr, jsonPathExpr),
                                JsonValueMappingKind.Guid => GetGuidOrNull(jsonElementExpr, jsonPathExpr),
                                JsonValueMappingKind.Boolean => GetBooleanOrNull(jsonElementExpr, jsonPathExpr),
                                JsonValueMappingKind.Single => GetSingleOrNull(jsonElementExpr, jsonPathExpr),
                                JsonValueMappingKind.Double => GetDoubleOrNull(jsonElementExpr, jsonPathExpr),
                                JsonValueMappingKind.Decimal => GetDecimalOrNull(jsonElementExpr, jsonPathExpr),
                                JsonValueMappingKind.SByte => GetSByteOrNull(jsonElementExpr, jsonPathExpr),
                                JsonValueMappingKind.Int16 => GetInt16OrNull(jsonElementExpr, jsonPathExpr),
                                JsonValueMappingKind.Int32 => GetInt32OrNull(jsonElementExpr, jsonPathExpr),
                                JsonValueMappingKind.Int64 => GetInt64OrNull(jsonElementExpr, jsonPathExpr),
                                JsonValueMappingKind.Byte => GetByteOrNull(jsonElementExpr, jsonPathExpr),
                                JsonValueMappingKind.UInt16 => GetUInt16OrNull(jsonElementExpr, jsonPathExpr),
                                JsonValueMappingKind.UInt32 => GetUInt32OrNull(jsonElementExpr, jsonPathExpr),
                                JsonValueMappingKind.UInt64 => GetUInt64OrNull(jsonElementExpr, jsonPathExpr),
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
