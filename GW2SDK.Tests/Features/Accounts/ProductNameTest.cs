using GW2SDK.Enums;
using Xunit;

namespace GW2SDK.Tests.Features.Accounts
{
    public class ProductNameTest
    {
        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Unit")]
        public void Default_ProductName_is_None()
        {
            Assert.Equal(ProductName.None, default);
        }
    }
}
