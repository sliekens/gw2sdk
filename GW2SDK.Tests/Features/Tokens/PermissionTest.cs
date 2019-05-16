using System;
using GW2SDK.Features.Tokens;
using Xunit;

namespace GW2SDK.Tests.Features.Tokens
{
    public class PermissionTest
    {
        [Fact]
        [Trait("Feature", "Tokens")]
        [Trait("Category", "Unit")]
        public void Permission_ShouldNotDefineDefaultValue()
        {
            Assert.False(Enum.IsDefined(typeof(Permission), default(Permission)));
        }
    }
}
