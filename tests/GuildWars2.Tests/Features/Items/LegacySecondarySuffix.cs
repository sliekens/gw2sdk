using System.Net;
using System.Text;
using System.Text.Json.Nodes;

using GuildWars2.Items;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Items;

[Feature("Items")]
[NotInParallel("Items")]
public class LegacySecondarySuffix
{
    // The API can return the default schema even when a newer schema is requested (#507).
    // Item 78705 was retrieved from https://api.guildwars2.com/v2/items/78705 on 2026-09-15:
    // the default schema returns "24618", while SchemaVersion.Recommended returns 24618.
    [Test]
    public async Task Default_schema_equipment_can_be_enumerated()
    {
        using JsonLinesHttpMessageHandler handler = new("Data/items-legacy-secondary-suffix.jsonl");
        using HttpClient httpClient = new(handler);
        Gw2Client sut = new(httpClient);

        int count = 0;
        await foreach ((Item actual, MessageContext _) in sut.Items.GetItemsBulk(cancellationToken: TestContext.Current!.Execution.CancellationToken))
        {
            count++;
            await Assert.That(actual is IUpgradable).IsTrue();
            IUpgradable upgradable = (IUpgradable)actual;
            if (actual.Id == 78705)
            {
                await Assert.That(actual is Staff).IsTrue();
                await Assert.That(upgradable.SecondarySuffixItemId).IsEqualTo(24618);
                await Assert.That(upgradable.UpgradeSlots[1]).IsEqualTo(24618);
            }
            else
            {
                await Assert.That(upgradable.SecondarySuffixItemId).IsNull();
                await Assert.That(upgradable.UpgradeSlotCount <= 1).IsTrue();
            }
        }

        // All seven armor subtypes, three trinket subtypes, a back item and a staff.
        await Assert.That(count).IsEqualTo(12);
    }

    [Test]
    [Arguments("24618", 24618)]
    [Arguments("\"24618\"", 24618)]
    [Arguments("\"\"", null)]
    [Arguments("null", null)]
    [Arguments(null, null)]
    public async Task Weapons_accept_numeric_and_string_ids(string? value, int? expected)
    {
        string sample = JsonLinesReader.Read("Data/items-legacy-secondary-suffix.jsonl").Last();
        // Exercise every weapon parser, including the base parser when details.type is absent.
        string?[] subtypes =
        [
            "Axe", "Dagger", "Focus", "Greatsword", "Hammer", "Harpoon", "LargeBundle",
            "LongBow", "Mace", "Pistol", "Rifle", "Scepter", "Shield", "ShortBow", "SmallBundle",
            "Speargun", "Staff", "Sword", "Torch", "Toy", "ToyTwoHanded", "Trident", "Warhorn", null
        ];
        foreach (string? subtype in subtypes)
        {
            JsonNode item = JsonNode.Parse(sample)!;
            JsonObject details = item["details"]!.AsObject();
            if (subtype is null)
            {
                details.Remove("type");
            }
            else
            {
                details["type"] = subtype;
            }

            SetSecondarySuffix(details, value);
            using ItemResponseHandler handler = new(item.ToJsonString());
            using HttpClient httpClient = new(handler);
            Gw2Client sut = new(httpClient);
            (Item actual, _) = await sut.Items.GetItemById(78705, cancellationToken: TestContext.Current!.Execution.CancellationToken);
            Weapon weapon = (Weapon)actual;
            await Assert.That(weapon.SecondarySuffixItemId).IsEqualTo(expected);
            await Assert.That(weapon.SuffixItemId).IsEqualTo(24615);
            if (weapon.TwoHanded)
            {
                await Assert.That(weapon.UpgradeSlots[1]).IsEqualTo(expected);
            }
        }
    }

    [Test]
    [Arguments("\"not-a-number\"")]
    [Arguments("\"2147483648\"")]
    [Arguments("\"1.5\"")]
    [Arguments("1.5")]
    [Arguments("true")]
    [Arguments("{}")]
    public async Task Invalid_weapon_ids_remain_incompatible(string value)
    {
        string sample = JsonLinesReader.Read("Data/items-legacy-secondary-suffix.jsonl").Last();
        JsonNode item = JsonNode.Parse(sample)!;
        SetSecondarySuffix(item["details"]!.AsObject(), value);
        using ItemResponseHandler handler = new(item.ToJsonString());
        using HttpClient httpClient = new(handler);
        Gw2Client sut = new(httpClient);
        await Assert.That(async () =>
        {
            await sut.Items.GetItemById(78705, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        }).Throws<InvalidOperationException>()
            .And.Member(exception => exception.Message, message => message.IsEqualTo("Value for 'secondary_suffix_item_id' is incompatible."));
    }

    [Test]
    [Arguments("\"\"")]
    [Arguments("\"24618\"")]
    [Arguments("24618")]
    [Arguments("true")]
    public async Task Non_weapon_equipment_ignores_the_value(string value)
    {
        foreach (string sample in JsonLinesReader.Read("Data/items-legacy-secondary-suffix.jsonl").SkipLast(1))
        {
            // Also exercise ArmorJson and TrinketJson without a subtype discriminator.
            foreach (bool removeSubtype in new[] { false, true })
            {
                JsonNode item = JsonNode.Parse(sample)!;
                JsonObject details = item["details"]!.AsObject();
                if (removeSubtype)
                {
                    details.Remove("type");
                }

                SetSecondarySuffix(details, value);
                using ItemResponseHandler handler = new(item.ToJsonString());
                using HttpClient httpClient = new(handler);
                Gw2Client sut = new(httpClient);
                (Item actual, _) = await sut.Items.GetItemById(item["id"]!.GetValue<int>(), cancellationToken: TestContext.Current!.Execution.CancellationToken);
                IUpgradable upgradable = (IUpgradable)actual;
                await Assert.That(upgradable.SecondarySuffixItemId).IsNull();
                await Assert.That(upgradable.UpgradeSlotCount <= 1).IsTrue();
            }
        }
    }

    private static void SetSecondarySuffix(JsonObject details, string? value)
    {
        if (value is null)
        {
            details.Remove("secondary_suffix_item_id");
        }
        else
        {
            details["secondary_suffix_item_id"] = JsonNode.Parse(value);
        }
    }

    private sealed class ItemResponseHandler(string json) : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            });
        }
    }
}
