using GuildWars2.Items;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Items;

[Feature("Items")]
[NotInParallel("Items")]
public class LegacySecondarySuffix
{
    // Older schema versions of the /v2/items endpoint report a (always empty)
    // 'secondary_suffix_item_id' member for armor, trinkets and back items.
    // These item types only have a single upgrade slot, but the extra member
    // must not cause deserialization to fail. See issue #507.
    [Test]
    public async Task Is_ignored_on_non_weapon_equipment()
    {
        using JsonLinesHttpMessageHandler handler = new("Data/items-legacy-secondary-suffix.jsonl");
        using HttpClient httpClient = new(handler);
        Gw2Client sut = new(httpClient);

        var count = 0;
        await foreach ((Item actual, MessageContext _) in sut.Items.GetItemsBulk(cancellationToken: TestContext.Current!.Execution.CancellationToken))
        {
            count++;
            await Assert.That(actual is IUpgradable).IsTrue();
            var upgradable = (IUpgradable)actual;
            await Assert.That(upgradable.SecondarySuffixItemId).IsNull();
            await Assert.That(upgradable.UpgradeSlotCount <= 1).IsTrue();
        }

        // The fixture contains one of each affected item subtype.
        await Assert.That(count).IsEqualTo(11);
    }
}
