using System.Runtime.InteropServices;

namespace GuildWars2.Tests.TestInfrastructure;

public sealed class OSPlatformFactAttribute : FactAttribute
{
    public OSPlatformFactAttribute(string platformName)
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Create(platformName)))
        {
            Skip = $"Test requires {platformName}";
        }
    }
}
