using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;
using static System.Linq.Expressions.Expression;

namespace GW2SDK.Impl.JsonReaders
{
    public class JsonReader<TObject>
    {
        private readonly List<ReaderInfo> _readers = new List<ReaderInfo>();

        public void Require(string propertyName, Expression<Func<TObject, string>> propertyExpression)
        {
            var propertySeen = Variable(typeof(bool),    propertyName + "_seen");
            var propertyValue = Variable(typeof(string), propertyName + "_value");
            _readers.Add(
                new ReaderInfo
                {
                    Required = true,
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
        }

        public void Require(string propertyName, Expression<Func<TObject, float>> propertyExpression)
        {
            var propertySeen = Variable(typeof(bool),   propertyName + "_seen");
            var propertyValue = Variable(typeof(float), propertyName + "_value");
            _readers.Add(
                new ReaderInfo
                {
                    Required = true,
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
        }

        public void Require(string propertyName, Expression<Func<TObject, double>> propertyExpression)
        {
            var propertySeen = Variable(typeof(bool),    propertyName + "_seen");
            var propertyValue = Variable(typeof(double), propertyName + "_value");
            _readers.Add(
                new ReaderInfo
                {
                    Required = true,
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
        }

        public void Require(string propertyName, Expression<Func<TObject, decimal>> propertyExpression)
        {
            var propertySeen = Variable(typeof(bool),     propertyName + "_seen");
            var propertyValue = Variable(typeof(decimal), propertyName + "_value");
            _readers.Add(
                new ReaderInfo
                {
                    Required = true,
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
        }

        public void Require(string propertyName, Expression<Func<TObject, sbyte>> propertyExpression)
        {
            var propertySeen = Variable(typeof(bool),   propertyName + "_seen");
            var propertyValue = Variable(typeof(sbyte), propertyName + "_value");
            _readers.Add(
                new ReaderInfo
                {
                    Required = true,
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
        }

        public void Require(string propertyName, Expression<Func<TObject, short>> propertyExpression)
        {
            var propertySeen = Variable(typeof(bool),   propertyName + "_seen");
            var propertyValue = Variable(typeof(short), propertyName + "_value");
            _readers.Add(
                new ReaderInfo
                {
                    Required = true,
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
        }

        public void Require(string propertyName, Expression<Func<TObject, int>> propertyExpression)
        {
            var propertySeen = Variable(typeof(bool), propertyName + "_seen");
            var propertyValue = Variable(typeof(int), propertyName + "_value");
            _readers.Add(
                new ReaderInfo
                {
                    Required = true,
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
        }

        public void Require(string propertyName, Expression<Func<TObject, long>> propertyExpression)
        {
            var propertySeen = Variable(typeof(bool),  propertyName + "_seen");
            var propertyValue = Variable(typeof(long), propertyName + "_value");
            _readers.Add(
                new ReaderInfo
                {
                    Required = true,
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
        }

        public void Require(string propertyName, Expression<Func<TObject, byte>> propertyExpression)
        {
            var propertySeen = Variable(typeof(bool),  propertyName + "_seen");
            var propertyValue = Variable(typeof(byte), propertyName + "_value");
            _readers.Add(
                new ReaderInfo
                {
                    Required = true,
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
        }

        public void Require(string propertyName, Expression<Func<TObject, ushort>> propertyExpression)
        {
            var propertySeen = Variable(typeof(bool),    propertyName + "_seen");
            var propertyValue = Variable(typeof(ushort), propertyName + "_value");
            _readers.Add(
                new ReaderInfo
                {
                    Required = true,
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
        }

        public void Require(string propertyName, Expression<Func<TObject, uint>> propertyExpression)
        {
            var propertySeen = Variable(typeof(bool),  propertyName + "_seen");
            var propertyValue = Variable(typeof(uint), propertyName + "_value");
            _readers.Add(
                new ReaderInfo
                {
                    Required = true,
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
        }

        public void Require(string propertyName, Expression<Func<TObject, ulong>> propertyExpression)
        {
            var propertySeen = Variable(typeof(bool),   propertyName + "_seen");
            var propertyValue = Variable(typeof(ulong), propertyName + "_value");
            _readers.Add(
                new ReaderInfo
                {
                    Required = true,
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
        }

        public void Require(string propertyName, Expression<Func<TObject, bool>> propertyExpression)
        {
            var propertySeen = Variable(typeof(bool),  propertyName + "_seen");
            var propertyValue = Variable(typeof(bool), propertyName + "_value");
            _readers.Add(
                new ReaderInfo
                {
                    Required = true,
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
        }

        public void Require(string propertyName, Expression<Func<TObject, DateTime>> propertyExpression)
        {
            var propertySeen = Variable(typeof(bool),      propertyName + "_seen");
            var propertyValue = Variable(typeof(DateTime), propertyName + "_value");
            _readers.Add(
                new ReaderInfo
                {
                    Required = true,
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
        }

        public void Require(string propertyName, Expression<Func<TObject, DateTimeOffset>> propertyExpression)
        {
            var propertySeen = Variable(typeof(bool),            propertyName + "_seen");
            var propertyValue = Variable(typeof(DateTimeOffset), propertyName + "_value");
            _readers.Add(
                new ReaderInfo
                {
                    Required = true,
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
        }

        public void Require(string propertyName, Expression<Func<TObject, Guid>> propertyExpression)
        {
            var propertySeen = Variable(typeof(bool),  propertyName + "_seen");
            var propertyValue = Variable(typeof(Guid), propertyName + "_value");
            _readers.Add(
                new ReaderInfo
                {
                    Required = true,
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
        }

        public Func<JsonElement, TObject> Compile()
        {
            var json = Parameter(typeof(JsonElement), "json");

            var vars = _readers.SelectMany(reader => new[] { reader.PropertySeen, reader.PropertyValue });
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
                                _readers.Select(
                                        reader => IfThen(
                                            NameEquals(currentMember, Constant(reader.PropertyName, typeof(string))),
                                            reader.OnMatch(currentMember, continueLabel)
                                        )
                                    )
                                    .Append(ThrowJsonException(MissingMember(currentMember)))
                            )
                        )
                    )
                    .Concat(_readers.Select(reader => reader.Required ? EnsureMemberseen(reader.PropertyName, reader.PropertySeen) : null))
                    .Append(
                        MemberInit(
                            New(typeof(TObject)),
                            _readers.Select(reader => Bind(reader.Destination, reader.PropertyValue))
                        )
                    )
            );

            var f = Lambda<Func<JsonElement, TObject>>(block, json);
            return f.Compile();

            Expression MissingMember(ParameterExpression member)
            {
                var format = typeof(string).GetMethod(nameof(string.Format), new[] { typeof(string), typeof(object) });
                var template = Constant("Could not find member '{0}' on object of type '" + typeof(TObject).Name + "'", typeof(string));
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

        private class ReaderInfo
        {
            public bool Required { get; set; }

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

            public static readonly MethodInfo EnumerateObject = typeof(JsonElement).GetMethod(nameof(JsonElement.EnumerateObject));

            public static readonly PropertyInfo Current = typeof(JsonElement.ObjectEnumerator).GetProperty(nameof(JsonElement.ObjectEnumerator.Current));

            public static readonly MethodInfo MoveNext = typeof(JsonElement.ObjectEnumerator).GetMethod(nameof(JsonElement.ObjectEnumerator.MoveNext));
        }
    }
}
