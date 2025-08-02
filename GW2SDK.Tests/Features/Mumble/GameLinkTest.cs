using System.Runtime.Versioning;

using GuildWars2.Hero.Equipment.Mounts;
using GuildWars2.Mumble;

namespace GuildWars2.Tests.Features.Mumble;

public class GameLinkTest
{
    [Fact]
    [SupportedOSPlatform("windows")]
    public void Name_can_be_read_from_Mumble_link()
    {
        Assert.SkipUnless(GameLink.IsSupported(), "Test requires Windows");
        using var sut = GameLink.Open();

        var actual = sut.GetSnapshot();

        Assert.Equal("Guild Wars 2", actual.Name);
    }

    [Fact]
    [SupportedOSPlatform("windows")]
    public async Task The_link_is_self_updating()
    {
        Assert.SkipUnless(GameLink.IsSupported(), "Test requires Windows");
        await using var sut = GameLink.Open();

        GameLinkTestObserver actual = new();

        sut.Subscribe(actual);

        await actual.WaitHandle.WaitAsync(
            TimeSpan.FromSeconds(3),
            TimeProvider.System,
            TestContext.Current.CancellationToken
        );
        Assert.True(
            actual.Last.UiTick != actual.First.UiTick,
            "GameLink should not re-publish the same tick twice"
        );
    }

    [Fact]
    [SupportedOSPlatform("windows")]
    public void The_link_provides_context()
    {
        Assert.SkipUnless(GameLink.IsSupported(), "Test requires Windows");
        using var sut = GameLink.Open();

        var gameTick = sut.GetSnapshot();
        Assert.True(gameTick.Context.BuildId > 100_000, "Game build should be over 100,000");

        var server = gameTick.Context.ServerAddress;
        Assert.NotEmpty(server.ToString());

        if (gameTick.Context.IsMounted)
        {
            Assert.True(Enum.IsDefined(typeof(MountName), gameTick.Context.Mount));
        }

        // Port is not specified
        Assert.Equal(0, server.Port);
    }

    [Fact]
    [SupportedOSPlatform("windows")]
    public void The_link_provides_identity()
    {
        Assert.SkipUnless(GameLink.IsSupported(), "Test requires Windows");
        using var sut = GameLink.Open();

        var actual = sut.GetSnapshot().GetIdentity();

        Assert.NotNull(actual);
        Assert.NotEmpty(actual.Name);
#if NET
        Assert.True(Enum.IsDefined<UiSize>(actual.UiSize));
#else
        Assert.True(Enum.IsDefined(typeof(UiSize), actual.UiSize));
#endif
    }
}
