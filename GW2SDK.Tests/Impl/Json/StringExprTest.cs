using System;
using System.Linq.Expressions;
using GW2SDK.Impl.Json;
using Xunit;

namespace GW2SDK.Tests.Impl.Json
{
    public class StringExprTest
    {
        [Fact]
        public void It_can_format_strings_with_one_arg()
        {
            var formatExpr = Expression.Constant("First '{0}'.", typeof(string));

            var arg0Expr = Expression.Constant("one", typeof(string));

            var expr = StringExpr.Format(formatExpr, arg0Expr);

            var compilation = Expression.Lambda<Func<string>>(expr);

            var sut = compilation.Compile();

            var actual = sut();

            Assert.Equal("First 'one'.", actual);
        }

        [Fact]
        public void It_can_format_strings_with_two_args()
        {
            var formatExpr = Expression.Constant("First '{0}', then '{1}'.", typeof(string));

            var arg0Expr = Expression.Constant("one", typeof(string));

            var arg1Expr = Expression.Constant("two", typeof(string));

            var expr = StringExpr.Format(formatExpr, arg0Expr, arg1Expr);

            var compilation = Expression.Lambda<Func<string>>(expr);

            var sut = compilation.Compile();

            var actual = sut();

            Assert.Equal("First 'one', then 'two'.", actual);
        }

        [Fact]
        public void It_can_format_strings_with_three_args()
        {
            var formatExpr = Expression.Constant("First '{0}', then '{1}' and finally '{2}'.", typeof(string));

            var arg0Expr = Expression.Constant("one", typeof(string));

            var arg1Expr = Expression.Constant("two", typeof(string));

            var arg2Expr = Expression.Constant("three", typeof(string));

            var expr = StringExpr.Format(formatExpr, arg0Expr, arg1Expr, arg2Expr);

            var compilation = Expression.Lambda<Func<string>>(expr);

            var sut = compilation.Compile();

            var actual = sut();

            Assert.Equal("First 'one', then 'two' and finally 'three'.", actual);
        }
    }
}
