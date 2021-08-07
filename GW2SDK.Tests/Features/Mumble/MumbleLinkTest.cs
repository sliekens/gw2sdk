using System.IO;
using System.IO.MemoryMappedFiles;
using System.Threading.Tasks;
using GW2SDK.Mumble;
using Xunit;

#pragma warning disable CA1416
namespace GW2SDK.Tests.Features.Mumble
{
    public class MumbleLinkTest
    {
        [MumbleLinkFact]
        public void Name_can_be_read_from_Mumble_link()
        {
            using var sut = MumbleLink.Open();

            var actual = sut.GetSnapshot();

            Assert.Equal("Guild Wars 2", actual.name);
        }

        [MumbleLinkFact]
        public async Task The_link_is_self_updating()
        {
            using var sut = MumbleLink.Open();

            var first = sut.GetSnapshot();

            // The link should be updated every frame; you should safely be able to read from it 50 times a second (every 20ms)
            await Task.Delay(20);

            var second = sut.GetSnapshot();

            Assert.True(second.UiTick > first.UiTick, "MumbleLink should be self-updating");
        }

        [MumbleLinkFact]
        public void The_link_provides_context()
        {
            using var sut = MumbleLink.Open();

            var snapshot = sut.GetSnapshot();
            var actual = snapshot.GetContext();

            Assert.True(actual.BuildId > 100_000, "Game build should be over 100,000");
        }
    }

    public class MumbleLinkFact : FactAttribute
    {
        public MumbleLinkFact()
        {
            if (!MumbleLink.IsSupported())
            {
                Skip = "MumbleLink is not supported on the current platform.";
                return;
            }

            try
            {
                using var file = MemoryMappedFile.OpenExisting("MumbleLink", MemoryMappedFileRights.Read);
            }
            catch (FileNotFoundException)
            {
                Skip = "The MumbleLink is not initialized. Start Mumble -and- Guild Wars 2 before running this test.";
            }
        }
    }
}
