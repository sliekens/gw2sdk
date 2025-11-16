using GuildWars2.Hero.Equipment.Templates;

namespace GuildWars2.Tests.Features.Hero.Equipment.Templates;

internal static class PvpEquipmentValidation
{
    public static async Task Validate(PvpEquipment pvpEquipment)
    {
        if (pvpEquipment.AmuletId.HasValue)
        {
            await Assert.That(pvpEquipment.AmuletId.Value).IsGreaterThan(0);
        }

        if (pvpEquipment.RuneId.HasValue)
        {
            await Assert.That(pvpEquipment.RuneId.Value).IsGreaterThan(0);
        }

        await Assert.That(pvpEquipment.SigilIds).IsNotNull();
        using (Assert.Multiple())
        {
            foreach (int? sigilId in pvpEquipment.SigilIds)
            {
                if (sigilId.HasValue)
                {
                    await Assert.That(sigilId.Value).IsGreaterThan(0);
                }
            }
        }
    }
}
