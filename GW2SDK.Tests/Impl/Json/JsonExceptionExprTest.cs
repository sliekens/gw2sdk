using System;
using System.Linq.Expressions;
using System.Text.Json;
using GW2SDK.Impl.Json;
using Xunit;

namespace GW2SDK.Tests.Impl.Json
{
    public class JsonExceptionExprTest
    {
        [Fact]
        public void It_can_throw_json_exception()
        {
            var message = Expression.Constant("An error");

            var expr = JsonExceptionExpr.ThrowJsonException(message);

            var compilation = Expression.Lambda<Action>(expr);

            var sut = compilation.Compile();

            var actual = Record.Exception(() => sut());

            var reason = Assert.IsType<JsonException>(actual);

            Assert.Equal("An error", reason.Message);
        }
    }
}
