using System.Runtime.InteropServices;

namespace GuildWars2.Tests.TestInfrastructure;

public sealed class OSPlatformFactAttribute : FactAttribute
{
    public OSPlatformFactAttribute(string platformName)
    {
        var platform = OSPlatform.Create(platformName.ToUpperInvariant());
        if (!RuntimeInformation.IsOSPlatform(platform))
        {
            Skip = $"Test requires {platformName}";
        }
    }
}
