using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using GW2SDK.Enums;
using GW2SDK.Subtokens.Impl;
using Xunit;

namespace GW2SDK.Tests.Features.Subtokens
{
    public class CreateSubtokenRequestTest
    {
        [Fact]
        [Trait("Feature",  "Subtokens")]
        [Trait("Category", "Unit")]
        public void Request_is_unauthorized_by_default()
        {
            var sut = new CreateSubtokenRequest.Builder().GetRequest();

            Assert.Null(sut.Headers.Authorization);
        }

        [Fact]
        [Trait("Feature",  "Subtokens")]
        [Trait("Category", "Unit")]
        public void Request_supports_bearer_authentication()
        {
            const string accessToken = "123";

            var sut = new CreateSubtokenRequest.Builder(accessToken).GetRequest();

            Assert.Equal(sut.Headers.Authorization, new AuthenticationHeaderValue("Bearer", accessToken));
        }

        [Fact]
        [Trait("Feature",  "Subtokens")]
        [Trait("Category", "Unit")]
        public void Request_supports_permission_filters()
        {
            var permissions = new List<Permission> { Permission.Account, Permission.Guilds, Permission.Progression };

            var sut = new CreateSubtokenRequest.Builder(permissions: permissions).GetRequest();

            Assert.Equal("/v2/createsubtoken?permissions=account,guilds,progression", sut.RequestUri.ToString());
        }

        [Fact]
        [Trait("Feature",  "Subtokens")]
        [Trait("Category", "Unit")]
        public void Request_supports_expiration_dates()
        {
            var expirationDate = new DateTimeOffset(2019, 12, 25, 12, 34, 56, TimeSpan.Zero);

            var sut = new CreateSubtokenRequest.Builder(absoluteExpirationDate: expirationDate).GetRequest();

            Assert.Equal("/v2/createsubtoken?expire=2019-12-25T12:34:56", sut.RequestUri.ToString());
        }

        [Fact]
        [Trait("Feature",  "Subtokens")]
        [Trait("Category", "Unit")]
        public void Request_supports_url_filters()
        {
            var urls = new List<string> { "/v2/characters/My Cool Character", "/v2/account/home/cats" };

            var sut = new CreateSubtokenRequest.Builder(urls: urls).GetRequest();

            Assert.Equal("/v2/createsubtoken?urls=%2Fv2%2Fcharacters%2FMy%20Cool%20Character,%2Fv2%2Faccount%2Fhome%2Fcats", sut.RequestUri.ToString());
        }
    }
}
