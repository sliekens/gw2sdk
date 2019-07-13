using GW2SDK.Enums;
using Xunit;

namespace GW2SDK.Tests.Features.Accounts
{
    public class ProductNameTest
    {
        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Unit")]
        public void ProductName_None_ShouldBeDefault()
        {
            Assert.Equal(ProductName.None, default);
        }
    }
}
