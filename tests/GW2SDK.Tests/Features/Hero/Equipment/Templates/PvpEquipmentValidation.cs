using GuildWars2.Hero.Equipment.Templates;

namespace GuildWars2.Tests.Features.Hero.Equipment.Templates;

internal static class PvpEquipmentValidation
{
    public static void Validate(PvpEquipment pvpEquipment)
    {
        if (pvpEquipment.AmuletId.HasValue)
        {
            Assert.True(pvpEquipment.AmuletId > 0);
        }

        if (pvpEquipment.RuneId.HasValue)
        {
            Assert.True(pvpEquipment.RuneId > 0);
        }

        Assert.NotNull(pvpEquipment.SigilIds);
        Assert.All(
            pvpEquipment.SigilIds,
            sigilId =>
            {
                if (sigilId.HasValue)
                {
                    Assert.True(sigilId > 0);
                }
            }
        );
    }
}
