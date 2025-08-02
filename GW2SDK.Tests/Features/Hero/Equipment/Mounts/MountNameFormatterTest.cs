using GuildWars2.Hero.Equipment.Mounts;

namespace GuildWars2.Tests.Features.Hero.Equipment.Mounts;

public class MountNameFormatterTest
{
    [Fact]
    public void Mount_names_can_be_formatted_as_text()
    {
#if NET
        var mounts = Enum.GetValues<MountName>();
#else
        var mounts = Enum.GetValues(typeof(MountName)).Cast<MountName>();
#endif

        Assert.All(
            mounts,
            mountName =>
            {
                var actual = MountNameFormatter.FormatMountName(mountName);
                Assert.NotEmpty(actual);
            }
        );
    }
}
