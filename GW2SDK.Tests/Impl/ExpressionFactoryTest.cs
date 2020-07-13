using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;
using GW2SDK.Builds;
using GW2SDK.Impl;
using Xunit;
using static System.Linq.Expressions.Expression;

namespace GW2SDK.Tests.Impl
{
    public class ExpressionFactoryTest
    {
        [Fact]
        public void It_can_create_strings()
        {
            var actual = Constant("hello world", typeof(string));

            Assert.Equal("hello world", actual.Value);
        }

        [Fact]
        public void It_can_create_exceptions()
        {
            var message = "error";

            var messageExpression = Constant(message, typeof(string));
            var actual = New(typeof(JsonException).GetConstructor(new[] { typeof(string) }), (Expression) messageExpression);

            Assert.Equal(typeof(JsonException), actual.Type);
            var arg = Assert.IsType<ConstantExpression>(actual.Arguments.Single());
            Assert.Equal(message, arg.Value);
        }

        [Fact]
        public void It_can_throw_exceptions()
        {
            var message = "error";

            var messageExpression = Constant(message, typeof(string));
            var newExpression = New(typeof(JsonException).GetConstructor(new[] { typeof(string) }), (Expression) messageExpression);
            var actual = ExpressionFactory.CreateThrowExpression(newExpression);
        }

        [Fact]
        public void It_can_create_reader()
        {
            var actual = ExpressionFactory.Reader<Build>();

            var f = actual.Compile(false);

            var json = JsonDocument.Parse("{\"id\": 12345}");

            var build = f(json.RootElement);
            Assert.Equal(12345, build.Id);
        }

        [Fact]
        public void It_can_create_build_parser()
        {
            var sut = new BuildJsonReader();

            var f = sut.Compile();
            var json = JsonDocument.Parse("{\"id\": 12345}");
        }
    }

    public class BuildJsonReader
    {
        private static readonly ConstructorInfo JsonExceptionConstructor = typeof(JsonException).GetConstructor(new[] { typeof(string) });

        public Func<JsonElement, Build> Compile()
        {
            var json = Parameter(typeof(JsonElement), "json");

            var memberValue = Variable(typeof(int),            "memberValue");
            var memberSeen = Variable(typeof(bool),            "memberSeen");
            var currentMember = Variable(typeof(JsonProperty), "currentMember");

            var block = Block(typeof(Build),
                new List<ParameterExpression>
                {
                    memberSeen,
                    memberValue
                },
                new List<Expression>
                {
                    EnsureValueKindIsObject(),
                    ForEachJsonProperty(currentMember,
                        Block(Expression.Assign(memberSeen, Constant(true)),
                            Expression.Assign(memberValue,  GetInt32(currentMember)))),
                    EnsureMemberseen("id", memberSeen),
                    CreateValue(Assign("Id", memberValue))
                });

            var f = Lambda<Func<JsonElement, Build>>(block, json);
            return f.Compile();

            Expression EnsureValueKindIsObject()
            {
                return IfThen(ValueKindNotObject(), ThrowJsonException());
            }

            Expression ValueKindNotObject()
            {
                var actual = Property(json, typeof(JsonElement).GetProperty("ValueKind")!);
                var expected = Constant(JsonValueKind.Object);
                return NotEqual(actual, expected);
            }

            Expression ThrowJsonException()
            {
                var message = Constant("JSON is not an object.", typeof(string));
                var constructorInfo = JsonExceptionConstructor;
                var exception = New(constructorInfo, message);
                return Throw(exception, exception.Type);
            }

            Expression GetInt32(Expression jsonPropertyExpression)
            {
                var value = Property(jsonPropertyExpression, typeof(JsonProperty).GetProperty(nameof(JsonProperty.Value))!);
                return Call(value, typeof(JsonElement).GetMethod(nameof(JsonElement.GetInt32))!);
            }

            Expression ForEachJsonProperty(ParameterExpression current, Expression body)
            {
                var enumerator = Variable(typeof(JsonElement.ObjectEnumerator), "enumerator");
                var breakLabel = Label("EOF");
                return Block(new[]
                    {
                        enumerator
                    },
                    Expression.Assign(enumerator, GetObjectEnumerator()),
                    Loop(IfThenElse(MoveNext(enumerator),
                            Block(new[] { current },
                                Expression.Assign(current, Current(enumerator)),
                                body),
                            Break(breakLabel)),
                        breakLabel));
            }

            Expression Current(ParameterExpression enumerator)
            {
                return Property(enumerator, typeof(JsonElement.ObjectEnumerator).GetProperty(nameof(JsonElement.ObjectEnumerator.Current))!);
            }

            MethodCallExpression MoveNext(ParameterExpression enumerator)
            {
                return Call(enumerator, typeof(JsonElement.ObjectEnumerator).GetMethod(nameof(JsonElement.ObjectEnumerator.MoveNext))!);
            }

            MethodCallExpression GetObjectEnumerator()
            {
                return Call(json, typeof(JsonElement).GetMethod(nameof(JsonElement.EnumerateObject))!);
            }

            Expression EnsureMemberseen(string propertyName, ParameterExpression check)
            {
                return IfThen(IsFalse(check),
                    Throw(New(JsonExceptionConstructor, Constant($"Missing required property '{propertyName}' for object of type '{nameof(Build)}'."))));
            }

            Expression CreateValue(params MemberBinding[] args)
            {
                var factory = New(typeof(Build));
                return MemberInit(factory, args);
            }

            MemberBinding Assign(string propertyName, Expression value)
            {
                return Bind(typeof(Build).GetProperty(propertyName)!, value);
            }
        }
    }
}
