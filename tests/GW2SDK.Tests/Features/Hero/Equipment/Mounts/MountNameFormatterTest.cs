using GuildWars2.Hero.Equipment.Mounts;

namespace GuildWars2.Tests.Features.Hero.Equipment.Mounts;

public class MountNameFormatterTest
{
    [Test]
    public async Task Mount_names_can_be_formatted_as_text()
    {
#if NET
        MountName[] mounts = Enum.GetValues<MountName>();
#else
        IEnumerable<MountName> mounts = Enum.GetValues(typeof(MountName)).Cast<MountName>();
#endif
        using (Assert.Multiple())
        {
            foreach (MountName mountName in mounts)
            {
                string actual = MountNameFormatter.FormatMountName(mountName);
                await Assert.That(actual).IsNotEmpty();
            }
        }
    }
}
