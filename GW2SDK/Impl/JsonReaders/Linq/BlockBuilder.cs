using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using static System.Linq.Expressions.Expression;

namespace GW2SDK.Impl.JsonReaders.Linq
{
    public class BlockBuilder
    {
        public BlockBuilder? Parent { get; private set; }

        public List<ParameterExpression> Variables { get; } = new List<ParameterExpression>();

        private List<Expression> Body { get; } = new List<Expression>();

        public List<Expression> AfterBody { get; } = new List<Expression>();

        public Type? ReturnType { get; set; }

        public void Then(Expression expr)
        {
            Body.Add(expr);
        }

        public IEnumerable<Expression> GetBody() =>
            Body.DefaultIfEmpty(ReturnType is null ? Empty() : Default(ReturnType));

        public BlockBuilder RootScope()
        {
            var scope = this;
            while (scope.Parent is BlockBuilder)
            {
                scope = scope.Parent;
            }

            return scope;
        }

        public BlockBuilder CreateScope
            (Type? returnType) =>
            new BlockBuilder { Parent = this, ReturnType = returnType };

        public BlockBuilder LeaveScope()
        {
            Debug.Assert(Parent is BlockBuilder, "Can't leave the root scope.");
            return Parent;
        }

        public Expression ToExpression()
        {
            if (ReturnType is Type)
            {
                return Block(ReturnType, Variables, GetBody());
            }

            return Block(Variables, GetBody());
        }
    }
}
