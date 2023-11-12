using System.IO.MemoryMappedFiles;

namespace GuildWars2.Tests.Features.Mumble;

public sealed class MumbleLinkFact : FactAttribute
{
    public MumbleLinkFact()
    {
        if (!GameLink.IsSupported())
        {
            Skip = "GameLink is not supported on the current platform.";
            return;
        }

        try
        {
            using var file = MemoryMappedFile.OpenExisting(
                "MumbleLink",
                MemoryMappedFileRights.Read
            );
        }
        catch (FileNotFoundException)
        {
            Skip =
                "The GameLink is not initialized. Start Mumble -and- Guild Wars 2 before running this test.";
        }
    }
}
