using System;
using GuildWars2.Mounts;
using Xunit;

namespace GuildWars2.Tests.Features.Mounts;

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
