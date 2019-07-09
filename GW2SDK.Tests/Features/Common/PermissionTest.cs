using System;
using GW2SDK.Enums;
using Xunit;

namespace GW2SDK.Tests.Features.Common
{
    public class PermissionTest
    {
        [Fact]
        [Trait("Feature",  "Tokens")]
        [Trait("Category", "Unit")]
        public void Permission_ShouldNotDefineDefaultValue()
        {
            Assert.False(Enum.IsDefined(typeof(Permission), default(Permission)));
        }
    }
}
