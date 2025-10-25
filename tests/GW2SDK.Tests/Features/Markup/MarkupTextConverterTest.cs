using GuildWars2.Markup;

namespace GuildWars2.Tests.Features.Markup;

public class MarkupTextConverterTest
{
    [Test]
    public void Describe_End_of_Dragons_Launch_Supply_Drop_Requisition()
    {
        string input = "Use this to sign your account up for weekly End of Dragons Launch Supply " + "Drops. Receive the first drop immediately and three more between now and " + "March 21.<br><br>First Drop:<br>A Red Crane Weapon Choice, 5 Black Lion " + "Chest Keys, a Golden Black Lion Chest Key, and 5 <REDACTED> Dye Kits.<br>" + "Second Drop:<br>A Black Lion Backpack and Glider Combo Voucher, an End of " + "Dragons Expedition Board, and 2 Heroic Boosters.<br>Third Drop:<br>A Black " + "Lion Outfit Voucher, a Black Lion Weapon Voucher, 2 Fine Black Lion Dye " + "Canisters—Blue, 2 Fine Black Lion Dye Canisters—Green, 2 Fine Black Lion " + "Dye Canisters—Red, and 2 Fine Black Lion Dye Canisters—Yellow.<br>Fourth " + "Drop:<br>A Gold Essence Weapon Choice, 2 Guaranteed Wardrobe Unlocks, and " + "2 Black Lion Miniature Claim Tickets.";
        string actual = MarkupConverter.ToPlainText(input);
        Assert.Equal("""
            Use this to sign your account up for weekly End of Dragons Launch Supply Drops. Receive the first drop immediately and three more between now and March 21.

            First Drop:
            A Red Crane Weapon Choice, 5 Black Lion Chest Keys, a Golden Black Lion Chest Key, and 5  Dye Kits.
            Second Drop:
            A Black Lion Backpack and Glider Combo Voucher, an End of Dragons Expedition Board, and 2 Heroic Boosters.
            Third Drop:
            A Black Lion Outfit Voucher, a Black Lion Weapon Voucher, 2 Fine Black Lion Dye Canisters—Blue, 2 Fine Black Lion Dye Canisters—Green, 2 Fine Black Lion Dye Canisters—Red, and 2 Fine Black Lion Dye Canisters—Yellow.
            Fourth Drop:
            A Gold Essence Weapon Choice, 2 Guaranteed Wardrobe Unlocks, and 2 Black Lion Miniature Claim Tickets.
            """, actual);
    }
}
