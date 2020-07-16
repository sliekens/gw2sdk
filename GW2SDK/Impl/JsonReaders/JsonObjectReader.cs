using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;
using static System.Linq.Expressions.Expression;

namespace GW2SDK.Impl.JsonReaders
{
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

        public TObject Read(in JsonDocument value) => Read(value.RootElement);

        public void Ignore(string propertyName)
        {
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Ignored,
                    PropertyName = propertyName,
                    OnMatch = (currentMember, continueLabel) => Block(Continue(continueLabel))
                }
            );
            _needsCompilation = true;
        }

        private static Expression For(ParameterExpression indexExpr, Expression lengthExpr, Expression body)
        {
            var breakLabelExpr = Label();
            var continueLabelExpr = Label();
            return Loop(
                IfThenElse(
                    LessThan(indexExpr, lengthExpr),
                    Block(
                        body,
                        PostIncrementAssign(indexExpr),
                        Continue(continueLabelExpr)
                    ),
                    Break(breakLabelExpr)
                ),
                breakLabelExpr,
                continueLabelExpr
            );
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
                    PropertySeen = propertySeenExpr,
                    PropertyValue = propertyValueExpr,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (currentMember, continueLabelExpr) => Block(
                        new[]
                        {
                            arrayLengthExpr,
                            indexExpr
                        },
                        Assign(propertySeenExpr,  Constant(true)),
                        Assign(indexExpr,         Constant(0)),
                        Assign(arrayLengthExpr,   Call(JsonPropertyValue(currentMember), JsonElementInfo.GetArrayLength)),
                        Assign(propertyValueExpr, NewArrayBounds(typeof(int), arrayLengthExpr)),
                        For(
                            indexExpr,
                            arrayLengthExpr,
                            AssignArray(
                                propertyValueExpr,
                                indexExpr,
                                Call(MakeIndex(JsonPropertyValue(currentMember), JsonPropertyInfo.Item, new[] { indexExpr }), JsonElementInfo.GetInt32)
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
                            PropertySeen = propertySeenExpr,
                            PropertyValue = propertyValueExpr,
                            Destination = ((MemberExpression) propertyExpression.Body).Member,
                            OnMatch = (currentMember, continueLabelExpr) => Block(
                                new[]
                                {
                                    arrayLengthExpr,
                                    indexExpr
                                },
                                Assign(propertySeenExpr,  Constant(true)),
                                Assign(indexExpr,         Constant(0)),
                                Assign(arrayLengthExpr,   Call(JsonPropertyValue(currentMember), JsonElementInfo.GetArrayLength)),
                                Assign(propertyValueExpr, NewArrayBounds(typeof(string), arrayLengthExpr)),
                                For(
                                    indexExpr,
                                    arrayLengthExpr,
                                    AssignArray(
                                        propertyValueExpr,
                                        indexExpr,
                                        Call(MakeIndex(JsonPropertyValue(currentMember), JsonPropertyInfo.Item, new[] { indexExpr }), JsonElementInfo.GetString)
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
                            PropertyValue = propertyValueExpr,
                            Destination = ((MemberExpression) propertyExpression.Body).Member,
                            OnMatch = (currentMember, continueLabelExpr) => Block(
                                new[]
                                {
                                    arrayLengthExpr,
                                    indexExpr
                                },
                                Assign(indexExpr,         Constant(0)),
                                Assign(arrayLengthExpr,   Call(JsonPropertyValue(currentMember), JsonElementInfo.GetArrayLength)),
                                Assign(propertyValueExpr, NewArrayBounds(typeof(string), arrayLengthExpr)),
                                For(
                                    indexExpr,
                                    arrayLengthExpr,
                                    AssignArray(
                                        propertyValueExpr,
                                        indexExpr,
                                        Call(MakeIndex(JsonPropertyValue(currentMember), JsonPropertyInfo.Item, new[] { indexExpr }), JsonElementInfo.GetString)
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
                            PropertySeen = propertySeenExpr,
                            PropertyValue = propertyValueExpr,
                            Destination = ((MemberExpression) propertyExpression.Body).Member,
                            OnMatch = (currentMember, continueLabelExpr) => Block(
                                new[]
                                {
                                    arrayLengthExpr,
                                    indexExpr
                                },
                                Assign(propertySeenExpr,  Constant(true)),
                                Assign(indexExpr,         Constant(0)),
                                Assign(arrayLengthExpr,   Call(JsonPropertyValue(currentMember), JsonElementInfo.GetArrayLength)),
                                Assign(propertyValueExpr, NewArrayBounds(typeof(TValue), arrayLengthExpr)),
                                For(
                                    indexExpr,
                                    arrayLengthExpr,
                                    AssignArray(
                                        propertyValueExpr,
                                        indexExpr,
                                        Call(
                                            readerExpression,
                                            readInfo,
                                            MakeIndex(JsonPropertyValue(currentMember), JsonPropertyInfo.Item, new[] { indexExpr })
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
                            PropertyValue = propertyValueExpr,
                            Destination = ((MemberExpression) propertyExpression.Body).Member,
                            OnMatch = (currentMember, continueLabelExpr) => Block(
                                new[]
                                {
                                    arrayLengthExpr,
                                    indexExpr
                                },
                                Assign(indexExpr,         Constant(0)),
                                Assign(arrayLengthExpr,   Call(JsonPropertyValue(currentMember), JsonElementInfo.GetArrayLength)),
                                Assign(propertyValueExpr, NewArrayBounds(typeof(TValue), arrayLengthExpr)),
                                For(
                                    indexExpr,
                                    arrayLengthExpr,
                                    AssignArray(
                                        propertyValueExpr,
                                        indexExpr,
                                        Call(
                                            readerExpression,
                                            readInfo,
                                            MakeIndex(JsonPropertyValue(currentMember), JsonPropertyInfo.Item, new[] { indexExpr })
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

        private static MemberExpression JsonPropertyValue(ParameterExpression currentMember) => Property(currentMember, JsonPropertyInfo.Value);

        public void Map<TValue>(string propertyName, Expression<Func<TObject, TValue>> propertyExpression, IJsonReader<TValue> valueReader)
        {
            var propertySeen = Variable(typeof(bool),    $"saw {propertyName}");
            var propertyValue = Variable(typeof(TValue), $"value of {propertyName}");
            var readerExpression = Constant(valueReader);
            var readInfo = valueReader.GetType().GetMethod(nameof(Read), new[] { typeof(JsonElement).MakeByRefType() });
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Required,
                    PropertyName = propertyName,
                    PropertySeen = propertySeen,
                    PropertyValue = propertyValue,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (currentMember, continueLabel) => Block(
                        Assign(propertySeen, Constant(true)),
                        Assign(
                            propertyValue,
                            Call(readerExpression, readInfo, Property(currentMember, JsonPropertyInfo.Value))
                        ),
                        Continue(continueLabel)
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(
            string propertyName,
            Expression<Func<TObject, string>> propertyExpression,
            PropertySignificance significance = PropertySignificance.Required)
        {
            var propertySeen = Variable(typeof(bool),    $"saw {propertyName}");
            var propertyValue = Variable(typeof(string), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = significance,
                    PropertyName = propertyName,
                    PropertySeen = propertySeen,
                    PropertyValue = propertyValue,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (currentMember, continueLabel) => Block(
                        Assign(propertySeen,  Constant(true)),
                        Assign(propertyValue, Call(Property(currentMember, JsonPropertyInfo.Value), JsonElementInfo.GetString)),
                        Continue(continueLabel)
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, float>> propertyExpression)
        {
            var propertySeen = Variable(typeof(bool),   $"saw {propertyName}");
            var propertyValue = Variable(typeof(float), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Required,
                    PropertyName = propertyName,
                    PropertySeen = propertySeen,
                    PropertyValue = propertyValue,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (currentMember, continueLabel) => Block(
                        Assign(propertySeen,  Constant(true)),
                        Assign(propertyValue, Call(Property(currentMember, JsonPropertyInfo.Value), JsonElementInfo.GetSingle)),
                        Continue(continueLabel)
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, float?>> propertyExpression)
        {
            var propertyValue = Variable(typeof(float?), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Optional,
                    PropertyName = propertyName,
                    PropertyValue = propertyValue,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (currentMember, continueLabel) => Block(
                        Assign(propertyValue, Call(Property(currentMember, JsonPropertyInfo.Value), JsonElementInfo.GetSingle)),
                        Continue(continueLabel)
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, double>> propertyExpression)
        {
            var propertySeen = Variable(typeof(bool),    $"saw {propertyName}");
            var propertyValue = Variable(typeof(double), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Required,
                    PropertyName = propertyName,
                    PropertySeen = propertySeen,
                    PropertyValue = propertyValue,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (currentMember, continueLabel) => Block(
                        Assign(propertySeen,  Constant(true)),
                        Assign(propertyValue, Call(Property(currentMember, JsonPropertyInfo.Value), JsonElementInfo.GetDouble)),
                        Continue(continueLabel)
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, double?>> propertyExpression)
        {
            var propertyValue = Variable(typeof(double?), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Optional,
                    PropertyName = propertyName,
                    PropertyValue = propertyValue,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (currentMember, continueLabel) => Block(
                        Assign(propertyValue, Call(Property(currentMember, JsonPropertyInfo.Value), JsonElementInfo.GetDouble)),
                        Continue(continueLabel)
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, decimal>> propertyExpression)
        {
            var propertySeen = Variable(typeof(bool),     $"saw {propertyName}");
            var propertyValue = Variable(typeof(decimal), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Required,
                    PropertyName = propertyName,
                    PropertySeen = propertySeen,
                    PropertyValue = propertyValue,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (currentMember, continueLabel) => Block(
                        Assign(propertySeen,  Constant(true)),
                        Assign(propertyValue, Call(Property(currentMember, JsonPropertyInfo.Value), JsonElementInfo.GetDecimal)),
                        Continue(continueLabel)
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, decimal?>> propertyExpression)
        {
            var propertyValue = Variable(typeof(decimal?), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Optional,
                    PropertyName = propertyName,
                    PropertyValue = propertyValue,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (currentMember, continueLabel) => Block(
                        Assign(propertyValue, Call(Property(currentMember, JsonPropertyInfo.Value), JsonElementInfo.GetDecimal)),
                        Continue(continueLabel)
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, sbyte>> propertyExpression)
        {
            var propertySeen = Variable(typeof(bool),   $"saw {propertyName}");
            var propertyValue = Variable(typeof(sbyte), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Required,
                    PropertyName = propertyName,
                    PropertySeen = propertySeen,
                    PropertyValue = propertyValue,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (currentMember, continueLabel) => Block(
                        Assign(propertySeen,  Constant(true)),
                        Assign(propertyValue, Call(Property(currentMember, JsonPropertyInfo.Value), JsonElementInfo.GetSByte)),
                        Continue(continueLabel)
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, sbyte?>> propertyExpression)
        {
            var propertyValue = Variable(typeof(sbyte?), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Optional,
                    PropertyName = propertyName,
                    PropertyValue = propertyValue,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (currentMember, continueLabel) => Block(
                        Assign(propertyValue, Call(Property(currentMember, JsonPropertyInfo.Value), JsonElementInfo.GetSByte)),
                        Continue(continueLabel)
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, short>> propertyExpression)
        {
            var propertySeen = Variable(typeof(bool),   $"saw {propertyName}");
            var propertyValue = Variable(typeof(short), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Required,
                    PropertyName = propertyName,
                    PropertySeen = propertySeen,
                    PropertyValue = propertyValue,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (currentMember, continueLabel) => Block(
                        Assign(propertySeen,  Constant(true)),
                        Assign(propertyValue, Call(Property(currentMember, JsonPropertyInfo.Value), JsonElementInfo.GetInt16)),
                        Continue(continueLabel)
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, short?>> propertyExpression)
        {
            var propertyValue = Variable(typeof(short?), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Optional,
                    PropertyName = propertyName,
                    PropertyValue = propertyValue,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (currentMember, continueLabel) => Block(
                        Assign(propertyValue, Call(Property(currentMember, JsonPropertyInfo.Value), JsonElementInfo.GetInt16)),
                        Continue(continueLabel)
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, int>> propertyExpression)
        {
            var propertySeen = Variable(typeof(bool), $"saw {propertyName}");
            var propertyValue = Variable(typeof(int), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Required,
                    PropertyName = propertyName,
                    PropertySeen = propertySeen,
                    PropertyValue = propertyValue,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (currentMember, continueLabel) => Block(
                        Assign(propertySeen,  Constant(true)),
                        Assign(propertyValue, Call(Property(currentMember, JsonPropertyInfo.Value), JsonElementInfo.GetInt32)),
                        Continue(continueLabel)
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, int?>> propertyExpression)
        {
            var propertyValue = Variable(typeof(int?), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Optional,
                    PropertyName = propertyName,
                    PropertyValue = propertyValue,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (currentMember, continueLabel) => Block(
                        Assign(propertyValue, Convert(Call(Property(currentMember, JsonPropertyInfo.Value), JsonElementInfo.GetInt32), propertyValue.Type)),
                        Continue(continueLabel)
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, long>> propertyExpression)
        {
            var propertySeen = Variable(typeof(bool),  $"saw {propertyName}");
            var propertyValue = Variable(typeof(long), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Required,
                    PropertyName = propertyName,
                    PropertySeen = propertySeen,
                    PropertyValue = propertyValue,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (currentMember, continueLabel) => Block(
                        Assign(propertySeen,  Constant(true)),
                        Assign(propertyValue, Call(Property(currentMember, JsonPropertyInfo.Value), JsonElementInfo.GetInt64)),
                        Continue(continueLabel)
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, long?>> propertyExpression)
        {
            var propertyValue = Variable(typeof(long?), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Optional,
                    PropertyName = propertyName,
                    PropertyValue = propertyValue,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (currentMember, continueLabel) => Block(
                        Assign(propertyValue, Call(Property(currentMember, JsonPropertyInfo.Value), JsonElementInfo.GetInt64)),
                        Continue(continueLabel)
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, byte>> propertyExpression)
        {
            var propertySeen = Variable(typeof(bool),  $"saw {propertyName}");
            var propertyValue = Variable(typeof(byte), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Required,
                    PropertyName = propertyName,
                    PropertySeen = propertySeen,
                    PropertyValue = propertyValue,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (currentMember, continueLabel) => Block(
                        Assign(propertySeen,  Constant(true)),
                        Assign(propertyValue, Call(Property(currentMember, JsonPropertyInfo.Value), JsonElementInfo.GetByte)),
                        Continue(continueLabel)
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, byte?>> propertyExpression)
        {
            var propertyValue = Variable(typeof(byte?), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Optional,
                    PropertyName = propertyName,
                    PropertyValue = propertyValue,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (currentMember, continueLabel) => Block(
                        Assign(propertyValue, Call(Property(currentMember, JsonPropertyInfo.Value), JsonElementInfo.GetByte)),
                        Continue(continueLabel)
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, ushort>> propertyExpression)
        {
            var propertySeen = Variable(typeof(bool),    $"saw {propertyName}");
            var propertyValue = Variable(typeof(ushort), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Required,
                    PropertyName = propertyName,
                    PropertySeen = propertySeen,
                    PropertyValue = propertyValue,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (currentMember, continueLabel) => Block(
                        Assign(propertySeen,  Constant(true)),
                        Assign(propertyValue, Call(Property(currentMember, JsonPropertyInfo.Value), JsonElementInfo.GetUInt16)),
                        Continue(continueLabel)
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, ushort?>> propertyExpression)
        {
            var propertyValue = Variable(typeof(ushort?), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Optional,
                    PropertyName = propertyName,
                    PropertyValue = propertyValue,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (currentMember, continueLabel) => Block(
                        Assign(propertyValue, Call(Property(currentMember, JsonPropertyInfo.Value), JsonElementInfo.GetUInt16)),
                        Continue(continueLabel)
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, uint>> propertyExpression)
        {
            var propertySeen = Variable(typeof(bool),  $"saw {propertyName}");
            var propertyValue = Variable(typeof(uint), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Required,
                    PropertyName = propertyName,
                    PropertySeen = propertySeen,
                    PropertyValue = propertyValue,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (currentMember, continueLabel) => Block(
                        Assign(propertySeen,  Constant(true)),
                        Assign(propertyValue, Call(Property(currentMember, JsonPropertyInfo.Value), JsonElementInfo.GetUInt32)),
                        Continue(continueLabel)
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, uint?>> propertyExpression)
        {
            var propertyValue = Variable(typeof(uint?), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Optional,
                    PropertyName = propertyName,
                    PropertyValue = propertyValue,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (currentMember, continueLabel) => Block(
                        Assign(propertyValue, Call(Property(currentMember, JsonPropertyInfo.Value), JsonElementInfo.GetUInt32)),
                        Continue(continueLabel)
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, ulong>> propertyExpression)
        {
            var propertySeen = Variable(typeof(bool),   $"saw {propertyName}");
            var propertyValue = Variable(typeof(ulong), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Required,
                    PropertyName = propertyName,
                    PropertySeen = propertySeen,
                    PropertyValue = propertyValue,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (currentMember, continueLabel) => Block(
                        Assign(propertySeen,  Constant(true)),
                        Assign(propertyValue, Call(Property(currentMember, JsonPropertyInfo.Value), JsonElementInfo.GetUInt64)),
                        Continue(continueLabel)
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, bool>> propertyExpression)
        {
            var propertySeen = Variable(typeof(bool),  $"saw {propertyName}");
            var propertyValue = Variable(typeof(bool), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Required,
                    PropertyName = propertyName,
                    PropertySeen = propertySeen,
                    PropertyValue = propertyValue,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (currentMember, continueLabel) => Block(
                        Assign(propertySeen,  Constant(true)),
                        Assign(propertyValue, Call(Property(currentMember, JsonPropertyInfo.Value), JsonElementInfo.GetBoolean)),
                        Continue(continueLabel)
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, DateTime>> propertyExpression)
        {
            var propertySeen = Variable(typeof(bool),      $"saw {propertyName}");
            var propertyValue = Variable(typeof(DateTime), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Required,
                    PropertyName = propertyName,
                    PropertySeen = propertySeen,
                    PropertyValue = propertyValue,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (currentMember, continueLabel) => Block(
                        Assign(propertySeen,  Constant(true)),
                        Assign(propertyValue, Call(Property(currentMember, JsonPropertyInfo.Value), JsonElementInfo.GetDateTime)),
                        Continue(continueLabel)
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, DateTimeOffset>> propertyExpression)
        {
            var propertySeen = Variable(typeof(bool),            $"saw {propertyName}");
            var propertyValue = Variable(typeof(DateTimeOffset), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Required,
                    PropertyName = propertyName,
                    PropertySeen = propertySeen,
                    PropertyValue = propertyValue,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (currentMember, continueLabel) => Block(
                        Assign(propertySeen,  Constant(true)),
                        Assign(propertyValue, Call(Property(currentMember, JsonPropertyInfo.Value), JsonElementInfo.GetDateTimeOffset)),
                        Continue(continueLabel)
                    )
                }
            );
            _needsCompilation = true;
        }

        public void Map(string propertyName, Expression<Func<TObject, Guid>> propertyExpression)
        {
            var propertySeen = Variable(typeof(bool),  $"saw {propertyName}");
            var propertyValue = Variable(typeof(Guid), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    PropertySignificance = PropertySignificance.Required,
                    PropertyName = propertyName,
                    PropertySeen = propertySeen,
                    PropertyValue = propertyValue,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (currentMember, continueLabel) => Block(
                        Assign(propertySeen,  Constant(true)),
                        Assign(propertyValue, Call(Property(currentMember, JsonPropertyInfo.Value), JsonElementInfo.GetGuid)),
                        Continue(continueLabel)
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
                            return varBag.Append(reader.PropertyValue);
                        case PropertySignificance.Required:
                            return varBag.Concat(new[] { reader.PropertySeen, reader.PropertyValue });
                        default:
                            return varBag;
                    }
                }
            );

            var currentMember = Variable(typeof(JsonProperty), "currentMember");

            var expressions = Enumerable.Empty<Expression>();

            var block = Block(
                typeof(TObject),
                new List<ParameterExpression>(vars),
                expressions
                    .Append(EnsureValueKindIsObject())
                    .Append(
                        ForEachJsonProperty(
                            currentMember,
                            continueLabel => Block(
                                ((Func<IEnumerable<Expression>>) (() =>
                                {
                                    var memberHandling = new List<Expression>(_readers.Count + 1);
                                    foreach (var reader in _readers)
                                    {
                                        memberHandling.Add(
                                            IfThen(
                                                NameEquals(currentMember, Constant(reader.PropertyName, typeof(string))),
                                                reader.OnMatch(currentMember, continueLabel)
                                            )
                                        );
                                    }

                                    if (UnexpectedPropertyBehavior == UnexpectedPropertyBehavior.Error)
                                    {
                                        memberHandling.Add(ThrowJsonException(MissingMember(currentMember)));
                                    }

                                    return memberHandling;
                                }))()
                            )
                        )
                    )
                    .Concat(
                        _readers.Where(reader => reader.PropertySignificance == PropertySignificance.Required)
                            .Select(reader => EnsureMemberseen(reader.PropertyName, reader.PropertySeen))
                    )
                    .Append(
                        MemberInit(
                            New(typeof(TObject)),
                            from reader in _readers
                            where reader.PropertySignificance != PropertySignificance.Ignored
                            select Bind(reader.Destination, reader.PropertyValue)
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
                var continueLabel = Label();
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
                                body(continueLabel)
                            ),
                            Break(breakLabel)
                        ),
                        breakLabel,
                        continueLabel
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
            public delegate BlockExpression Process(ParameterExpression jsonPropertyExpression, LabelTarget continueLabel);

            public PropertySignificance PropertySignificance { get; set; }

            public string PropertyName { get; set; } = default!;

            public ParameterExpression PropertySeen { get; set; } = default!;

            public ParameterExpression PropertyValue { get; set; } = default!;

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
