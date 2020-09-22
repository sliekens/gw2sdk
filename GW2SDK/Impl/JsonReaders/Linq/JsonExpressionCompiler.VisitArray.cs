using System.Linq.Expressions;
using GW2SDK.Impl.Json;
using GW2SDK.Impl.JsonReaders.Mappings;
using static System.Linq.Expressions.Expression;

namespace GW2SDK.Impl.JsonReaders.Linq
{
    public partial class JsonExpressionCompiler
    {
        public void VisitArray(IJsonArrayMapping mapping)
        {
            var ctx = Context.Peek();

            if (mapping.Significance == MappingSignificance.Ignored)
            {
                Builder.Body.Add(Empty());
            }
            else
            {
                var arraySeenExpr = Variable(typeof(bool), $"{mapping.Name}_array_seen");
                if (mapping.Significance == MappingSignificance.Required)
                {
                    Builder.Variables.Add(arraySeenExpr);
                    Builder.Body.Add(Assign(arraySeenExpr, Constant(true)));
                }

                var arrayLengthExpr = Variable(typeof(int), $"{mapping.Name}_array_length");
                Builder.Variables.Add(arrayLengthExpr);
                Builder.Body.Add(Assign(arrayLengthExpr, JsonElementExpr.GetArrayLength(ctx.JsonNodeExpression)));

                var indexExpr = Variable(typeof(int), $"{mapping.Name}_index");
                Builder.Variables.Add(indexExpr);
                Builder.Body.Add(Assign(indexExpr, Constant(0)));

                var arrayType = mapping.ValueType.MakeArrayType();
                var arrayValueExpr = Variable(arrayType, $"{mapping.Name}_value");
                Builder.Variables.Add(arrayValueExpr);
                Builder.Body.Add(Assign(arrayValueExpr, NewArrayBounds(mapping.ValueType, arrayLengthExpr)));

                
                var breakTarget = Label(arrayType);
                var continueTarget = Label();
                Builder.Body.Add(Loop(
                    IfThenElse(
                        LessThan(indexExpr, arrayLengthExpr),
                        Block(
                            Assign(ArrayAccess(arrayValueExpr, indexExpr), ReadItem()),
                            PostIncrementAssign(indexExpr)
                        ),
                        Break(breakTarget, arrayValueExpr, arrayType)
                    ),
                    breakTarget,
                    continueTarget
                ));

                Expression ReadItem()
                {
                    var indexExpression = MakeIndex(ctx.JsonNodeExpression, JsonElementInfo.Item, new[] { indexExpr });
                    var indexPathExpr = JsonPathExpr.AccessArrayIndex(ctx.JsonPathExpression, indexExpr);

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
}
