using System;
using GW2SDK.Features.Worlds;
using Xunit;

namespace GW2SDK.Tests.Features.Worlds
{
    public class PopulationTest
    {
        [Fact]
        [Trait("Feature",  "Worlds")]
        [Trait("Category", "Unit")]
        public void Population_ShouldNotDefineDefaultValue()
        {
            Assert.False(Enum.IsDefined(typeof(Population), default(Population)));
        }
    }
}
