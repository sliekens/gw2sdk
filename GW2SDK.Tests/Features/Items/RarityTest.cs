﻿using System;
using System.Linq;
using GW2SDK.Enums;
using GW2SDK.Tests.Features.Items.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Items
{
    [Collection(nameof(ItemDbCollection))]
    public class RarityTest
    {
        public RarityTest(ItemFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly ItemFixture _fixture;

        [Fact]
        [Trait("Feature",    "Items")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Rarity_can_be_created_from_json()
        {
            var expected = _fixture.Db.GetItemRarities().ToHashSet();

            var actual = Enum.GetNames(typeof(Rarity)).ToHashSet();

            Assert.Superset(expected, actual);
        }
    }
}
