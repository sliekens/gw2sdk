using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using GW2SDK.Impl.Json;
using GW2SDK.Impl.JsonReaders.Mappings;
using static System.Linq.Expressions.Expression;

namespace GW2SDK.Impl.JsonReaders
{
    public class JsonObjectReader2<TObject> : IJsonReader<TObject>
    {
        private readonly JsonObjectMapping<TObject> _mapping = new JsonObjectMapping<TObject>
        {
            Name = "$"
        };

        private Expression<ReadJson<TObject>>? _source;

        private ReadJson<TObject>? _read;

        public void Configure(Action<JsonObjectMapping<TObject>> configureAction)
        {
            var compiler = new JsonMappingCompiler<TObject>();
            configureAction(_mapping);
            _source = compiler.Build(_mapping);
            _read = _source.Compile();
        }

        public TObject Read(in JsonElement json)
        {
            if (_read is null) throw new InvalidOperationException("Call Configure before attempting to Read.");
            return _read(json);
        }

        public bool CanRead(in JsonElement json) => json.ValueKind == JsonValueKind.Object;
    }

    public class JsonObjectReader<TObject> : IJsonReader<TObject>
    {
        private static readonly ConstantExpression TypeNameExpr = Constant(typeof(TObject).Name, typeof(string));
        private readonly List<ReaderInfo> _readers = new List<ReaderInfo>();

        private ReadJson<TObject> _compilation = (in JsonElement json) => default!;

        private bool _needsCompilation = true;

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
                    MappingSignificance = MappingSignificance.Ignored,
                    PropertyName = propertyName,
                    OnMatch = (jsonPropertyExpr, continueLabelExpr) => Block(Continue(continueLabelExpr))
                }
            );
            _needsCompilation = true;
        }

        public Expression AssignArray(Expression arrayExpr, Expression indexExpr, Expression valueExpr) 
            => Assign(ArrayAccess(arrayExpr, indexExpr), valueExpr);

        public void Map(
            string propertyName,
            Expression<Func<TObject, IEnumerable<int>?>> propertyExpression,
            MappingSignificance significance = MappingSignificance.Required)
        {
            var propertySeenExpr = Variable(typeof(bool),   $"saw {propertyName}");
            var propertyValueExpr = Variable(typeof(int[]), $"value of {propertyName}");
            var arrayLengthExpr = Variable(typeof(int),     $"length of {propertyName}");
            var indexExpr = Variable(typeof(int),           "i");
            switch (significance)
            {
                case MappingSignificance.Required:
                    _readers.Add(
                        new ReaderInfo
                        {
                            MappingSignificance = MappingSignificance.Required,
                            PropertyName = propertyName,
                            PropertySeenExpr = propertySeenExpr,
                            PropertyValueExpr = propertyValueExpr,
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
                                        JsonElementExpr.GetInt32(
                                            MakeIndex(JsonPropertyExpr.GetValue(jsonPropertyExpr), JsonElementInfo.Item, new[] { indexExpr })
                                        )
                                    )
                                )
                            )
                        }
                    );
                    break;
                case MappingSignificance.Optional:
                    _readers.Add(
                        new ReaderInfo
                        {
                            MappingSignificance = MappingSignificance.Optional,
                            PropertyName = propertyName,
                            PropertyValueExpr = propertyValueExpr,
                            Destination = ((MemberExpression) propertyExpression.Body).Member,
                            OnMatch = (jsonPropertyExpr, _) => Block(
                                new[]
                                {
                                    arrayLengthExpr,
                                    indexExpr
                                },
                                Assign(indexExpr,         Constant(0)),
                                Assign(arrayLengthExpr,   Call(JsonPropertyExpr.GetValue(jsonPropertyExpr), JsonElementInfo.GetArrayLength)),
                                Assign(propertyValueExpr, NewArrayBounds(typeof(int), arrayLengthExpr)),
                                Expr.For(
                                    indexExpr,
                                    arrayLengthExpr,
                                    (_, __) => AssignArray(
                                        propertyValueExpr,
                                        indexExpr,
                                        JsonElementExpr.GetInt32(
                                            MakeIndex(JsonPropertyExpr.GetValue(jsonPropertyExpr), JsonElementInfo.Item, new[] { indexExpr })
                                        )
                                    )
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
            Expression<Func<TObject, IEnumerable<string>?>> propertyExpression,
            MappingSignificance significance = MappingSignificance.Required)
        {
            var propertySeenExpr = Variable(typeof(bool),      $"saw {propertyName}");
            var propertyValueExpr = Variable(typeof(string[]), $"value of {propertyName}");
            var arrayLengthExpr = Variable(typeof(int),        $"length of {propertyName}");
            var indexExpr = Variable(typeof(int),              "i");
            switch (significance)
            {
                case MappingSignificance.Required:
                    _readers.Add(
                        new ReaderInfo
                        {
                            MappingSignificance = MappingSignificance.Required,
                            PropertyName = propertyName,
                            PropertySeenExpr = propertySeenExpr,
                            PropertyValueExpr = propertyValueExpr,
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
                                            MakeIndex(JsonPropertyExpr.GetValue(jsonPropertyExpr), JsonElementInfo.Item, new[] { indexExpr })
                                        )
                                    )
                                )
                            )
                        }
                    );
                    break;
                case MappingSignificance.Optional:
                    _readers.Add(
                        new ReaderInfo
                        {
                            MappingSignificance = MappingSignificance.Optional,
                            PropertyName = propertyName,
                            PropertyValueExpr = propertyValueExpr,
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
                                            MakeIndex(JsonPropertyExpr.GetValue(jsonPropertyExpr), JsonElementInfo.Item, new[] { indexExpr })
                                        )
                                    )
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

        public void Map<TValue>(
            string propertyName,
            Expression<Func<TObject, IEnumerable<TValue>?>> propertyExpression,
            IJsonReader<TValue> valueReader,
            MappingSignificance significance = MappingSignificance.Required)
        {
            var propertySeenExpr = Variable(typeof(bool),      $"saw {propertyName}");
            var propertyValueExpr = Variable(typeof(TValue[]), $"value of {propertyName}");
            var arrayLengthExpr = Variable(typeof(int),        $"length of {propertyName}");
            var indexExpr = Variable(typeof(int),              "i");
            var readerExpression = Constant(valueReader);
            var readInfo = valueReader.GetType().GetMethod(nameof(Read), new[] { typeof(JsonElement).MakeByRefType() });
            switch (significance)
            {
                case MappingSignificance.Required:
                    _readers.Add(
                        new ReaderInfo
                        {
                            MappingSignificance = MappingSignificance.Required,
                            PropertyName = propertyName,
                            PropertySeenExpr = propertySeenExpr,
                            PropertyValueExpr = propertyValueExpr,
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
                                            MakeIndex(JsonPropertyExpr.GetValue(jsonPropertyExpr), JsonElementInfo.Item, new[] { indexExpr })
                                        )
                                    )
                                )
                            )
                        }
                    );
                    break;
                case MappingSignificance.Optional:
                    _readers.Add(
                        new ReaderInfo
                        {
                            MappingSignificance = MappingSignificance.Optional,
                            PropertyName = propertyName,
                            PropertyValueExpr = propertyValueExpr,
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
                                            MakeIndex(JsonPropertyExpr.GetValue(jsonPropertyExpr), JsonElementInfo.Item, new[] { indexExpr })
                                        )
                                    )
                                )
                            )
                        }
                    );
                    break;
                case MappingSignificance.Ignored:
                    Ignore(propertyName);
                    break;
            }

            _needsCompilation = true;
        }

        public void Map<TValue>(
            string propertyName,
            Expression<Func<TObject, TValue>> propertyExpression,
            IJsonReader<TValue> valueReader,
            MappingSignificance significance = MappingSignificance.Required)
        {
            var propertySeenExpr = Variable(typeof(bool),    $"saw {propertyName}");
            var propertyValueExpr = Variable(typeof(TValue), $"value of {propertyName}");
            var readerExpr = Constant(valueReader);
            var readInfo = valueReader.GetType().GetMethod(nameof(Read), new[] { typeof(JsonElement).MakeByRefType() });
            switch (significance)
            {
                case MappingSignificance.Required:
                    _readers.Add(
                        new ReaderInfo
                        {
                            MappingSignificance = MappingSignificance.Required,
                            PropertyName = propertyName,
                            PropertySeenExpr = propertySeenExpr,
                            PropertyValueExpr = propertyValueExpr,
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
                case MappingSignificance.Optional:
                    _readers.Add(
                        new ReaderInfo
                        {
                            MappingSignificance = MappingSignificance.Optional,
                            PropertyName = propertyName,
                            PropertyValueExpr = propertyValueExpr,
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
            MappingSignificance significance = MappingSignificance.Required)
        {
            var propertySeenExpr = Variable(typeof(bool),    $"saw {propertyName}");
            var propertyValueExpr = Variable(typeof(string), $"value of {propertyName}");
            _readers.Add(
                new ReaderInfo
                {
                    MappingSignificance = significance,
                    PropertyName = propertyName,
                    PropertySeenExpr = propertySeenExpr,
                    PropertyValueExpr = propertyValueExpr,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (jsonPropertyExpr, _) => Block(
                        Assign(propertySeenExpr,  Constant(true)),
                        Assign(propertyValueExpr, JsonElementExpr.GetString(JsonPropertyExpr.GetValue(jsonPropertyExpr)))
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
                    MappingSignificance = MappingSignificance.Required,
                    PropertyName = propertyName,
                    PropertySeenExpr = propertySeenExpr,
                    PropertyValueExpr = propertyValueExpr,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (jsonPropertyExpr, _) => Block(
                        Assign(propertySeenExpr,  Constant(true)),
                        Assign(propertyValueExpr, JsonElementExpr.GetDouble(JsonPropertyExpr.GetValue(jsonPropertyExpr)))
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
                    MappingSignificance = MappingSignificance.Required,
                    PropertyName = propertyName,
                    PropertySeenExpr = propertySeenExpr,
                    PropertyValueExpr = propertyValueExpr,
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
                    MappingSignificance = MappingSignificance.Optional,
                    PropertyName = propertyName,
                    PropertyValueExpr = propertyValueExpr,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (jsonPropertyExpr, _) => Block(
                        Assign(propertyValueExpr, JsonElementExpr.GetInt32OrNull(JsonPropertyExpr.GetValue(jsonPropertyExpr)))
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
                    MappingSignificance = MappingSignificance.Required,
                    PropertyName = propertyName,
                    PropertySeenExpr = propertySeenExpr,
                    PropertyValueExpr = propertyValueExpr,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (jsonPropertyExpr, _) => Block(
                        Assign(propertySeenExpr,  Constant(true)),
                        Assign(propertyValueExpr, JsonElementExpr.GetBoolean(JsonPropertyExpr.GetValue(jsonPropertyExpr)))
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
                    MappingSignificance = MappingSignificance.Required,
                    PropertyName = propertyName,
                    PropertySeenExpr = propertySeenExpr,
                    PropertyValueExpr = propertyValueExpr,
                    Destination = ((MemberExpression) propertyExpression.Body).Member,
                    OnMatch = (jsonPropertyExpr, _) => Block(
                        Assign(propertySeenExpr,  Constant(true)),
                        Assign(propertyValueExpr, JsonElementExpr.GetDateTimeOffset(JsonPropertyExpr.GetValue(jsonPropertyExpr)))
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
                    switch (reader.MappingSignificance)
                    {
                        case MappingSignificance.Optional:
                            return varBag.Append(reader.PropertyValueExpr);
                        case MappingSignificance.Required:
                            return varBag.Concat(new[] { reader.PropertySeenExpr, reader.PropertyValueExpr });
                        default:
                            return varBag;
                    }
                }
            );

            var jsonPropertyExpr = Variable(typeof(JsonProperty), "jsonProperty");

            var expressions = Enumerable.Empty<Expression>();

            var block = Block(
                typeof(TObject),
                new List<ParameterExpression>(vars),
                expressions
                    .Append(Expr.EnsureValueKindIsObject(json))
                    .Append(
                        Expr.ForEachJsonProperty(
                            json,
                            jsonPropertyExpr,
                            @continue => Expr.Read(TypeNameExpr, jsonPropertyExpr, @continue, _readers, 0, _unexpectedPropertyBehavior)
                        )
                    )
                    .Concat(
                        _readers.Where(reader => reader.MappingSignificance == MappingSignificance.Required)
                            .Select(reader => Expr.EnsureMemberseen(reader.PropertyName, reader.PropertySeenExpr, typeof(TObject).Name))
                    )
                    .Append(
                        MemberInit(
                            New(typeof(TObject)),
                            from reader in _readers
                            where reader.MappingSignificance != MappingSignificance.Ignored
                            select Bind(reader.Destination, reader.PropertyValueExpr)
                        )
                    )
            );

            _compilation = Lambda<ReadJson<TObject>>(block, json).Compile();
            _needsCompilation = false;
        }
    }
}
