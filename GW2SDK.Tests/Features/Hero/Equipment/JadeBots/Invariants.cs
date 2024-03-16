using GuildWars2.Hero.Equipment.JadeBots;

namespace GuildWars2.Tests.Features.Hero.Equipment.JadeBots;

internal static class Invariants
{
    internal static void Has_id(this JadeBotSkin actual) => Assert.True(actual.Id > 0);

    internal static void Has_name(this JadeBotSkin actual) => Assert.NotEmpty(actual.Name);

    internal static void Has_description(this JadeBotSkin actual)
    {
        // Missing descriptionfor Roundtail Dragon
        if (actual.Id == 6)
        {
            Assert.Empty(actual.Description);
        }
        else
        {
            Assert.NotEmpty(actual.Description);
        }
    }

    internal static void Has_unlock_item(this JadeBotSkin actual) =>
        Assert.True(actual.UnlockItemId > 0);
}
