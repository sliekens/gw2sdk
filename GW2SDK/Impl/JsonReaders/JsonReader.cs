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
                        Assign(propertyValue, GetInt32(GetValue(currentMember))),
                        Continue(continueLabel)
                    )
                }
            );
        }

        private static Expression GetName(Expression jsonPropertyExpression) => Property(jsonPropertyExpression, Member.Name);

        private static Expression GetValue(Expression jsonPropertyExpression) => Property(jsonPropertyExpression, Member.Value);

        private static Expression GetInt32(Expression jsonElementExpression) => Call(jsonElementExpression, Member.GetInt32);

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
                return Call(null, format, template, GetName(member));
            }

            Expression EnsureValueKindIsObject()
            {
                return IfThen(ValueKindNotObject(), ThrowJsonException(Constant("JSON is not an object.", typeof(string))));
            }

            Expression ValueKindNotObject()
            {
                var actual = Property(json, Member.ValueKind);
                var expected = Constant(JsonValueKind.Object);
                return NotEqual(actual, expected);
            }

            Expression ThrowJsonException(Expression message)
            {
                var constructorInfo = Member.JsonExceptionConstructor;
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
                return Property(enumerator, Member.Current);
            }

            MethodCallExpression MoveNext(ParameterExpression enumerator)
            {
                return Call(enumerator, Member.MoveNext);
            }

            MethodCallExpression GetObjectEnumerator()
            {
                return Call(json, Member.EnumerateObject);
            }

            Expression NameEquals(Expression jsonPropertyExpression, Expression textExpression)
            {
                return Call(jsonPropertyExpression, Member.NameEquals, textExpression);
            }

            Expression EnsureMemberseen(string propertyName, ParameterExpression check)
            {
                return IfThen(
                    IsFalse(check),
                    Throw(
                        New(
                            Member.JsonExceptionConstructor,
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

        private static class Member
        {
            public static readonly PropertyInfo ValueKind = typeof(JsonElement).GetProperty(nameof(JsonElement.ValueKind));

            public static readonly MethodInfo NameEquals = typeof(JsonProperty).GetMethod(nameof(JsonProperty.NameEquals), new[] { typeof(string) });

            public static readonly PropertyInfo Name = typeof(JsonProperty).GetProperty(nameof(JsonProperty.Name));

            public static readonly PropertyInfo Value = typeof(JsonProperty).GetProperty(nameof(JsonProperty.Value));

            public static readonly ConstructorInfo JsonExceptionConstructor = typeof(JsonException).GetConstructor(new[] { typeof(string) });

            public static readonly MethodInfo GetInt32 = typeof(JsonElement).GetMethod(nameof(JsonElement.GetInt32));

            public static readonly PropertyInfo Current = typeof(JsonElement.ObjectEnumerator).GetProperty(nameof(JsonElement.ObjectEnumerator.Current));

            public static readonly MethodInfo MoveNext = typeof(JsonElement.ObjectEnumerator).GetMethod(nameof(JsonElement.ObjectEnumerator.MoveNext));

            public static readonly MethodInfo EnumerateObject = typeof(JsonElement).GetMethod(nameof(JsonElement.EnumerateObject));
        }
    }
}
