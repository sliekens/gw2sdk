using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;
using static System.Linq.Expressions.Expression;

namespace GW2SDK.Impl.JsonReaders
{
    internal static class Expr
    {
        internal delegate Expression DoFor(LabelTarget @break, LabelTarget @continue);

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

        //internal static Expression ForEach(ParameterExpression current, Expression collection, Func<LabelTarget, Expression> body)
        //{
        //    Expression enumerableExpr = default!;
        //    Expression enumeratorExpr;

        //    var enumerator = Variable(typeof(JsonElement.ObjectEnumerator), "enumerator");
        //    var breakLabel = Label();
        //    var continueLabelExpr = Label();
        //    return Block(
        //        new[]
        //        {
        //            enumerator
        //        },
        //        Assign(enumerator, GetObjectEnumerator()),
        //        Loop(
        //            IfThenElse(
        //                MoveNext(enumerator),
        //                Block(
        //                    new[] { current },
        //                    Assign(current, GetCurrent(enumerator)),
        //                    body(continueLabelExpr)
        //                ),
        //                Break(breakLabel)
        //            ),
        //            breakLabel,
        //            continueLabelExpr
        //        )
        //    );

        //    Expression GetCurrent(ParameterExpression enumerator)
        //    {
        //        return Property(enumerator, JsonElementInfo.Current);
        //    }

        //    MethodCallExpression MoveNext(ParameterExpression enumerator)
        //    {
        //        return Call(enumerator, JsonElementInfo.MoveNext);
        //    }
        //}

    }

    public class JsonObjectReader<TObject> : IJsonReader<TObject>
    {
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
                    OnMatch = (jsonPropertyExpr, continueLabelExpr) => Block(
                        new[]
                        {
                            arrayLengthExpr,
                            indexExpr
                        },
                        Assign(propertySeenExpr,  Constant(true)),
                        Assign(indexExpr,         Constant(0)),
                        Assign(arrayLengthExpr,   Call(JsonPropertyValue(jsonPropertyExpr), JsonElementInfo.GetArrayLength)),
                        Assign(propertyValueExpr, NewArrayBounds(typeof(int), arrayLengthExpr)),
                        Expr.For(
                            indexExpr,
                            arrayLengthExpr,
                            (_, __) => AssignArray(
                                propertyValueExpr,
                                indexExpr,
                                Call(MakeIndex(JsonPropertyValue(jsonPropertyExpr), JsonPropertyInfo.Item, new[] { indexExpr }), JsonElementInfo.GetInt32)
                            )
                        ),
                        Continue(continueLabelExpr)
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
                            OnMatch = (jsonPropertyExpr, continueLabelExpr) => Block(
                                new[]
                                {
                                    arrayLengthExpr,
                                    indexExpr
                                },
                                Assign(propertySeenExpr,  Constant(true)),
                                Assign(indexExpr,         Constant(0)),
                                Assign(arrayLengthExpr,   Call(JsonPropertyValue(jsonPropertyExpr), JsonElementInfo.GetArrayLength)),
                                Assign(propertyValueExpr, NewArrayBounds(typeof(string), arrayLengthExpr)),
                                Expr.For(
                                    indexExpr,
                                    arrayLengthExpr,
                                    (_, __) => AssignArray(
                                        propertyValueExpr,
                                        indexExpr,
                                        Call(MakeIndex(JsonPropertyValue(jsonPropertyExpr), JsonPropertyInfo.Item, new[] { indexExpr }), JsonElementInfo.GetString)
                                    )
                                ),
                                Continue(continueLabelExpr)
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
                            OnMatch = (jsonPropertyExpr, continueLabelExpr) => Block(
                                new[]
                                {
                                    arrayLengthExpr,
                                    indexExpr
                                },
                                Assign(indexExpr,         Constant(0)),
                                Assign(arrayLengthExpr,   Call(JsonPropertyValue(jsonPropertyExpr), JsonElementInfo.GetArrayLength)),
                                Assign(propertyValueExpr, NewArrayBounds(typeof(string), arrayLengthExpr)),
                                Expr.For(
                                    indexExpr,
                                    arrayLengthExpr,
                                    (_, __) => AssignArray(
                                        propertyValueExpr,
                                        indexExpr,
                                        Call(MakeIndex(JsonPropertyValue(jsonPropertyExpr), JsonPropertyInfo.Item, new[] { indexExpr }), JsonElementInfo.GetString)
                                    )
                                ),
                                Continue(continueLabelExpr)
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
                            OnMatch = (jsonPropertyExpr, continueLabelExpr) => Block(
                                new[]
                                {
                                    arrayLengthExpr,
                                    indexExpr
                                },
                                Assign(propertySeenExpr,  Constant(true)),
                                Assign(indexExpr,         Constant(0)),
                                Assign(arrayLengthExpr,   Call(JsonPropertyValue(jsonPropertyExpr), JsonElementInfo.GetArrayLength)),
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
                                            MakeIndex(JsonPropertyValue(jsonPropertyExpr), JsonPropertyInfo.Item, new[] { indexExpr })
                                        )
                                    )
                                ),
                                Continue(continueLabelExpr)
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
                            OnMatch = (jsonPropertyExpr, continueLabelExpr) => Block(
                                new[]
                                {
                                    arrayLengthExpr,
                                    indexExpr
                                },
                                Assign(indexExpr,         Constant(0)),
                                Assign(arrayLengthExpr,   Call(JsonPropertyValue(jsonPropertyExpr), JsonElementInfo.GetArrayLength)),
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
                                            MakeIndex(JsonPropertyValue(jsonPropertyExpr), JsonPropertyInfo.Item, new[] { indexExpr })
                                        )
                                    )
                                ),
                                Continue(continueLabelExpr)
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

        private static MemberExpression JsonPropertyValue(ParameterExpression jsonPropertyExpr) => Property(jsonPropertyExpr, JsonPropertyInfo.Value);

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
                            OnMatch = (jsonPropertyExpr, continueLabelExpr) => Block(
                                Assign(propertySeenExpr, Constant(true)),
                                Assign(
                                    propertyValueExpr,
                                    Call(readerExpr, readInfo, Property(jsonPropertyExpr, JsonPropertyInfo.Value))
                                ),
                                Continue(continueLabelExpr)
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
                            OnMatch = (jsonPropertyExpr, continueLabelExpr) => Block(
                                Assign(
                                    propertyValueExpr,
                                    Call(readerExpr, readInfo, Property(jsonPropertyExpr, JsonPropertyInfo.Value))
                                ),
                                Continue(continueLabelExpr)
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
                    OnMatch = (jsonPropertyExpr, continueLabelExpr) => Block(
                        Assign(propertySeenExpr,  Constant(true)),
                        Assign(propertyValueExpr, Call(Property(jsonPropertyExpr, JsonPropertyInfo.Value), JsonElementInfo.GetString)),
                        Continue(continueLabelExpr)
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
                    OnMatch = (jsonPropertyExpr, continueLabelExpr) => Block(
                        Assign(propertySeenExpr,  Constant(true)),
                        Assign(propertyValueExpr, Call(Property(jsonPropertyExpr, JsonPropertyInfo.Value), JsonElementInfo.GetSingle)),
                        Continue(continueLabelExpr)
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
                    OnMatch = (jsonPropertyExpr, continueLabelExpr) => Block(
                        Assign(propertyValueExpr, Call(Property(jsonPropertyExpr, JsonPropertyInfo.Value), JsonElementInfo.GetSingle)),
                        Continue(continueLabelExpr)
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
                    OnMatch = (jsonPropertyExpr, continueLabelExpr) => Block(
                        Assign(propertySeenExpr,  Constant(true)),
                        Assign(propertyValueExpr, Call(Property(jsonPropertyExpr, JsonPropertyInfo.Value), JsonElementInfo.GetDouble)),
                        Continue(continueLabelExpr)
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
                    OnMatch = (jsonPropertyExpr, continueLabelExpr) => Block(
                        Assign(propertyValueExpr, Call(Property(jsonPropertyExpr, JsonPropertyInfo.Value), JsonElementInfo.GetDouble)),
                        Continue(continueLabelExpr)
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
                    OnMatch = (jsonPropertyExpr, continueLabelExpr) => Block(
                        Assign(propertySeenExpr,  Constant(true)),
                        Assign(propertyValueExpr, Call(Property(jsonPropertyExpr, JsonPropertyInfo.Value), JsonElementInfo.GetDecimal)),
                        Continue(continueLabelExpr)
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
                    OnMatch = (jsonPropertyExpr, continueLabelExpr) => Block(
                        Assign(propertyValueExpr, Call(Property(jsonPropertyExpr, JsonPropertyInfo.Value), JsonElementInfo.GetDecimal)),
                        Continue(continueLabelExpr)
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
                    OnMatch = (jsonPropertyExpr, continueLabelExpr) => Block(
                        Assign(propertySeenExpr,  Constant(true)),
                        Assign(propertyValueExpr, Call(Property(jsonPropertyExpr, JsonPropertyInfo.Value), JsonElementInfo.GetSByte)),
                        Continue(continueLabelExpr)
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
                    OnMatch = (jsonPropertyExpr, continueLabelExpr) => Block(
                        Assign(propertyValueExpr, Call(Property(jsonPropertyExpr, JsonPropertyInfo.Value), JsonElementInfo.GetSByte)),
                        Continue(continueLabelExpr)
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
                    OnMatch = (jsonPropertyExpr, continueLabelExpr) => Block(
                        Assign(propertySeenExpr,  Constant(true)),
                        Assign(propertyValueExpr, Call(Property(jsonPropertyExpr, JsonPropertyInfo.Value), JsonElementInfo.GetInt16)),
                        Continue(continueLabelExpr)
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
                    OnMatch = (jsonPropertyExpr, continueLabelExpr) => Block(
                        Assign(propertyValueExpr, Call(Property(jsonPropertyExpr, JsonPropertyInfo.Value), JsonElementInfo.GetInt16)),
                        Continue(continueLabelExpr)
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
                    OnMatch = (jsonPropertyExpr, continueLabelExpr) => Block(
                        Assign(propertySeenExpr,  Constant(true)),
                        Assign(propertyValueExpr, Call(Property(jsonPropertyExpr, JsonPropertyInfo.Value), JsonElementInfo.GetInt32)),
                        Continue(continueLabelExpr)
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
                    OnMatch = (jsonPropertyExpr, continueLabelExpr) => Block(
                        Assign(propertyValueExpr, Convert(Call(Property(jsonPropertyExpr, JsonPropertyInfo.Value), JsonElementInfo.GetInt32), propertyValueExpr.Type)),
                        Continue(continueLabelExpr)
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
                    OnMatch = (jsonPropertyExpr, continueLabelExpr) => Block(
                        Assign(propertySeenExpr,  Constant(true)),
                        Assign(propertyValueExpr, Call(Property(jsonPropertyExpr, JsonPropertyInfo.Value), JsonElementInfo.GetInt64)),
                        Continue(continueLabelExpr)
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
                    OnMatch = (jsonPropertyExpr, continueLabelExpr) => Block(
                        Assign(propertyValueExpr, Call(Property(jsonPropertyExpr, JsonPropertyInfo.Value), JsonElementInfo.GetInt64)),
                        Continue(continueLabelExpr)
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
                    OnMatch = (jsonPropertyExpr, continueLabelExpr) => Block(
                        Assign(propertySeenExpr,  Constant(true)),
                        Assign(propertyValueExpr, Call(Property(jsonPropertyExpr, JsonPropertyInfo.Value), JsonElementInfo.GetByte)),
                        Continue(continueLabelExpr)
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
                    OnMatch = (jsonPropertyExpr, continueLabelExpr) => Block(
                        Assign(propertyValueExpr, Call(Property(jsonPropertyExpr, JsonPropertyInfo.Value), JsonElementInfo.GetByte)),
                        Continue(continueLabelExpr)
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
                    OnMatch = (jsonPropertyExpr, continueLabelExpr) => Block(
                        Assign(propertySeenExpr,  Constant(true)),
                        Assign(propertyValueExpr, Call(Property(jsonPropertyExpr, JsonPropertyInfo.Value), JsonElementInfo.GetUInt16)),
                        Continue(continueLabelExpr)
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
                    OnMatch = (jsonPropertyExpr, continueLabelExpr) => Block(
                        Assign(propertyValueExpr, Call(Property(jsonPropertyExpr, JsonPropertyInfo.Value), JsonElementInfo.GetUInt16)),
                        Continue(continueLabelExpr)
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
                    OnMatch = (jsonPropertyExpr, continueLabelExpr) => Block(
                        Assign(propertySeenExpr,  Constant(true)),
                        Assign(propertyValueExpr, Call(Property(jsonPropertyExpr, JsonPropertyInfo.Value), JsonElementInfo.GetUInt32)),
                        Continue(continueLabelExpr)
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
                    OnMatch = (jsonPropertyExpr, continueLabelExpr) => Block(
                        Assign(propertyValueExpr, Call(Property(jsonPropertyExpr, JsonPropertyInfo.Value), JsonElementInfo.GetUInt32)),
                        Continue(continueLabelExpr)
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
                    OnMatch = (jsonPropertyExpr, continueLabelExpr) => Block(
                        Assign(propertySeenExpr,  Constant(true)),
                        Assign(propertyValueExpr, Call(Property(jsonPropertyExpr, JsonPropertyInfo.Value), JsonElementInfo.GetUInt64)),
                        Continue(continueLabelExpr)
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
                    OnMatch = (jsonPropertyExpr, continueLabelExpr) => Block(
                        Assign(propertySeenExpr,  Constant(true)),
                        Assign(propertyValueExpr, Call(Property(jsonPropertyExpr, JsonPropertyInfo.Value), JsonElementInfo.GetBoolean)),
                        Continue(continueLabelExpr)
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
                    OnMatch = (jsonPropertyExpr, continueLabelExpr) => Block(
                        Assign(propertySeenExpr,  Constant(true)),
                        Assign(propertyValueExpr, Call(Property(jsonPropertyExpr, JsonPropertyInfo.Value), JsonElementInfo.GetDateTime)),
                        Continue(continueLabelExpr)
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
                    OnMatch = (jsonPropertyExpr, continueLabelExpr) => Block(
                        Assign(propertySeenExpr,  Constant(true)),
                        Assign(propertyValueExpr, Call(Property(jsonPropertyExpr, JsonPropertyInfo.Value), JsonElementInfo.GetDateTimeOffset)),
                        Continue(continueLabelExpr)
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
                    OnMatch = (jsonPropertyExpr, continueLabelExpr) => Block(
                        Assign(propertySeenExpr,  Constant(true)),
                        Assign(propertyValueExpr, Call(Property(jsonPropertyExpr, JsonPropertyInfo.Value), JsonElementInfo.GetGuid)),
                        Continue(continueLabelExpr)
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
                            continueLabelExpr => Block(
                                ((Func<IEnumerable<Expression>>) (() =>
                                {
                                    var memberHandling = new List<Expression>(_readers.Count + 1);
                                    foreach (var reader in _readers)
                                    {
                                        memberHandling.Add(
                                            IfThen(
                                                NameEquals(jsonPropertyExpr, Constant(reader.PropertyName, typeof(string))),
                                                reader.OnMatch(jsonPropertyExpr, continueLabelExpr)
                                            )
                                        );
                                    }

                                    if (UnexpectedPropertyBehavior == UnexpectedPropertyBehavior.Error)
                                    {
                                        memberHandling.Add(ThrowJsonException(MissingMember(jsonPropertyExpr)));
                                    }

                                    return memberHandling;
                                }))()
                            )
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

            Expression MissingMember(ParameterExpression member)
            {
                var format = typeof(string).GetMethod(nameof(string.Format), new[] { typeof(string), typeof(object) });
                var template = Constant("JSON property '{0}' was unexpected for type '" + typeof(TObject).Name + "'.", typeof(string));
                return Call(null, format, template, Property(member, JsonPropertyInfo.Name));
            }

            Expression EnsureValueKindIsObject()
            {
                return IfThen(ValueKindNotObject(), ThrowJsonException(Constant("JSON is not an object.", typeof(string))));
            }

            Expression ValueKindNotObject()
            {
                var actual = Property(json, JsonPropertyInfo.ValueKind);
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

        private static class JsonExceptionInfo
        {
            public static readonly ConstructorInfo JsonExceptionConstructor = typeof(JsonException).GetConstructor(new[] { typeof(string) });
        }

        private static class JsonPropertyInfo
        {
            public static readonly PropertyInfo Name = typeof(JsonProperty).GetProperty(nameof(JsonProperty.Name));
            public static readonly PropertyInfo Value = typeof(JsonProperty).GetProperty(nameof(JsonProperty.Value));
            public static readonly MethodInfo NameEquals = typeof(JsonProperty).GetMethod(nameof(JsonProperty.NameEquals), new[] { typeof(string) });
            public static readonly PropertyInfo ValueKind = typeof(JsonElement).GetProperty(nameof(JsonElement.ValueKind));
            public static readonly PropertyInfo Item = typeof(JsonElement).GetProperty("Item");
        }

        private static class JsonElementInfo
        {
            public static readonly MethodInfo GetString = typeof(JsonElement).GetMethod(nameof(JsonElement.GetString));

            public static readonly MethodInfo GetDateTime = typeof(JsonElement).GetMethod(nameof(JsonElement.GetDateTime));

            public static readonly MethodInfo GetDateTimeOffset = typeof(JsonElement).GetMethod(nameof(JsonElement.GetDateTimeOffset));

            public static readonly MethodInfo GetBoolean = typeof(JsonElement).GetMethod(nameof(JsonElement.GetBoolean));

            public static readonly MethodInfo GetGuid = typeof(JsonElement).GetMethod(nameof(JsonElement.GetGuid));

            public static readonly MethodInfo GetDouble = typeof(JsonElement).GetMethod(nameof(JsonElement.GetDouble));

            public static readonly MethodInfo GetSingle = typeof(JsonElement).GetMethod(nameof(JsonElement.GetSingle));

            public static readonly MethodInfo GetByte = typeof(JsonElement).GetMethod(nameof(JsonElement.GetByte));

            public static readonly MethodInfo GetDecimal = typeof(JsonElement).GetMethod(nameof(JsonElement.GetDecimal));

            public static readonly MethodInfo GetInt16 = typeof(JsonElement).GetMethod(nameof(JsonElement.GetInt16));

            public static readonly MethodInfo GetInt32 = typeof(JsonElement).GetMethod(nameof(JsonElement.GetInt32));

            public static readonly MethodInfo GetInt64 = typeof(JsonElement).GetMethod(nameof(JsonElement.GetInt64));

            public static readonly MethodInfo GetSByte = typeof(JsonElement).GetMethod(nameof(JsonElement.GetSByte));

            public static readonly MethodInfo GetUInt16 = typeof(JsonElement).GetMethod(nameof(JsonElement.GetUInt16));

            public static readonly MethodInfo GetUInt32 = typeof(JsonElement).GetMethod(nameof(JsonElement.GetUInt32));

            public static readonly MethodInfo GetUInt64 = typeof(JsonElement).GetMethod(nameof(JsonElement.GetUInt64));

            public static readonly MethodInfo GetArrayLength = typeof(JsonElement).GetMethod(nameof(JsonElement.GetArrayLength));

            public static readonly MethodInfo EnumerateObject = typeof(JsonElement).GetMethod(nameof(JsonElement.EnumerateObject));

            public static readonly PropertyInfo Current = typeof(JsonElement.ObjectEnumerator).GetProperty(nameof(JsonElement.ObjectEnumerator.Current));

            public static readonly MethodInfo MoveNext = typeof(JsonElement.ObjectEnumerator).GetMethod(nameof(JsonElement.ObjectEnumerator.MoveNext));
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
