using GuildWars2.Hero.Equipment.Mounts;

namespace GuildWars2.Tests.Features.Hero.Equipment.Mounts;

public class MountNameFormatterTest
{
    [Fact]
    public static void Mount_names_can_be_formatted_as_text()
    {
        var mounts = (MountName[])Enum.GetValues(typeof(MountName));

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
