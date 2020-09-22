using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;

namespace GW2SDK.Impl.JsonReaders.Linq
{
    public class BlockBuilder
    {
        public BlockBuilder? Parent { get; private set; }

        public List<ParameterExpression> Variables { get; } = new List<ParameterExpression>();

        public List<Expression> Body { get; } = new List<Expression>();

        public Type? ReturnType { get; set; }

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
                return Expression.Block(ReturnType, Variables, Body);
            }

            return Expression.Block(Variables, Body);
        }
    }
}
