using System;
using GW2SDK.Mounts;
using Xunit;

namespace GW2SDK.Tests.Features.Mounts;

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
