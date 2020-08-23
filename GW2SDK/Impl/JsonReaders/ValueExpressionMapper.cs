using System.Linq.Expressions;
using System.Text.Json;
using GW2SDK.Impl.Json;
using GW2SDK.Impl.JsonReaders.Mappings;
using static System.Linq.Expressions.Expression;

namespace GW2SDK.Impl.JsonReaders
{
    public class ValueExpressionMapper<TValue> : IValueExpressionMapper
    {
        private readonly JsonValueMapping<TValue> _mapping;

        public ValueExpressionMapper(JsonValueMapping<TValue> mapping)
        {
            _mapping = mapping;
        }

        public Expression MapValueExpr(
            Expression jsonElementExpr,
            Expression jsonPathExpr)
        {
            ExpressionDebug.AssertType(typeof(JsonElement), jsonElementExpr);
            ExpressionDebug.AssertType(typeof(JsonPath),    jsonPathExpr);
            return _mapping.Significance switch
            {
                MappingSignificance.Required => _mapping.ValueKind switch
                {
                    JsonValueMappingKind.Any => JsonElementExpr.GetCustom(jsonElementExpr, jsonPathExpr, _mapping.JsonReader!),
                    JsonValueMappingKind.String => JsonElementExpr.GetString(jsonElementExpr, jsonPathExpr),
                    JsonValueMappingKind.DateTime => JsonElementExpr.GetDateTime(jsonElementExpr, jsonPathExpr),
                    JsonValueMappingKind.DateTimeOffset => JsonElementExpr.GetDateTimeOffset(jsonElementExpr, jsonPathExpr),
                    JsonValueMappingKind.Guid => JsonElementExpr.GetGuid(jsonElementExpr, jsonPathExpr),
                    JsonValueMappingKind.Boolean => JsonElementExpr.GetBoolean(jsonElementExpr, jsonPathExpr),
                    JsonValueMappingKind.Single => JsonElementExpr.GetSingle(jsonElementExpr, jsonPathExpr),
                    JsonValueMappingKind.Double => JsonElementExpr.GetDouble(jsonElementExpr, jsonPathExpr),
                    JsonValueMappingKind.Decimal => JsonElementExpr.GetDecimal(jsonElementExpr, jsonPathExpr),
                    JsonValueMappingKind.SByte => JsonElementExpr.GetSByte(jsonElementExpr, jsonPathExpr),
                    JsonValueMappingKind.Int16 => JsonElementExpr.GetInt16(jsonElementExpr, jsonPathExpr),
                    JsonValueMappingKind.Int32 => JsonElementExpr.GetInt32(jsonElementExpr, jsonPathExpr),
                    JsonValueMappingKind.Int64 => JsonElementExpr.GetInt64(jsonElementExpr, jsonPathExpr),
                    JsonValueMappingKind.Byte => JsonElementExpr.GetByte(jsonElementExpr, jsonPathExpr),
                    JsonValueMappingKind.UInt16 => JsonElementExpr.GetUInt16(jsonElementExpr, jsonPathExpr),
                    JsonValueMappingKind.UInt32 => JsonElementExpr.GetUInt32(jsonElementExpr, jsonPathExpr),
                    JsonValueMappingKind.UInt64 => JsonElementExpr.GetUInt64(jsonElementExpr, jsonPathExpr),
                    _ => Empty()
                },
                MappingSignificance.Optional => _mapping.ValueKind switch
                {
                    JsonValueMappingKind.Any => JsonElementExpr.GetCustomOrNull(jsonElementExpr, jsonPathExpr, _mapping.JsonReader!),
                    JsonValueMappingKind.String => JsonElementExpr.GetStringOrNull(jsonElementExpr, jsonPathExpr),
                    JsonValueMappingKind.DateTime => JsonElementExpr.GetDateTimeOrNull(jsonElementExpr, jsonPathExpr),
                    JsonValueMappingKind.DateTimeOffset => JsonElementExpr.GetDateTimeOffsetOrNull(jsonElementExpr, jsonPathExpr),
                    JsonValueMappingKind.Guid => JsonElementExpr.GetGuidOrNull(jsonElementExpr, jsonPathExpr),
                    JsonValueMappingKind.Boolean => JsonElementExpr.GetBooleanOrNull(jsonElementExpr, jsonPathExpr),
                    JsonValueMappingKind.Single => JsonElementExpr.GetSingleOrNull(jsonElementExpr, jsonPathExpr),
                    JsonValueMappingKind.Double => JsonElementExpr.GetDoubleOrNull(jsonElementExpr, jsonPathExpr),
                    JsonValueMappingKind.Decimal => JsonElementExpr.GetDecimalOrNull(jsonElementExpr, jsonPathExpr),
                    JsonValueMappingKind.SByte => JsonElementExpr.GetSByteOrNull(jsonElementExpr, jsonPathExpr),
                    JsonValueMappingKind.Int16 => JsonElementExpr.GetInt16OrNull(jsonElementExpr, jsonPathExpr),
                    JsonValueMappingKind.Int32 => JsonElementExpr.GetInt32OrNull(jsonElementExpr, jsonPathExpr),
                    JsonValueMappingKind.Int64 => JsonElementExpr.GetInt64OrNull(jsonElementExpr, jsonPathExpr),
                    JsonValueMappingKind.Byte => JsonElementExpr.GetByteOrNull(jsonElementExpr, jsonPathExpr),
                    JsonValueMappingKind.UInt16 => JsonElementExpr.GetUInt16OrNull(jsonElementExpr, jsonPathExpr),
                    JsonValueMappingKind.UInt32 => JsonElementExpr.GetUInt32OrNull(jsonElementExpr, jsonPathExpr),
                    JsonValueMappingKind.UInt64 => JsonElementExpr.GetUInt64OrNull(jsonElementExpr, jsonPathExpr),
                    _ => Empty()
                },
                _ => Empty()
            };
        }
    }
}
