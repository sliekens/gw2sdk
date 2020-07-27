using System;
using System.Linq.Expressions;
using System.Text.Json;
using static System.Linq.Expressions.Expression;
using static GW2SDK.Impl.Json.ExpressionDebug;

namespace GW2SDK.Impl.Json
{
    internal static class JsonElementExpr
    {
        internal static MethodCallExpression GetRawText(Expression jsonElementExpr)
        {
            AssertType<JsonElement>(jsonElementExpr);
            return Call(jsonElementExpr, JsonElementInfo.GetRawText);
        }

        internal static Expression GetValueKind(Expression jsonElementExpr)
        {
            AssertType<JsonElement>(jsonElementExpr);
            return Property(jsonElementExpr, JsonElementInfo.ValueKind);
        }

        internal static Expression GetArrayLength(Expression jsonElementExpr)
        {
            AssertType<JsonElement>(jsonElementExpr);
            return Call(jsonElementExpr, JsonElementInfo.GetArrayLength);
        }

        internal static Expression EnumerateObject(Expression jsonElementExpr)
        {
            AssertType<JsonElement>(jsonElementExpr);
            return Call(jsonElementExpr, JsonElementInfo.EnumerateObject);
        }
        

        internal static Expression GetCurrent(ParameterExpression objectEnumeratorExpr)
        {
            AssertType<JsonElement.ObjectEnumerator>(objectEnumeratorExpr);
            return Property(objectEnumeratorExpr, JsonElementInfo.Current);
        }

        internal static MethodCallExpression MoveNext(ParameterExpression objectEnumeratorExpr)
        {
            AssertType<JsonElement.ObjectEnumerator>(objectEnumeratorExpr);
            return Call(objectEnumeratorExpr, JsonElementInfo.MoveNext);
        }

        internal static Expression ForEachProperty(Expression jsonElementExpr, ParameterExpression current, Func<LabelTarget, Expression> body)
        {
            AssertType<JsonElement>(jsonElementExpr);
            AssertType<JsonProperty>(current);
            var enumerator = Variable(typeof(JsonElement.ObjectEnumerator), "enumerator");
            var breakLabel = Label();
            var continueLabelExpr = Label();
            return Block(
                new[]
                {
                    enumerator
                },
                Assign(enumerator, EnumerateObject(jsonElementExpr)),
                Loop(
                    IfThenElse(
                        MoveNext(enumerator),
                        Block(
                            new[] { current },
                            Assign(current, GetCurrent(enumerator)),
                            body(continueLabelExpr)
                        ),
                        Break(breakLabel)
                    ),
                    breakLabel,
                    continueLabelExpr
                )
            );
        }


        /// <summary>
        ///     An expression that returns the value of a JsonElement as a string, or throws JsonException.
        /// </summary>
        internal static Expression GetString(Expression jsonElementExpr)
        {
            AssertType<JsonElement>(jsonElementExpr);
            var valueExpr = Variable(typeof(string));
            var valueKindExpr = GetValueKind(jsonElementExpr);
            var expectedValueKindExpr = Constant(JsonValueKind.String, typeof(JsonValueKind));
            return Block(
                new[]
                {
                    valueExpr
                },
                IfThenElse(
                    Not(Equal(valueKindExpr, expectedValueKindExpr)),
                    JsonExceptionExpr.ThrowJsonException(
                        StringExpr.Format(
                            Constant("Expected JSON value of type String, found {0}: '{1}'.", typeof(string)),
                            GetValueKind(jsonElementExpr),
                            GetRawText(jsonElementExpr)
                        )
                    ),
                    Assign(valueExpr, Call(jsonElementExpr, JsonElementInfo.GetString))
                ),
                valueExpr
            );
        }

        internal static Expression GetStringOrNull(Expression jsonElementExpr) => GetValueOrNull<string>(jsonElementExpr, GetString(jsonElementExpr));

        /// <summary>
        ///     An expression that returns the value of a JsonElement as a DateTime, or throws JsonException.
        /// </summary>
        internal static Expression GetDateTime(Expression jsonElementExpr)
        {
            AssertType<JsonElement>(jsonElementExpr);
            var valueExpr = Variable(typeof(DateTime));
            var valueKindExpr = GetValueKind(jsonElementExpr);
            var expectedValueKindExpr = Constant(JsonValueKind.String, typeof(JsonValueKind));
            return Block(
                new[]
                {
                    valueExpr
                },
                IfThen(
                    Not(
                        AndAlso(
                            Equal(valueKindExpr, expectedValueKindExpr),
                            Call(jsonElementExpr, JsonElementInfo.TryGetDateTime, valueExpr)
                        )
                    ),
                    JsonExceptionExpr.ThrowJsonException(
                        StringExpr.Format(
                            Constant("Expected JSON value of type DateTime, found {0}: '{1}'.", typeof(string)),
                            GetValueKind(jsonElementExpr),
                            GetRawText(jsonElementExpr)
                        )
                    )
                ),
                valueExpr
            );
        }

        internal static Expression GetDateTimeOrNull(Expression jsonElementExpr) => GetValueOrNull<DateTime?>(jsonElementExpr, GetDateTime(jsonElementExpr));

        /// <summary>
        ///     An expression that returns the value of a JsonElement as a DateTimeOffset, or throws JsonException.
        /// </summary>
        internal static Expression GetDateTimeOffset(Expression jsonElementExpr)
        {
            AssertType<JsonElement>(jsonElementExpr);
            var valueExpr = Variable(typeof(DateTimeOffset));
            var valueKindExpr = GetValueKind(jsonElementExpr);
            var expectedValueKindExpr = Constant(JsonValueKind.String, typeof(JsonValueKind));
            return Block(
                new[]
                {
                    valueExpr
                },
                IfThen(
                    Not(
                        AndAlso(
                            Equal(valueKindExpr, expectedValueKindExpr),
                            Call(jsonElementExpr, JsonElementInfo.TryGetDateTimeOffset, valueExpr)
                        )
                    ),
                    JsonExceptionExpr.ThrowJsonException(
                        StringExpr.Format(
                            Constant("Expected JSON value of type DateTimeOffset, found {0}: '{1}'.", typeof(string)),
                            GetValueKind(jsonElementExpr),
                            GetRawText(jsonElementExpr)
                        )
                    )
                ),
                valueExpr
            );
        }

        internal static Expression GetDateTimeOffsetOrNull(Expression jsonElementExpr) =>
            GetValueOrNull<DateTimeOffset?>(jsonElementExpr, GetDateTimeOffset(jsonElementExpr));

        /// <summary>
        ///     An expression that returns the value of a JsonElement as a Guid, or throws JsonException.
        /// </summary>
        internal static Expression GetGuid(Expression jsonElementExpr)
        {
            AssertType<JsonElement>(jsonElementExpr);
            var valueExpr = Variable(typeof(Guid));
            var valueKindExpr = GetValueKind(jsonElementExpr);
            var expectedValueKindExpr = Constant(JsonValueKind.String, typeof(JsonValueKind));
            return Block(
                new[]
                {
                    valueExpr
                },
                IfThen(
                    Not(
                        AndAlso(
                            Equal(valueKindExpr, expectedValueKindExpr),
                            Call(jsonElementExpr, JsonElementInfo.TryGetGuid, valueExpr)
                        )
                    ),
                    JsonExceptionExpr.ThrowJsonException(
                        StringExpr.Format(
                            Constant("Expected JSON value of type Guid, found {0}: '{1}'.", typeof(string)),
                            GetValueKind(jsonElementExpr),
                            GetRawText(jsonElementExpr)
                        )
                    )
                ),
                valueExpr
            );
        }

        internal static Expression GetGuidOrNull(Expression jsonElementExpr) => GetValueOrNull<Guid?>(jsonElementExpr, GetGuid(jsonElementExpr));

        /// <summary>
        ///     An expression that returns the value of a JsonElement as a Boolean, or throws JsonException.
        /// </summary>
        internal static Expression GetBoolean(Expression jsonElementExpr)
        {
            AssertType<JsonElement>(jsonElementExpr);
            var valueExpr = Variable(typeof(bool));
            var valueKindExpr = GetValueKind(jsonElementExpr);
            return Block(
                new[]
                {
                    valueExpr
                },
                Switch(
                    valueKindExpr,
                    Block(
                        JsonExceptionExpr.ThrowJsonException(
                            StringExpr.Format(
                                Constant("Expected JSON value of type Boolean, found {0}: '{1}'.", typeof(string)),
                                GetValueKind(jsonElementExpr),
                                GetRawText(jsonElementExpr)
                            )
                        ),
                        Assign(valueExpr, Constant(false, typeof(bool)))
                    ),
                    SwitchCase(
                        Block(Assign(valueExpr, Constant(true, typeof(bool)))),
                        Constant(JsonValueKind.True, typeof(JsonValueKind))
                    ),
                    SwitchCase(
                        Block(Assign(valueExpr, Constant(false, typeof(bool)))),
                        Constant(JsonValueKind.False, typeof(JsonValueKind))
                    )
                ),
                valueExpr
            );
        }

        internal static Expression GetBooleanOrNull(Expression jsonElementExpr) => GetValueOrNull<bool?>(jsonElementExpr, GetBoolean(jsonElementExpr));

        /// <summary>
        ///     An expression that returns the value of a JsonElement as a Single, or throws JsonException.
        /// </summary>
        internal static Expression GetSingle(Expression jsonElementExpr)
        {
            AssertType<JsonElement>(jsonElementExpr);
            var valueExpr = Variable(typeof(float));
            var valueKindExpr = GetValueKind(jsonElementExpr);
            var expectedValueKindExpr = Constant(JsonValueKind.Number, typeof(JsonValueKind));
            return Block(
                new[]
                {
                    valueExpr
                },
                IfThen(
                    Not(
                        AndAlso(
                            Equal(valueKindExpr, expectedValueKindExpr),
                            Call(jsonElementExpr, JsonElementInfo.TryGetSingle, valueExpr)
                        )
                    ),
                    JsonExceptionExpr.ThrowJsonException(
                        StringExpr.Format(
                            Constant("Expected JSON value of type Single, found {0}: '{1}'.", typeof(string)),
                            GetValueKind(jsonElementExpr),
                            GetRawText(jsonElementExpr)
                        )
                    )
                ),
                valueExpr
            );
        }

        internal static Expression GetSingleOrNull(Expression jsonElementExpr) => GetValueOrNull<float?>(jsonElementExpr, GetSingle(jsonElementExpr));

        /// <summary>
        ///     An expression that returns the value of a JsonElement as a Double, or throws JsonException.
        /// </summary>
        internal static Expression GetDouble(Expression jsonElementExpr)
        {
            AssertType<JsonElement>(jsonElementExpr);
            var valueExpr = Variable(typeof(double));
            var valueKindExpr = GetValueKind(jsonElementExpr);
            var expectedValueKindExpr = Constant(JsonValueKind.Number, typeof(JsonValueKind));
            return Block(
                new[]
                {
                    valueExpr
                },
                IfThen(
                    Not(
                        AndAlso(
                            Equal(valueKindExpr, expectedValueKindExpr),
                            Call(jsonElementExpr, JsonElementInfo.TryGetDouble, valueExpr)
                        )
                    ),
                    JsonExceptionExpr.ThrowJsonException(
                        StringExpr.Format(
                            Constant("Expected JSON value of type Double, found {0}: '{1}'.", typeof(string)),
                            GetValueKind(jsonElementExpr),
                            GetRawText(jsonElementExpr)
                        )
                    )
                ),
                valueExpr
            );
        }

        internal static Expression GetDoubleOrNull(Expression jsonElementExpr) => GetValueOrNull<double?>(jsonElementExpr, GetDouble(jsonElementExpr));

        /// <summary>
        ///     An expression that returns the value of a JsonElement as a Decimal, or throws JsonException.
        /// </summary>
        internal static Expression GetDecimal(Expression jsonElementExpr)
        {
            AssertType<JsonElement>(jsonElementExpr);
            var valueExpr = Variable(typeof(decimal));
            var valueKindExpr = GetValueKind(jsonElementExpr);
            var expectedValueKindExpr = Constant(JsonValueKind.Number, typeof(JsonValueKind));
            return Block(
                new[]
                {
                    valueExpr
                },
                IfThen(
                    Not(
                        AndAlso(
                            Equal(valueKindExpr, expectedValueKindExpr),
                            Call(jsonElementExpr, JsonElementInfo.TryGetDecimal, valueExpr)
                        )
                    ),
                    JsonExceptionExpr.ThrowJsonException(
                        StringExpr.Format(
                            Constant("Expected JSON value of type Decimal, found {0}: '{1}'.", typeof(string)),
                            GetValueKind(jsonElementExpr),
                            GetRawText(jsonElementExpr)
                        )
                    )
                ),
                valueExpr
            );
        }

        internal static Expression GetDecimalOrNull(Expression jsonElementExpr) => GetValueOrNull<decimal?>(jsonElementExpr, GetDecimal(jsonElementExpr));

        /// <summary>
        ///     An expression that returns the value of a JsonElement as an GetSByte, or throws JsonException.
        /// </summary>
        internal static Expression GetSByte(Expression jsonElementExpr)
        {
            AssertType<JsonElement>(jsonElementExpr);
            var valueExpr = Variable(typeof(sbyte));
            var valueKindExpr = GetValueKind(jsonElementExpr);
            var expectedValueKindExpr = Constant(JsonValueKind.Number, typeof(JsonValueKind));
            return Block(
                new[]
                {
                    valueExpr
                },
                IfThen(
                    Not(
                        AndAlso(
                            Equal(valueKindExpr, expectedValueKindExpr),
                            Call(jsonElementExpr, JsonElementInfo.TryGetSByte, valueExpr)
                        )
                    ),
                    JsonExceptionExpr.ThrowJsonException(
                        StringExpr.Format(
                            Constant("Expected JSON value of type SByte, found {0}: '{1}'.", typeof(string)),
                            GetValueKind(jsonElementExpr),
                            GetRawText(jsonElementExpr)
                        )
                    )
                ),
                valueExpr
            );
        }

        internal static Expression GetSByteOrNull(Expression jsonElementExpr) => GetValueOrNull<sbyte?>(jsonElementExpr, GetSByte(jsonElementExpr));


        /// <summary>
        ///     An expression that returns the value of a JsonElement as an Int32, or throws JsonException.
        /// </summary>
        internal static Expression GetInt16(Expression jsonElementExpr)
        {
            AssertType<JsonElement>(jsonElementExpr);
            var valueExpr = Variable(typeof(short));
            var valueKindExpr = GetValueKind(jsonElementExpr);
            var expectedValueKindExpr = Constant(JsonValueKind.Number, typeof(JsonValueKind));
            return Block(
                new[]
                {
                    valueExpr
                },
                IfThen(
                    Not(
                        AndAlso(
                            Equal(valueKindExpr, expectedValueKindExpr),
                            Call(jsonElementExpr, JsonElementInfo.TryGetInt16, valueExpr)
                        )
                    ),
                    JsonExceptionExpr.ThrowJsonException(
                        StringExpr.Format(
                            Constant("Expected JSON value of type Int16, found {0}: '{1}'.", typeof(string)),
                            GetValueKind(jsonElementExpr),
                            GetRawText(jsonElementExpr)
                        )
                    )
                ),
                valueExpr
            );
        }

        internal static Expression GetInt16OrNull(Expression jsonElementExpr) => GetValueOrNull<short?>(jsonElementExpr, GetInt16(jsonElementExpr));

        /// <summary>
        ///     An expression that returns the value of a JsonElement as an Int32, or throws JsonException.
        /// </summary>
        internal static Expression GetInt32(Expression jsonElementExpr)
        {
            AssertType<JsonElement>(jsonElementExpr);
            var valueExpr = Variable(typeof(int));
            var valueKindExpr = GetValueKind(jsonElementExpr);
            var expectedValueKindExpr = Constant(JsonValueKind.Number, typeof(JsonValueKind));
            return Block(
                new[]
                {
                    valueExpr
                },
                IfThen(
                    Not(
                        AndAlso(
                            Equal(valueKindExpr, expectedValueKindExpr),
                            Call(jsonElementExpr, JsonElementInfo.TryGetInt32, valueExpr)
                        )
                    ),
                    JsonExceptionExpr.ThrowJsonException(
                        StringExpr.Format(
                            Constant("Expected JSON value of type Int32, found {0}: '{1}'.", typeof(string)),
                            GetValueKind(jsonElementExpr),
                            GetRawText(jsonElementExpr)
                        )
                    )
                ),
                valueExpr
            );
        }

        internal static Expression GetInt32OrNull(Expression jsonElementExpr) => GetValueOrNull<int?>(jsonElementExpr, GetInt32(jsonElementExpr));
        
        /// <summary>
        ///     An expression that returns the value of a JsonElement as an Int64, or throws JsonException.
        /// </summary>
        internal static Expression GetInt64(Expression jsonElementExpr)
        {
            AssertType<JsonElement>(jsonElementExpr);
            var valueExpr = Variable(typeof(long));
            var valueKindExpr = GetValueKind(jsonElementExpr);
            var expectedValueKindExpr = Constant(JsonValueKind.Number, typeof(JsonValueKind));
            return Block(
                new[]
                {
                    valueExpr
                },
                IfThen(
                    Not(
                        AndAlso(
                            Equal(valueKindExpr, expectedValueKindExpr),
                            Call(jsonElementExpr, JsonElementInfo.TryGetInt64, valueExpr)
                        )
                    ),
                    JsonExceptionExpr.ThrowJsonException(
                        StringExpr.Format(
                            Constant("Expected JSON value of type Int64, found {0}: '{1}'.", typeof(string)),
                            GetValueKind(jsonElementExpr),
                            GetRawText(jsonElementExpr)
                        )
                    )
                ),
                valueExpr
            );
        }

        internal static Expression GetInt64OrNull(Expression jsonElementExpr) => GetValueOrNull<long?>(jsonElementExpr, GetInt64(jsonElementExpr));

        /// <summary>
        ///     An expression that returns the value of a JsonElement as a Byte, or throws JsonException.
        /// </summary>
        internal static Expression GetByte(Expression jsonElementExpr)
        {
            AssertType<JsonElement>(jsonElementExpr);
            var valueExpr = Variable(typeof(byte));
            var valueKindExpr = GetValueKind(jsonElementExpr);
            var expectedValueKindExpr = Constant(JsonValueKind.Number, typeof(JsonValueKind));
            return Block(
                new[]
                {
                    valueExpr
                },
                IfThen(
                    Not(
                        AndAlso(
                            Equal(valueKindExpr, expectedValueKindExpr),
                            Call(jsonElementExpr, JsonElementInfo.TryGetByte, valueExpr)
                        )
                    ),
                    JsonExceptionExpr.ThrowJsonException(
                        StringExpr.Format(
                            Constant("Expected JSON value of type Byte, found {0}: '{1}'.", typeof(string)),
                            GetValueKind(jsonElementExpr),
                            GetRawText(jsonElementExpr)
                        )
                    )
                ),
                valueExpr
            );
        }

        internal static Expression GetByteOrNull(Expression jsonElementExpr) => GetValueOrNull<byte?>(jsonElementExpr, GetByte(jsonElementExpr));
        
        /// <summary>
        ///     An expression that returns the value of a JsonElement as an UInt16, or throws JsonException.
        /// </summary>
        internal static Expression GetUInt16(Expression jsonElementExpr)
        {
            AssertType<JsonElement>(jsonElementExpr);
            var valueExpr = Variable(typeof(ushort));
            var valueKindExpr = GetValueKind(jsonElementExpr);
            var expectedValueKindExpr = Constant(JsonValueKind.Number, typeof(JsonValueKind));
            return Block(
                new[]
                {
                    valueExpr
                },
                IfThen(
                    Not(
                        AndAlso(
                            Equal(valueKindExpr, expectedValueKindExpr),
                            Call(jsonElementExpr, JsonElementInfo.TryGetUInt16, valueExpr)
                        )
                    ),
                    JsonExceptionExpr.ThrowJsonException(
                        StringExpr.Format(
                            Constant("Expected JSON value of type UInt16, found {0}: '{1}'.", typeof(string)),
                            GetValueKind(jsonElementExpr),
                            GetRawText(jsonElementExpr)
                        )
                    )
                ),
                valueExpr
            );
        }

        internal static Expression GetUInt16OrNull(Expression jsonElementExpr) => GetValueOrNull<ushort?>(jsonElementExpr, GetUInt16(jsonElementExpr));
        
        /// <summary>
        ///     An expression that returns the value of a JsonElement as an UInt32, or throws JsonException.
        /// </summary>
        internal static Expression GetUInt32(Expression jsonElementExpr)
        {
            AssertType<JsonElement>(jsonElementExpr);
            var valueExpr = Variable(typeof(uint));
            var valueKindExpr = GetValueKind(jsonElementExpr);
            var expectedValueKindExpr = Constant(JsonValueKind.Number, typeof(JsonValueKind));
            return Block(
                new[]
                {
                    valueExpr
                },
                IfThen(
                    Not(
                        AndAlso(
                            Equal(valueKindExpr, expectedValueKindExpr),
                            Call(jsonElementExpr, JsonElementInfo.TryGetUInt32, valueExpr)
                        )
                    ),
                    JsonExceptionExpr.ThrowJsonException(
                        StringExpr.Format(
                            Constant("Expected JSON value of type UInt32, found {0}: '{1}'.", typeof(string)),
                            GetValueKind(jsonElementExpr),
                            GetRawText(jsonElementExpr)
                        )
                    )
                ),
                valueExpr
            );
        }

        internal static Expression GetUInt32OrNull(Expression jsonElementExpr) => GetValueOrNull<uint?>(jsonElementExpr, GetUInt32(jsonElementExpr));

        /// <summary>
        ///     An expression that returns the value of a JsonElement as an UInt64, or throws JsonException.
        /// </summary>
        internal static Expression GetUInt64(Expression jsonElementExpr)
        {
            AssertType<JsonElement>(jsonElementExpr);
            var valueExpr = Variable(typeof(ulong));
            var valueKindExpr = GetValueKind(jsonElementExpr);
            var expectedValueKindExpr = Constant(JsonValueKind.Number, typeof(JsonValueKind));
            return Block(
                new[]
                {
                    valueExpr
                },
                IfThen(
                    Not(
                        AndAlso(
                            Equal(valueKindExpr, expectedValueKindExpr),
                            Call(jsonElementExpr, JsonElementInfo.TryGetUInt64, valueExpr)
                        )
                    ),
                    JsonExceptionExpr.ThrowJsonException(
                        StringExpr.Format(
                            Constant("Expected JSON value of type UInt64, found {0}: '{1}'.", typeof(string)),
                            GetValueKind(jsonElementExpr),
                            GetRawText(jsonElementExpr)
                        )
                    )
                ),
                valueExpr
            );
        }

        internal static Expression GetUInt64OrNull(Expression jsonElementExpr) => GetValueOrNull<ulong?>(jsonElementExpr, GetUInt64(jsonElementExpr));


        private static Expression GetValueOrNull<TValue>(Expression jsonElementExpr, Expression ifNotNull)
        {
            AssertType<JsonElement>(jsonElementExpr);
            AssignableTo<TValue>(ifNotNull);
            var valueKindExpr = GetValueKind(jsonElementExpr);
            var expectedValueKindExpr = Constant(JsonValueKind.Null, typeof(JsonValueKind));
            return Condition(
                Equal(valueKindExpr, expectedValueKindExpr),
                Constant(null, typeof(TValue)),
                Convert(ifNotNull, typeof(TValue))
            );
        }
    }
}
