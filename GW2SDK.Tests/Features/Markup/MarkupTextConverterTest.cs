using GuildWars2.Markup;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Markup;

public class MarkupTextConverterTest
{
    [Fact]
    public async Task Describe_End_of_Dragons_Launch_Supply_Drop_Requisition()
    {
        var gw2 = Composer.Resolve<Gw2Client>();
        var item = await gw2.Items.GetItemById(97418).ValueOnly();

        var lexer = new MarkupLexer();
        var parser = new MarkupParser();
        var converter = new MarkupTextConverter();

        var actual = converter.Convert(parser.Parse(lexer.Tokenize(item.Description)));

        Assert.Equal(
            """
            Use this to sign your account up for weekly End of Dragons Launch Supply Drops. Receive the first drop immediately and three more between now and March 21.

            First Drop:
            A Red Crane Weapon Choice, 5 Black Lion Chest Keys, a Golden Black Lion Chest Key, and 5  Dye Kits.
            Second Drop:
            A Black Lion Backpack and Glider Combo Voucher, an End of Dragons Expedition Board, and 2 Heroic Boosters.
            Third Drop:
            A Black Lion Outfit Voucher, a Black Lion Weapon Voucher, 2 Fine Black Lion Dye Canisters—Blue, 2 Fine Black Lion Dye Canisters—Green, 2 Fine Black Lion Dye Canisters—Red, and 2 Fine Black Lion Dye Canisters—Yellow.
            Fourth Drop:
            A Gold Essence Weapon Choice, 2 Guaranteed Wardrobe Unlocks, and 2 Black Lion Miniature Claim Tickets.
            """,
            actual
        );
    }
}
