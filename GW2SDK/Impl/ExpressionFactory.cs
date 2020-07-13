using System;
using System.Linq.Expressions;
using System.Text.Json;

namespace GW2SDK.Impl
{
    internal static class ExpressionFactory
    {
        /*
         * Build (JsonElement json)
         * {
         *      if (json.ValueKind != JsonValueKind.Object)
         *      {
         *          throw new JsonException("JSON is not an object");
         *      }
         *
         * }
         *
         */

        /*
         *
         * throw new JsonException("JSON is not an object");
         */
        //internal static Expression ThrowJsonException()
        //{
        //    Expression.New(typeof(JsonException),)
        //}
        
        // Type var = default(Type);
        internal static (Expression var, Expression varInit) CreateVariable(Type type)
        {
            var var = Expression.Variable(type);
            return (var, Expression.Assign(var, Expression.Constant(42)));
        }

        internal static UnaryExpression CreateThrowExpression(NewExpression expression) => Expression.Throw(expression, expression.Type);

        internal static Expression<Func<JsonElement, TValue>> Reader<TValue>() where TValue : new()
        {
            var json = Expression.Parameter(typeof(JsonElement), "json");

            var body = Expression.Block(
                typeof(TValue),
                EnsureValueKindIsObject(json),
                CreateValue(
                    Assign("Id", GetInt32(GetProperty("id")))));

            return Expression.Lambda<Func<JsonElement, TValue>>(body, json);

            Expression CreateValue(params MemberBinding[] args)
            {
                var factory = Expression.New(typeof(TValue));
                return Expression.MemberInit(factory, args);
            }

            MemberBinding Assign(string propertyName, Expression value)
            {
                return Expression.Bind(typeof(TValue).GetProperty(propertyName), value);
            }

            Expression GetInt32(Expression e)
            {
                var method = typeof(JsonElement).GetMethod("GetInt32");
                return Expression.Call(e, method);
            }

            Expression GetProperty(string propertyName)
            {
                var method = typeof(JsonElement).GetMethod("GetProperty", new []{typeof(string)});
                return Expression.Call(json, method, Expression.Constant(propertyName));
            }

            Expression EnsureValueKindIsObject(ParameterExpression json)
            {
                return Expression.IfThen(test: ValueKindNotObject(json), ifTrue: ThrowJsonException());
            }

            Expression ValueKindNotObject(ParameterExpression json)
            {
                var actual = Expression.Property(json, typeof(JsonElement).GetProperty("ValueKind"));
                var expected = Expression.Constant(JsonValueKind.Object);
                return Expression.NotEqual(actual, expected);
            }

            Expression ThrowJsonException()
            {
                var message = Expression.Constant("JSON is not an object.", typeof(string));
                var constructorInfo = typeof(JsonException).GetConstructor(new[] { typeof(string) });
                var exception = Expression.New(constructorInfo, message);
                return Expression.Throw(exception, exception.Type);
            }
        }
    }
}
