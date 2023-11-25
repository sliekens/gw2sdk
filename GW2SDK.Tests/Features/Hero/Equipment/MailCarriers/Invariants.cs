using GuildWars2.Hero.Equipment.MailCarriers;

namespace GuildWars2.Tests.Features.Hero.Equipment.MailCarriers;

internal static class Invariants
{
    internal static void Id_is_positive(this MailCarrier actual) =>
        Assert.InRange(actual.Id, 1, int.MaxValue);

    internal static void Non_default_carriers_can_be_unlocked(this MailCarrier actual)
    {
        if (actual.Flags.SingleOrDefault() == MailCarrierFlag.Default)
        {
            Assert.Empty(actual.UnlockItems);
        }
        else
        {
            Assert.NotEmpty(actual.UnlockItems);
        }
    }

    internal static void Name_is_not_empty(this MailCarrier actual) => Assert.NotEmpty(actual.Name);

    internal static void Order_is_not_negative(this MailCarrier actual) =>
        Assert.InRange(actual.Order, 0, 1000);

    internal static void Icon_is_not_empty(this MailCarrier actual) => Assert.NotEmpty(actual.IconHref);
}
