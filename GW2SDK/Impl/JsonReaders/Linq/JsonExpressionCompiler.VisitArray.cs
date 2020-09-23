using System.Linq.Expressions;
using System.Text.Json;
using GW2SDK.Impl.Json;
using GW2SDK.Impl.JsonReaders.Mappings;
using static System.Linq.Expressions.Expression;

namespace GW2SDK.Impl.JsonReaders.Linq
{
    public partial class JsonExpressionCompiler
    {
        public void VisitArray(IJsonArrayMapping mapping)
        {
            if (mapping.Significance == MappingSignificance.Ignored)
            {
                return;
            }

            var ctx = Context.Peek();

            var arrayType = mapping.ValueType.MakeArrayType();
            var arrayValueExpr = Variable(arrayType, $"{mapping.Name}_value");
            Builder.Variables.Add(arrayValueExpr);

            if (mapping.Significance == MappingSignificance.Required)
            {
                Builder.Then
                (
                    IfThen
                    (
                        NotEqual
                        (
                            JsonElementExpr.GetValueKind(ctx.JsonNodeExpression),
                            Constant(JsonValueKind.Array)
                        ),
                        Throw
                        (
                            JsonExceptionExpr.ThrowJsonException
                            (
                                StringExpr.Format
                                (
                                    Constant
                                    (
                                        "Expected value of type Array, found {0}.",
                                        typeof(string)
                                    ),
                                    JsonElementExpr.GetValueKind(ctx.JsonNodeExpression)
                                ),
                                JsonPathExpr.ToString(ctx.JsonPathExpression)
                            )
                        )
                    )
                );

                Builder.Then(ConvertArray());
            }
            else
            {
                Builder.Then
                (
                    IfThen
                    (
                        Equal
                        (
                            JsonElementExpr.GetValueKind(ctx.JsonNodeExpression),
                            Constant(JsonValueKind.Array)
                        ),
                        ConvertArray()
                    )
                );
            }

            Builder.Then(arrayValueExpr);

            Expression ConvertArray()
            {
                Builder = Builder.CreateScope(arrayType);

                var arrayLengthExpr = Variable(typeof(int), $"{mapping.Name}_array_length");
                Builder.Variables.Add(arrayLengthExpr);
                Builder.Then
                (
                    Assign(arrayLengthExpr, JsonElementExpr.GetArrayLength(ctx.JsonNodeExpression))
                );

                var indexExpr = Variable(typeof(int), $"{mapping.Name}_index");
                Builder.Variables.Add(indexExpr);
                Builder.Then(Assign(indexExpr, Constant(0)));
                Builder.Then
                    (Assign(arrayValueExpr, NewArrayBounds(mapping.ValueType, arrayLengthExpr)));

                var breakTarget = Label(arrayType);
                Builder.Then
                (
                    Loop
                    (
                        IfThenElse
                        (
                            LessThan(indexExpr, arrayLengthExpr),
                            Block
                            (
                                Assign
                                (
                                    ArrayAccess(arrayValueExpr, indexExpr),
                                    ConvertArrayItem(indexExpr)
                                ),
                                PostIncrementAssign(indexExpr)
                            ),
                            Break(breakTarget, arrayValueExpr)
                        ),
                        breakTarget
                    )
                );

                try
                {
                    return Builder.ToExpression();
                }
                finally
                {
                    Builder = Builder.LeaveScope();
                }
            }

            Expression ConvertArrayItem(Expression indexExpr)
            {
                var indexExpression = MakeIndex
                    (ctx.JsonNodeExpression, JsonElementInfo.Item, new[] { indexExpr });
                var indexPathExpr = JsonPathExpr.AccessArrayIndex
                    (ctx.JsonPathExpression, indexExpr);

                Context.Push(new JsonContext(indexExpression, indexPathExpr));
                Builder = Builder.CreateScope(mapping.ValueType);

                try
                {
                    mapping.ValueMapping.Accept(this);
                    return Builder.ToExpression();
                }
                finally
                {
                    Context.Pop();
                    Builder = Builder.LeaveScope();
                }
            }
        }
    }
}
