using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;
using GW2SDK.Impl.Json;
using static System.Linq.Expressions.Expression;

namespace GW2SDK.Impl.JsonReaders
{
    internal static class Expr
    {
        internal static Expression For(ParameterExpression indexExpr, Expression lengthExpr, DoFor bodyExpr)
        {
            var breakTarget = Label();
            var continueTarget = Label();
            return Loop(
                IfThenElse(
                    LessThan(indexExpr, lengthExpr),
                    Block(
                        bodyExpr(breakTarget, continueTarget),
                        PostIncrementAssign(indexExpr)
                    ),
                    Break(breakTarget)
                ),
                breakTarget,
                continueTarget
            );
        }

        internal delegate Expression DoFor(LabelTarget @break, LabelTarget @continue);
    }

    public class JsonObjectReader<TObject> : IJsonReader<TObject>
    {
        private static readonly ConstantExpression TypeNameExpr = Constant(typeof(TObject).Name, typeof(string));
        private readonly List<ReaderInfo> _readers = new List<ReaderInfo>();

        private ReadJson<TObject> _compilation = (in JsonElement json) => default!;

        private bool _needsCompilation = true;

        private Expression<ReadJson<TObject>>? _source;

        private UnexpectedPropertyBehavior _unexpectedPropertyBehavior;

        public UnexpectedPropertyBehavior UnexpectedPropertyBehavior
        {
            get => _unexpectedPropertyBehavior;
            set
            {
                _unexpectedPropertyBehavior = value;
                _needsCompilation = true;
            }
        }

        public TObject Read(in string json) => Read(JsonDocument.Parse(json));

        public TObject Read(in JsonElement value)
        {
            if (_needsCompilation)
            {
                Compile();
            }

            return _compilation(value);
        }

        public bool CanRead(in JsonElement json) => json.ValueKind == JsonValueKind.Object;

        public TObject Read(in JsonDocument value) => Read(value.RootElement);

        public void Ignore(string propertyName)
        {
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Ignored,
                    PropertyName = propertyName,
                    OnMatch = (jsonPropertyExpr, continueLabelExpr) => Block(Continue(continueLabelExpr))
                }
            );
            _needsCompilation = true;
        }

        public Expression AssignArray(Expression arrayExpr, Expression indexExpr, Expression valueExpr) => Assign(ArrayAccess(arrayExpr, indexExpr), valueExpr);

        public void Map(string propertyName, Expression<Func<TObject, IEnumerable<int>>> propertyExpression)
        {
            var propertySeenExpr = Variable(typeof(bool),   $"saw {propertyName}");
            var propertyValueExpr = Variable(typeof(int[]), $"value of {propertyName}");
            var arrayLengthExpr = Variable(typeof(int),     $"length of {propertyName}");
            var indexExpr = Variable(typeof(int),           "i");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Required,
                    PropertyName = propertyName,
                    propertySeenExpr = propertySeenExpr,
                    propertyValueExpr = propertyValueExpr,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (jsonPropertyExpr, _) => Block(
                        new[]
                        {
                            arrayLengthExpr,
                            indexExpr
                        },
                        Assign(propertySeenExpr,  Constant(true)),
                        Assign(indexExpr,         Constant(0)),
                        Assign(arrayLengthExpr,   Call(JsonPropertyExpr.GetValue(jsonPropertyExpr), JsonElementInfo.GetArrayLength)),
                        Assign(propertyValueExpr, NewArrayBounds(typeof(int), arrayLengthExpr)),
                        Expr.For(
                            indexExpr,
                            arrayLengthExpr,
                            (_, __) => AssignArray(
                                propertyValueExpr,
                                indexExpr,
                                JsonElementExpr.GetInt32(MakeIndex(JsonPropertyExpr.GetValue(jsonPropertyExpr), JsonPropertyInfo.Item, new[] { indexExpr }))
                            )
                        )
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(
            string propertyName,
            Expression<Func<TObject, IEnumerable<string>?>> propertyExpression,
            PropertySignificance significance = PropertySignificance.Required)
        {
            var propertySeenExpr = Variable(typeof(bool),      $"saw {propertyName}");
            var propertyValueExpr = Variable(typeof(string[]), $"value of {propertyName}");
            var arrayLengthExpr = Variable(typeof(int),        $"length of {propertyName}");
            var indexExpr = Variable(typeof(int),              "i");
            switch (significance)
            {
                case PropertySignificance.Required:
                    _readers.Add(
                        new ReaderInfo
                        {
                            PropertySignificance = PropertySignificance.Required,
                            PropertyName = propertyName,
                            propertySeenExpr = propertySeenExpr,
                            propertyValueExpr = propertyValueExpr,
                            Destination = ((MemberExpression) propertyExpression.Body).Member,
                            OnMatch = (jsonPropertyExpr, _) => Block(
                                new[]
                                {
                                    arrayLengthExpr,
                                    indexExpr
                                },
                                Assign(propertySeenExpr,  Constant(true)),
                                Assign(indexExpr,         Constant(0)),
                                Assign(arrayLengthExpr,   Call(JsonPropertyExpr.GetValue(jsonPropertyExpr), JsonElementInfo.GetArrayLength)),
                                Assign(propertyValueExpr, NewArrayBounds(typeof(string), arrayLengthExpr)),
                                Expr.For(
                                    indexExpr,
                                    arrayLengthExpr,
                                    (_, __) => AssignArray(
                                        propertyValueExpr,
                                        indexExpr,
                                        JsonElementExpr.GetString(
                                            MakeIndex(JsonPropertyExpr.GetValue(jsonPropertyExpr), JsonPropertyInfo.Item, new[] { indexExpr })
                                        )
                                    )
                                )
                            )
                        }
                    );
                    break;
                case PropertySignificance.Optional:
                    _readers.Add(
                        new ReaderInfo
                        {
                            PropertySignificance = PropertySignificance.Optional,
                            PropertyName = propertyName,
                            propertyValueExpr = propertyValueExpr,
                            Destination = ((MemberExpression) propertyExpression.Body).Member,
                            OnMatch = (jsonPropertyExpr, _) => Block(
                                new[]
                                {
                                    arrayLengthExpr,
                                    indexExpr
                                },
                                Assign(indexExpr,         Constant(0)),
                                Assign(arrayLengthExpr,   Call(JsonPropertyExpr.GetValue(jsonPropertyExpr), JsonElementInfo.GetArrayLength)),
                                Assign(propertyValueExpr, NewArrayBounds(typeof(string), arrayLengthExpr)),
                                Expr.For(
                                    indexExpr,
                                    arrayLengthExpr,
                                    (_, __) => AssignArray(
                                        propertyValueExpr,
                                        indexExpr,
                                        JsonElementExpr.GetString(
                                            MakeIndex(JsonPropertyExpr.GetValue(jsonPropertyExpr), JsonPropertyInfo.Item, new[] { indexExpr })
                                        )
                                    )
                                )
                            )
                        }
                    );
                    break;
                case PropertySignificance.Ignored:
                    Ignore(propertyName);
                    break;
            }

            _needsCompilation = true;
        }

        public void Map<TValue>(
            string propertyName,
            Expression<Func<TObject, IEnumerable<TValue>?>> propertyExpression,
            IJsonReader<TValue> valueReader,
            PropertySignificance significance = PropertySignificance.Required)
        {
            var propertySeenExpr = Variable(typeof(bool),      $"saw {propertyName}");
            var propertyValueExpr = Variable(typeof(TValue[]), $"value of {propertyName}");
            var arrayLengthExpr = Variable(typeof(int),        $"length of {propertyName}");
            var indexExpr = Variable(typeof(int),              "i");
            var readerExpression = Constant(valueReader);
            var readInfo = valueReader.GetType().GetMethod(nameof(Read), new[] { typeof(JsonElement).MakeByRefType() });
            switch (significance)
            {
                case PropertySignificance.Required:
                    _readers.Add(
                        new ReaderInfo
                        {
                            PropertySignificance = PropertySignificance.Required,
                            PropertyName = propertyName,
                            propertySeenExpr = propertySeenExpr,
                            propertyValueExpr = propertyValueExpr,
                            Destination = ((MemberExpression) propertyExpression.Body).Member,
                            OnMatch = (jsonPropertyExpr, _) => Block(
                                new[]
                                {
                                    arrayLengthExpr,
                                    indexExpr
                                },
                                Assign(propertySeenExpr,  Constant(true)),
                                Assign(indexExpr,         Constant(0)),
                                Assign(arrayLengthExpr,   Call(JsonPropertyExpr.GetValue(jsonPropertyExpr), JsonElementInfo.GetArrayLength)),
                                Assign(propertyValueExpr, NewArrayBounds(typeof(TValue), arrayLengthExpr)),
                                Expr.For(
                                    indexExpr,
                                    arrayLengthExpr,
                                    (_, __) => AssignArray(
                                        propertyValueExpr,
                                        indexExpr,
                                        Call(
                                            readerExpression,
                                            readInfo,
                                            MakeIndex(JsonPropertyExpr.GetValue(jsonPropertyExpr), JsonPropertyInfo.Item, new[] { indexExpr })
                                        )
                                    )
                                )
                            )
                        }
                    );
                    break;
                case PropertySignificance.Optional:
                    _readers.Add(
                        new ReaderInfo
                        {
                            PropertySignificance = PropertySignificance.Optional,
                            PropertyName = propertyName,
                            propertyValueExpr = propertyValueExpr,
                            Destination = ((MemberExpression) propertyExpression.Body).Member,
                            OnMatch = (jsonPropertyExpr, _) => Block(
                                new[]
                                {
                                    arrayLengthExpr,
                                    indexExpr
                                },
                                Assign(indexExpr,         Constant(0)),
                                Assign(arrayLengthExpr,   JsonElementExpr.GetArrayLength(JsonPropertyExpr.GetValue(jsonPropertyExpr))),
                                Assign(propertyValueExpr, NewArrayBounds(typeof(TValue), arrayLengthExpr)),
                                Expr.For(
                                    indexExpr,
                                    arrayLengthExpr,
                                    (_, __) => AssignArray(
                                        propertyValueExpr,
                                        indexExpr,
                                        Call(
                                            readerExpression,
                                            readInfo,
                                            MakeIndex(JsonPropertyExpr.GetValue(jsonPropertyExpr), JsonPropertyInfo.Item, new[] { indexExpr })
                                        )
                                    )
                                )
                            )
                        }
                    );
                    break;
                case PropertySignificance.Ignored:
                    Ignore(propertyName);
                    break;
            }

            _needsCompilation = true;
        }

        public void Map<TValue>(
            string propertyName,
            Expression<Func<TObject, TValue>> propertyExpression,
            IJsonReader<TValue> valueReader,
            PropertySignificance significance = PropertySignificance.Required)
        {
            var propertySeenExpr = Variable(typeof(bool),    $"saw {propertyName}");
            var propertyValueExpr = Variable(typeof(TValue), $"value of {propertyName}");
            var readerExpr = Constant(valueReader);
            var readInfo = valueReader.GetType().GetMethod(nameof(Read), new[] { typeof(JsonElement).MakeByRefType() });
            switch (significance)
            {
                case PropertySignificance.Required:
                    _readers.Add(
                        new ReaderInfo
                        {
                            PropertySignificance = PropertySignificance.Required,
                            PropertyName = propertyName,
                            propertySeenExpr = propertySeenExpr,
                            propertyValueExpr = propertyValueExpr,
                            Destination = ((MemberExpression) propertyExpression.Body).Member,
                            OnMatch = (jsonPropertyExpr, _) => Block(
                                Assign(propertySeenExpr, Constant(true)),
                                Assign(
                                    propertyValueExpr,
                                    Call(readerExpr, readInfo, JsonPropertyExpr.GetValue(jsonPropertyExpr))
                                )
                            )
                        }
                    );
                    break;
                case PropertySignificance.Optional:
                    _readers.Add(
                        new ReaderInfo
                        {
                            PropertySignificance = PropertySignificance.Optional,
                            PropertyName = propertyName,
                            propertyValueExpr = propertyValueExpr,
                            Destination = ((MemberExpression) propertyExpression.Body).Member,
                            OnMatch = (jsonPropertyExpr, _) => Block(
                                Assign(
                                    propertyValueExpr,
                                    Call(readerExpr, readInfo, JsonPropertyExpr.GetValue(jsonPropertyExpr))
                                )
                            )
                        }
                    );
                    break;
                default:
                    Ignore(propertyName);
                    break;
            }

            _needsCompilation = true;
        }

        public void Map(
            string propertyName,
            Expression<Func<TObject, string>> propertyExpression,
            PropertySignificance significance = PropertySignificance.Required)
        {
            var propertySeenExpr = Variable(typeof(bool),    $"saw {propertyName}");
            var propertyValueExpr = Variable(typeof(string), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = significance,
                    PropertyName = propertyName,
                    propertySeenExpr = propertySeenExpr,
                    propertyValueExpr = propertyValueExpr,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (jsonPropertyExpr, _) => Block(
                        Assign(propertySeenExpr,  Constant(true)),
                        Assign(propertyValueExpr, JsonElementExpr.GetString(JsonPropertyExpr.GetValue(jsonPropertyExpr)))
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, float>> propertyExpression)
        {
            var propertySeenExpr = Variable(typeof(bool),   $"saw {propertyName}");
            var propertyValueExpr = Variable(typeof(float), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Required,
                    PropertyName = propertyName,
                    propertySeenExpr = propertySeenExpr,
                    propertyValueExpr = propertyValueExpr,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (jsonPropertyExpr, _) => Block(
                        Assign(propertySeenExpr,  Constant(true)),
                        Assign(propertyValueExpr, JsonElementExpr.GetSingle(JsonPropertyExpr.GetValue(jsonPropertyExpr)))
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, float?>> propertyExpression)
        {
            var propertyValueExpr = Variable(typeof(float?), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Optional,
                    PropertyName = propertyName,
                    propertyValueExpr = propertyValueExpr,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (jsonPropertyExpr, _) => Block(
                        Assign(propertyValueExpr, JsonElementExpr.GetSingleOrNull(JsonPropertyExpr.GetValue(jsonPropertyExpr)))
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, double>> propertyExpression)
        {
            var propertySeenExpr = Variable(typeof(bool),    $"saw {propertyName}");
            var propertyValueExpr = Variable(typeof(double), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Required,
                    PropertyName = propertyName,
                    propertySeenExpr = propertySeenExpr,
                    propertyValueExpr = propertyValueExpr,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (jsonPropertyExpr, _) => Block(
                        Assign(propertySeenExpr,  Constant(true)),
                        Assign(propertyValueExpr, JsonElementExpr.GetDouble(JsonPropertyExpr.GetValue(jsonPropertyExpr)))
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, double?>> propertyExpression)
        {
            var propertyValueExpr = Variable(typeof(double?), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Optional,
                    PropertyName = propertyName,
                    propertyValueExpr = propertyValueExpr,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (jsonPropertyExpr, _) => Block(
                        Assign(propertyValueExpr, JsonElementExpr.GetDoubleOrNull(JsonPropertyExpr.GetValue(jsonPropertyExpr)))
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, decimal>> propertyExpression)
        {
            var propertySeenExpr = Variable(typeof(bool),     $"saw {propertyName}");
            var propertyValueExpr = Variable(typeof(decimal), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Required,
                    PropertyName = propertyName,
                    propertySeenExpr = propertySeenExpr,
                    propertyValueExpr = propertyValueExpr,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (jsonPropertyExpr, _) => Block(
                        Assign(propertySeenExpr,  Constant(true)),
                        Assign(propertyValueExpr, JsonElementExpr.GetDecimal(JsonPropertyExpr.GetValue(jsonPropertyExpr)))
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, decimal?>> propertyExpression)
        {
            var propertyValueExpr = Variable(typeof(decimal?), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Optional,
                    PropertyName = propertyName,
                    propertyValueExpr = propertyValueExpr,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (jsonPropertyExpr, _) => Block(
                        Assign(propertyValueExpr, JsonElementExpr.GetDecimalOrNull(JsonPropertyExpr.GetValue(jsonPropertyExpr)))
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, sbyte>> propertyExpression)
        {
            var propertySeenExpr = Variable(typeof(bool),   $"saw {propertyName}");
            var propertyValueExpr = Variable(typeof(sbyte), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Required,
                    PropertyName = propertyName,
                    propertySeenExpr = propertySeenExpr,
                    propertyValueExpr = propertyValueExpr,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (jsonPropertyExpr, _) => Block(
                        Assign(propertySeenExpr,  Constant(true)),
                        Assign(propertyValueExpr, Call(JsonPropertyExpr.GetValue(jsonPropertyExpr), JsonElementInfo.GetSByte))
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, sbyte?>> propertyExpression)
        {
            var propertyValueExpr = Variable(typeof(sbyte?), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Optional,
                    PropertyName = propertyName,
                    propertyValueExpr = propertyValueExpr,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (jsonPropertyExpr, _) => Block(
                        Assign(propertyValueExpr, Call(JsonPropertyExpr.GetValue(jsonPropertyExpr), JsonElementInfo.GetSByte))
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, short>> propertyExpression)
        {
            var propertySeenExpr = Variable(typeof(bool),   $"saw {propertyName}");
            var propertyValueExpr = Variable(typeof(short), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Required,
                    PropertyName = propertyName,
                    propertySeenExpr = propertySeenExpr,
                    propertyValueExpr = propertyValueExpr,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (jsonPropertyExpr, _) => Block(
                        Assign(propertySeenExpr,  Constant(true)),
                        Assign(propertyValueExpr, JsonElementExpr.GetInt16(JsonPropertyExpr.GetValue(jsonPropertyExpr)))
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, short?>> propertyExpression)
        {
            var propertyValueExpr = Variable(typeof(short?), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Optional,
                    PropertyName = propertyName,
                    propertyValueExpr = propertyValueExpr,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (jsonPropertyExpr, _) => Block(
                        Assign(propertyValueExpr, JsonElementExpr.GetInt16OrNull(JsonPropertyExpr.GetValue(jsonPropertyExpr)))
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, int>> propertyExpression)
        {
            var propertySeenExpr = Variable(typeof(bool), $"saw {propertyName}");
            var propertyValueExpr = Variable(typeof(int), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Required,
                    PropertyName = propertyName,
                    propertySeenExpr = propertySeenExpr,
                    propertyValueExpr = propertyValueExpr,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (jsonPropertyExpr, _) => Block(
                        Assign(propertySeenExpr,  Constant(true)),
                        Assign(propertyValueExpr, JsonElementExpr.GetInt32(JsonPropertyExpr.GetValue(jsonPropertyExpr)))
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, int?>> propertyExpression)
        {
            var propertyValueExpr = Variable(typeof(int?), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Optional,
                    PropertyName = propertyName,
                    propertyValueExpr = propertyValueExpr,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (jsonPropertyExpr, _) => Block(
                        Assign(propertyValueExpr, JsonElementExpr.GetInt32OrNull(JsonPropertyExpr.GetValue(jsonPropertyExpr)))
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, long>> propertyExpression)
        {
            var propertySeenExpr = Variable(typeof(bool),  $"saw {propertyName}");
            var propertyValueExpr = Variable(typeof(long), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Required,
                    PropertyName = propertyName,
                    propertySeenExpr = propertySeenExpr,
                    propertyValueExpr = propertyValueExpr,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (jsonPropertyExpr, _) => Block(
                        Assign(propertySeenExpr,  Constant(true)),
                        Assign(propertyValueExpr, JsonElementExpr.GetInt64(JsonPropertyExpr.GetValue(jsonPropertyExpr)))
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, long?>> propertyExpression)
        {
            var propertyValueExpr = Variable(typeof(long?), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Optional,
                    PropertyName = propertyName,
                    propertyValueExpr = propertyValueExpr,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (jsonPropertyExpr, _) => Block(
                        Assign(propertyValueExpr, JsonElementExpr.GetInt64OrNull(JsonPropertyExpr.GetValue(jsonPropertyExpr)))
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, byte>> propertyExpression)
        {
            var propertySeenExpr = Variable(typeof(bool),  $"saw {propertyName}");
            var propertyValueExpr = Variable(typeof(byte), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Required,
                    PropertyName = propertyName,
                    propertySeenExpr = propertySeenExpr,
                    propertyValueExpr = propertyValueExpr,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (jsonPropertyExpr, _) => Block(
                        Assign(propertySeenExpr,  Constant(true)),
                        Assign(propertyValueExpr, JsonElementExpr.GetByte(JsonPropertyExpr.GetValue(jsonPropertyExpr)))
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, byte?>> propertyExpression)
        {
            var propertyValueExpr = Variable(typeof(byte?), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Optional,
                    PropertyName = propertyName,
                    propertyValueExpr = propertyValueExpr,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (jsonPropertyExpr, _) => Block(
                        Assign(propertyValueExpr, JsonElementExpr.GetByteOrNull(JsonPropertyExpr.GetValue(jsonPropertyExpr)))
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, ushort>> propertyExpression)
        {
            var propertySeenExpr = Variable(typeof(bool),    $"saw {propertyName}");
            var propertyValueExpr = Variable(typeof(ushort), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Required,
                    PropertyName = propertyName,
                    propertySeenExpr = propertySeenExpr,
                    propertyValueExpr = propertyValueExpr,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (jsonPropertyExpr, _) => Block(
                        Assign(propertySeenExpr,  Constant(true)),
                        Assign(propertyValueExpr, JsonElementExpr.GetUInt16(JsonPropertyExpr.GetValue(jsonPropertyExpr)))
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, ushort?>> propertyExpression)
        {
            var propertyValueExpr = Variable(typeof(ushort?), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Optional,
                    PropertyName = propertyName,
                    propertyValueExpr = propertyValueExpr,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (jsonPropertyExpr, _) => Block(
                        Assign(propertyValueExpr, JsonElementExpr.GetUInt16OrNull(JsonPropertyExpr.GetValue(jsonPropertyExpr)))
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, uint>> propertyExpression)
        {
            var propertySeenExpr = Variable(typeof(bool),  $"saw {propertyName}");
            var propertyValueExpr = Variable(typeof(uint), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Required,
                    PropertyName = propertyName,
                    propertySeenExpr = propertySeenExpr,
                    propertyValueExpr = propertyValueExpr,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (jsonPropertyExpr, _) => Block(
                        Assign(propertySeenExpr,  Constant(true)),
                        Assign(propertyValueExpr, JsonElementExpr.GetUInt32(JsonPropertyExpr.GetValue(jsonPropertyExpr)))
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, uint?>> propertyExpression)
        {
            var propertyValueExpr = Variable(typeof(uint?), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Optional,
                    PropertyName = propertyName,
                    propertyValueExpr = propertyValueExpr,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (jsonPropertyExpr, _) => Block(
                        Assign(propertyValueExpr, JsonElementExpr.GetUInt32OrNull(JsonPropertyExpr.GetValue(jsonPropertyExpr)))
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, ulong>> propertyExpression)
        {
            var propertySeenExpr = Variable(typeof(bool),   $"saw {propertyName}");
            var propertyValueExpr = Variable(typeof(ulong), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Required,
                    PropertyName = propertyName,
                    propertySeenExpr = propertySeenExpr,
                    propertyValueExpr = propertyValueExpr,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (jsonPropertyExpr, _) => Block(
                        Assign(propertySeenExpr,  Constant(true)),
                        Assign(propertyValueExpr, JsonElementExpr.GetUInt64(JsonPropertyExpr.GetValue(jsonPropertyExpr)))
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, ulong?>> propertyExpression)
        {
            var propertyValueExpr = Variable(typeof(ulong?), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Optional,
                    PropertyName = propertyName,
                    propertyValueExpr = propertyValueExpr,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (jsonPropertyExpr, _) => Block(
                        Assign(propertyValueExpr, JsonElementExpr.GetUInt64OrNull(JsonPropertyExpr.GetValue(jsonPropertyExpr)))
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, bool>> propertyExpression)
        {
            var propertySeenExpr = Variable(typeof(bool),  $"saw {propertyName}");
            var propertyValueExpr = Variable(typeof(bool), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Required,
                    PropertyName = propertyName,
                    propertySeenExpr = propertySeenExpr,
                    propertyValueExpr = propertyValueExpr,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (jsonPropertyExpr, _) => Block(
                        Assign(propertySeenExpr,  Constant(true)),
                        Assign(propertyValueExpr, JsonElementExpr.GetBoolean(JsonPropertyExpr.GetValue(jsonPropertyExpr)))
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, bool?>> propertyExpression)
        {
            var propertyValueExpr = Variable(typeof(bool?), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Optional,
                    PropertyName = propertyName,
                    propertyValueExpr = propertyValueExpr,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (jsonPropertyExpr, _) => Block(
                        Assign(propertyValueExpr, JsonElementExpr.GetBooleanOrNull(JsonPropertyExpr.GetValue(jsonPropertyExpr)))
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, DateTime>> propertyExpression)
        {
            var propertySeenExpr = Variable(typeof(bool),      $"saw {propertyName}");
            var propertyValueExpr = Variable(typeof(DateTime), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Required,
                    PropertyName = propertyName,
                    propertySeenExpr = propertySeenExpr,
                    propertyValueExpr = propertyValueExpr,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (jsonPropertyExpr, _) => Block(
                        Assign(propertySeenExpr,  Constant(true)),
                        Assign(propertyValueExpr, JsonElementExpr.GetDateTime(JsonPropertyExpr.GetValue(jsonPropertyExpr)))
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, DateTime?>> propertyExpression)
        {
            var propertyValueExpr = Variable(typeof(DateTime), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Optional,
                    PropertyName = propertyName,
                    propertyValueExpr = propertyValueExpr,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (jsonPropertyExpr, _) => Block(
                        Assign(propertyValueExpr, JsonElementExpr.GetDateTimeOrNull(JsonPropertyExpr.GetValue(jsonPropertyExpr)))
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, DateTimeOffset>> propertyExpression)
        {
            var propertySeenExpr = Variable(typeof(bool),            $"saw {propertyName}");
            var propertyValueExpr = Variable(typeof(DateTimeOffset), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Required,
                    PropertyName = propertyName,
                    propertySeenExpr = propertySeenExpr,
                    propertyValueExpr = propertyValueExpr,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (jsonPropertyExpr, _) => Block(
                        Assign(propertySeenExpr,  Constant(true)),
                        Assign(propertyValueExpr, JsonElementExpr.GetDateTimeOffset(JsonPropertyExpr.GetValue(jsonPropertyExpr)))
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, DateTimeOffset?>> propertyExpression)
        {
            var propertySeenExpr = Variable(typeof(bool),             $"saw {propertyName}");
            var propertyValueExpr = Variable(typeof(DateTimeOffset?), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Optional,
                    PropertyName = propertyName,
                    propertyValueExpr = propertyValueExpr,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (jsonPropertyExpr, _) => Block(
                        Assign(propertyValueExpr, JsonElementExpr.GetDateTimeOffsetOrNull(JsonPropertyExpr.GetValue(jsonPropertyExpr)))
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, Guid>> propertyExpression)
        {
            var propertySeenExpr = Variable(typeof(bool),  $"saw {propertyName}");
            var propertyValueExpr = Variable(typeof(Guid), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Required,
                    PropertyName = propertyName,
                    propertySeenExpr = propertySeenExpr,
                    propertyValueExpr = propertyValueExpr,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (jsonPropertyExpr, _) => Block(
                        Assign(propertySeenExpr,  Constant(true)),
                        Assign(propertyValueExpr, JsonElementExpr.GetGuid(JsonPropertyExpr.GetValue(jsonPropertyExpr)))
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, Guid?>> propertyExpression)
        {
            var propertyValueExpr = Variable(typeof(Guid?), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Optional,
                    PropertyName = propertyName,
                    propertyValueExpr = propertyValueExpr,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (jsonPropertyExpr, _) => Block(
                        Assign(propertyValueExpr, JsonElementExpr.GetGuidOrNull(JsonPropertyExpr.GetValue(jsonPropertyExpr)))
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Compile()
        {
            var json = Parameter(typeof(JsonElement).MakeByRefType(), "json");

            var vars = _readers.Aggregate(
                Enumerable.Empty<ParameterExpression>(),
                (varBag, reader) =>
                {
                    switch (reader.PropertySignificance)
                    {
                        case PropertySignificance.Optional:
                            return varBag.Append(reader.propertyValueExpr);
                        case PropertySignificance.Required:
                            return varBag.Concat(new[] { reader.propertySeenExpr, reader.propertyValueExpr });
                        default:
                            return varBag;
                    }
                }
            );

            var jsonPropertyExpr = Variable(typeof(JsonProperty), "jsonPropertyExpr");

            var expressions = Enumerable.Empty<Expression>();

            var block = Block(
                typeof(TObject),
                new List<ParameterExpression>(vars),
                expressions
                    .Append(EnsureValueKindIsObject())
                    .Append(
                        ForEachJsonProperty(
                            jsonPropertyExpr,
                            @continue => Read(jsonPropertyExpr, @continue, _readers, 0)
                        )
                    )
                    .Concat(
                        _readers.Where(reader => reader.PropertySignificance == PropertySignificance.Required)
                            .Select(reader => EnsureMemberseen(reader.PropertyName, reader.propertySeenExpr))
                    )
                    .Append(
                        MemberInit(
                            New(typeof(TObject)),
                            from reader in _readers
                            where reader.PropertySignificance != PropertySignificance.Ignored
                            select Bind(reader.Destination, reader.propertyValueExpr)
                        )
                    )
            );

            var source = Lambda<ReadJson<TObject>>(block, json);
            _source = source;
            _compilation = source.Compile();
            _needsCompilation = false;

            Expression Read(ParameterExpression jsonPropertyExpr, LabelTarget @continue, IReadOnlyList<ReaderInfo> readers, int index)
            {
                var reader = readers[index];
                var test = NameEquals(jsonPropertyExpr, Constant(reader.PropertyName, typeof(string)));
                var ifTrue = reader.OnMatch(jsonPropertyExpr, @continue);
                Expression? ifFalse;
                if (index + 1 < readers.Count)
                {
                    ifFalse = Read(jsonPropertyExpr, @continue, readers, index + 1);
                }
                else if (UnexpectedPropertyBehavior == UnexpectedPropertyBehavior.Error)
                {
                    ifFalse = ThrowJsonException(MissingMember(jsonPropertyExpr));
                }
                else
                {
                    ifFalse = default;
                }

                return ifFalse is null ? IfThen(test, ifTrue) : IfThenElse(test, ifTrue, ifFalse);
            }

            Expression MissingMember(ParameterExpression member)
            {
                var format = Constant("JSON property '{0}' was unexpected for type '{1}'.", typeof(string));
                return StringExpr.Format(format, Property(member, JsonPropertyInfo.Name), TypeNameExpr);
            }

            Expression EnsureValueKindIsObject()
            {
                return IfThen(ValueKindNotObject(), ThrowJsonException(Constant("JSON is not an object.", typeof(string))));
            }

            Expression ValueKindNotObject()
            {
                var actual = Property(json, JsonElementInfo.ValueKind);
                var expected = Constant(JsonValueKind.Object);
                return NotEqual(actual, expected);
            }

            Expression ThrowJsonException(Expression message)
            {
                var constructorInfo = JsonExceptionInfo.JsonExceptionConstructor;
                var exception = New(constructorInfo, message);
                return Throw(exception, exception.Type);
            }

            Expression ForEachJsonProperty(ParameterExpression current, Func<LabelTarget, Expression> body)
            {
                var enumerator = Variable(typeof(JsonElement.ObjectEnumerator), "enumerator");
                var breakLabel = Label();
                var continueLabelExpr = Label();
                return Block(
                    new[]
                    {
                        enumerator
                    },
                    Assign(enumerator, GetObjectEnumerator()),
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

            Expression GetCurrent(ParameterExpression enumerator)
            {
                return Property(enumerator, JsonElementInfo.Current);
            }

            MethodCallExpression MoveNext(ParameterExpression enumerator)
            {
                return Call(enumerator, JsonElementInfo.MoveNext);
            }

            MethodCallExpression GetObjectEnumerator()
            {
                return Call(json, JsonElementInfo.EnumerateObject);
            }

            Expression NameEquals(Expression jsonPropertyExpression, Expression textExpression)
            {
                return Call(jsonPropertyExpression, JsonPropertyInfo.NameEquals, textExpression);
            }

            Expression EnsureMemberseen(string propertyName, ParameterExpression check)
            {
                return IfThen(
                    IsFalse(check),
                    Throw(
                        New(
                            JsonExceptionInfo.JsonExceptionConstructor,
                            Constant($"Missing required property '{propertyName}' for object of type '{typeof(TObject).Name}'.")
                        )
                    )
                );
            }
        }

        private delegate TValue ReadJson<out TValue>(in JsonElement json);

        private class ReaderInfo
        {
            public delegate BlockExpression Process(ParameterExpression jsonPropertyExpression, LabelTarget continueLabelExpr);

            public PropertySignificance PropertySignificance { get; set; }

            public string PropertyName { get; set; } = default!;

            public ParameterExpression propertySeenExpr { get; set; } = default!;

            public ParameterExpression propertyValueExpr { get; set; } = default!;

            public MemberInfo Destination { get; set; } = default!;

            public Process OnMatch { get; set; } = default!;
        }
    }

    public enum PropertySignificance
    {
        Required,

        Optional,

        Ignored
    }

    public enum UnexpectedPropertyBehavior
    {
        Error,

        Ignore
    }
}
