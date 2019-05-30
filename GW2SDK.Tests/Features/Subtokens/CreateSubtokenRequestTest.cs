using System;
using System.Collections.Generic;
using System.Linq;
using GW2SDK.Features.Common;
using GW2SDK.Infrastructure.Subtokens;
using Xunit;

namespace GW2SDK.Tests.Features.Subtokens
{
    public class CreateSubtokenRequestTest
    {
        [Fact]
        [Trait("Feature",  "Subtokens")]
        [Trait("Category", "Unit")]
        public void CreateSubtokenRequest_WithAccessTokenNull_ShouldHaveNoAuthorizationHeader()
        {
            var sut = new CreateSubtokenRequest.Builder().GetRequest();

            Assert.Null(sut.Headers.Authorization);
        }

        [Fact]
        [Trait("Feature",  "Subtokens")]
        [Trait("Category", "Unit")]
        public void CreateSubtokenRequest_WithPermissions_ShouldSerializePermissionsAsQueryString()
        {
            var permissions = new List<Permission> { Permission.Account, Permission.Guilds, Permission.Progression };

            var sut = new CreateSubtokenRequest.Builder(permissions: permissions).GetRequest();

            Assert.Equal("/v2/createsubtoken?permissions=account,guilds,progression", sut.RequestUri.ToString());
        }

        [Fact]
        [Trait("Feature",  "Subtokens")]
        [Trait("Category", "Unit")]
        public void CreateSubtokenRequest_WithPermissionsNull_ShouldNotHavePermissionsInQueryString()
        {
            var sut = new CreateSubtokenRequest.Builder().GetRequest();

            Assert.Equal("/v2/createsubtoken", sut.RequestUri.ToString());
        }

        [Fact]
        [Trait("Feature",  "Subtokens")]
        [Trait("Category", "Unit")]
        public void CreateSubtokenRequest_WithPermissionsEmpty_ShouldNotHavePermissionsInQueryString()
        {
            var permissions = Enumerable.Empty<Permission>().ToList();

            var sut = new CreateSubtokenRequest.Builder(permissions: permissions).GetRequest();

            Assert.Equal("/v2/createsubtoken", sut.RequestUri.ToString());
        }
        
        [Fact]
        [Trait("Feature",  "Subtokens")]
        [Trait("Category", "Unit")]
        public void CreateSubtokenRequest_WithExpirationDate_ShouldSerializeDateAsQueryString()
        {
            var expirationDate = new DateTimeOffset(2019, 12, 25, 12, 34, 56, TimeSpan.Zero);

            var sut = new CreateSubtokenRequest.Builder(absoluteExpirationDate: expirationDate).GetRequest();

            Assert.Equal("/v2/createsubtoken?expire=2019-12-25T12:34:56", sut.RequestUri.ToString());
        }
        
        [Fact]
        [Trait("Feature",  "Subtokens")]
        [Trait("Category", "Unit")]
        public void CreateSubtokenRequest_WithUrls_ShouldSerializeUrlsAsQueryString()
        {
            var urls = new List<string>
            {
                "/v2/characters/My Cool Character",
                "/v2/account/home/cats"
            };

            var sut = new CreateSubtokenRequest.Builder(urls: urls).GetRequest();

            Assert.Equal("/v2/createsubtoken?urls=%2Fv2%2Fcharacters%2FMy%20Cool%20Character,%2Fv2%2Faccount%2Fhome%2Fcats", sut.RequestUri.ToString());
        }
    }
}
