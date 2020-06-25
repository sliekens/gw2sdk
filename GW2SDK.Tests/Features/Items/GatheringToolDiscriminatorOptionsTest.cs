﻿using System.Linq;
using GW2SDK.Items.Impl;
using GW2SDK.Tests.Features.Items.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Items
{
    [Collection(nameof(ItemDbCollection))]
    public class GatheringToolDiscriminatorOptionsTest
    {
        public GatheringToolDiscriminatorOptionsTest(ItemFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly ItemFixture _fixture;

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public void It_supports_every_known_type_name()
        {
            var sut = new GatheringToolDiscriminatorOptions();

            var expected = _fixture.Db.GetGatheringToolTypeNames().ToHashSet();

            var actual = sut.GetDiscriminatedTypes().Select(x => x.TypeName).ToHashSet();

            Assert.Equal(expected, actual);
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public void It_can_create_objects_of_each_supported_type()
        {
            var sut = new GatheringToolDiscriminatorOptions();

            var actual = sut.GetDiscriminatedTypes().Select(x => x.Type).ToList();

            Assert.All(actual, type => Assert.IsType(type, sut.CreateInstance(type)));
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public void It_only_creates_objects_that_extend_the_correct_base_type()
        {
            var sut = new GatheringToolDiscriminatorOptions();

            var actual = sut.GetDiscriminatedTypes().Select(x => x.Type).ToList();

            Assert.All(actual, type => Assert.IsAssignableFrom(sut.BaseType, sut.CreateInstance(type)));
        }
    }
}
