using GW2SDK.Features.Accounts;
using Xunit;

namespace GW2SDK.Tests.Features.Accounts
{
    public class GameAccessTest
    {
        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Unit")]
        public void GameAccess_None_ShouldBeDefault()
        {
            Assert.Equal(GameAccess.None, default);
        }
    }
}
