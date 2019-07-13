﻿using System;
using System.Linq;
using GW2SDK.Enums;
using GW2SDK.Impl.JsonConverters;
using GW2SDK.Tests.Features.Tokens.Fixtures;
using GW2SDK.Tests.TestInfrastructure;
using GW2SDK.Tokens;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Tokens
{
    public class SubtokenInfoTest : IClassFixture<SubtokenInfoFixture>
    {
        public SubtokenInfoTest(SubtokenInfoFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        private readonly SubtokenInfoFixture _fixture;

        private readonly ITestOutputHelper _output;

        [Fact]
        [Trait("Feature",    "Tokens")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Class_ShouldHaveNoMissingMembers()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output))
                                                              .UseMissingMemberHandling(MissingMemberHandling.Error)
                                                              .Build();

            // Next statement throws if there are missing members
            _ = JsonConvert.DeserializeObject<SubtokenInfo>(_fixture.SubtokenInfoJson, settings);
        }

        [Fact]
        [Trait("Feature",  "Tokens")]
        [Trait("Category", "Integration")]
        public void Id_ShouldNotBeEmpty()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<SubtokenInfo>(_fixture.SubtokenInfoJson, settings);

            Assert.NotEmpty(actual.Id);
        }

        [Fact]
        [Trait("Feature",  "Tokens")]
        [Trait("Category", "Integration")]
        public void Name_ShouldBeGW2SDKFull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<SubtokenInfo>(_fixture.SubtokenInfoJson, settings);

            // This is not intended to improve account security, only to prevent key abuse
            // The reason is that some services like GW2BLTC.com associate keys with logins but require you to use a key name of their choice
            // If this key leaks to the outside world, it still can't be (ab)used to login with GW2BLTC.com or similar sites
            Assert.Equal("GW2SDK-Full", actual.Name);
        }

        [Fact]
        [Trait("Feature",  "Tokens")]
        [Trait("Category", "Integration")]
        public void Permissions_ShouldHaveExpectedPermissions()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<SubtokenInfo>(_fixture.SubtokenInfoJson, settings);

            var expected = Enum.GetValues(typeof(Permission)).Cast<Permission>().ToHashSet();

            Assert.Equal(expected, actual.Permissions.ToHashSet());
        }

        [Fact]
        [Trait("Feature",  "Tokens")]
        [Trait("Category", "Integration")]
        public void IssuedAt_ShouldHaveExpectedIssueTime()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<SubtokenInfo>(_fixture.SubtokenInfoJson, settings);

            // Ensure that the IssuedAt date is close to the Date response header
            // Allow up to 1 minute of delays to compensate for latency issues
            var low = _fixture.CreatedSubtokenDate - TimeSpan.FromMinutes(1);
            Assert.InRange(actual.IssuedAt, low, _fixture.CreatedSubtokenDate);
        }

        [Fact]
        [Trait("Feature",  "Tokens")]
        [Trait("Category", "Integration")]
        public void ExpiresAt_ShouldHaveExpectedExpirationTime()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<SubtokenInfo>(_fixture.SubtokenInfoJson, settings);

            Assert.Equal(_fixture.ExpiresAt, actual.ExpiresAt);
        }

        [Fact]
        [Trait("Feature",  "Tokens")]
        [Trait("Category", "Integration")]
        public void Urls_ShouldBeExpectedUrls()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<SubtokenInfo>(_fixture.SubtokenInfoJson, settings);

            Assert.Equal(_fixture.Urls, actual.Urls.Select(url => Uri.UnescapeDataString(url.ToString())).ToList());
        }
    }
}
