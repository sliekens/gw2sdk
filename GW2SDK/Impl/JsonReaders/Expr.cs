using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using GW2SDK.Impl.Json;

namespace GW2SDK.Impl.JsonReaders
{
    internal static class Expr
    {
        internal static Expression For(ParameterExpression indexExpr, Expression lengthExpr, DoFor bodyExpr)
        {
            var breakTarget = Expression.Label();
            var continueTarget = Expression.Label();
            return Expression.Loop(
                Expression.IfThenElse(
                    Expression.LessThan(indexExpr, lengthExpr),
                    Expression.Block(
                        bodyExpr(breakTarget, continueTarget),
                        Expression.PostIncrementAssign(indexExpr)
                    ),
                    Expression.Break(breakTarget)
                ),
                breakTarget,
                continueTarget
            );
        }

        internal static Expression Read(
            Expression typeNameExpr,
            ParameterExpression jsonPropertyExpr,
            LabelTarget @continue,
            IReadOnlyList<ReaderInfo> readers,
            int index,
            UnexpectedPropertyBehavior unexpectedPropertyBehavior)
        {
            var reader = readers[index];
            var test = JsonPropertyExpr.NameEquals(jsonPropertyExpr, Expression.Constant(reader.PropertyName, typeof(string)));
            var ifTrue = reader.OnMatch(jsonPropertyExpr, @continue);
            Expression? ifFalse;
            if (index + 1 < readers.Count)
            {
                ifFalse = Read(typeNameExpr, jsonPropertyExpr, @continue, readers, index + 1, unexpectedPropertyBehavior);
            }
            else if (unexpectedPropertyBehavior == UnexpectedPropertyBehavior.Error)
            {
                ifFalse = ThrowJsonException(MissingMember(typeNameExpr, jsonPropertyExpr));
            }
            else
            {
                ifFalse = default;
            }

            return ifFalse is null ? Expression.IfThen(test, ifTrue) : Expression.IfThenElse(test, ifTrue, ifFalse);
        }

        internal static Expression UnexpectedProperty(Expression jsonPathExpr, Expression jsonPropertyExpr)
        {
            var format = Expression.Constant("Unexpected property '{0}.{1}'.", typeof(string));
            var nameExpr = Expression.Property(jsonPropertyExpr, JsonPropertyInfo.Name);
            return StringExpr.Format(format, jsonPathExpr, nameExpr);
        }

        internal static Expression MissingMember(Expression typeNameExpr, Expression member)
        {
            var format = Expression.Constant("JSON property '{0}' was unexpected for type '{1}'.", typeof(string));
            return StringExpr.Format(format, Expression.Property(member, JsonPropertyInfo.Name), typeNameExpr);
        }

        internal static Expression EnsureValueKindIsObject(Expression jsonElementExpr) =>
            Expression.IfThen(ValueKindNotObject(jsonElementExpr), ThrowJsonException(Expression.Constant("JSON is not an object.", typeof(string))));

        internal static Expression ValueKindNotObject(Expression jsonElementExpr)
        {
            var actual = Expression.Property(jsonElementExpr, JsonElementInfo.ValueKind);
            var expected = Expression.Constant(JsonValueKind.Object);
            return Expression.NotEqual(actual, expected);
        }

        internal static Expression ThrowJsonException(Expression message)
        {
            var constructorInfo = JsonExceptionInfo.JsonExceptionConstructor;
            var exception = Expression.New(constructorInfo, message);
            return Expression.Throw(exception, exception.Type);
        }

        internal static Expression ForEachJsonProperty(Expression jsonElementExpr, ParameterExpression current, Func<LabelTarget, Expression> body)
        {
            var enumerator = Expression.Variable(typeof(JsonElement.ObjectEnumerator), "enumerator");
            var breakLabel = Expression.Label();
            var continueLabelExpr = Expression.Label();
            return Expression.Block(
                new[]
                {
                    enumerator
                },
                Expression.Assign(enumerator, GetObjectEnumerator(jsonElementExpr)),
                Expression.Loop(
                    Expression.IfThenElse(
                        MoveNext(enumerator),
                        Expression.Block(
                            new[] { current },
                            Expression.Assign(current, GetCurrent(enumerator)),
                            body(continueLabelExpr)
                        ),
                        Expression.Break(breakLabel)
                    ),
                    breakLabel,
                    continueLabelExpr
                )
            );
        }

        internal static Expression GetCurrent(ParameterExpression enumerator) => Expression.Property(enumerator, JsonElementInfo.Current);

        internal static MethodCallExpression MoveNext(ParameterExpression enumerator) => Expression.Call(enumerator, JsonElementInfo.MoveNext);

        internal static MethodCallExpression GetObjectEnumerator(Expression jsonElementExpr) => Expression.Call(jsonElementExpr, JsonElementInfo.EnumerateObject);

        internal static Expression EnsureMemberseen(string propertyName, ParameterExpression check, string typeName) =>
            Expression.IfThen(
                Expression.IsFalse(check),
                Expression.Throw(
                    Expression.New(
                        JsonExceptionInfo.JsonExceptionConstructor,
                        Expression.Constant($"Missing required property '{propertyName}' for object of type '{typeName}'.")
                    )
                )
            );

        internal delegate Expression DoFor(LabelTarget @break, LabelTarget @continue);
    }
}
