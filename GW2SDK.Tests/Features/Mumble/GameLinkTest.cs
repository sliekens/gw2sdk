using System.Runtime.Versioning;
using GuildWars2.Hero.Equipment.Mounts;

namespace GuildWars2.Tests.Features.Mumble;

public class GameLinkTest
{
    [MumbleLinkFact]
    [SupportedOSPlatform("windows")]
    public void Name_can_be_read_from_Mumble_link()
    {
        using var sut = GameLink.Open();

        var actual = sut.GetSnapshot();

        Assert.Equal("Guild Wars 2", actual.Name);
    }

    [MumbleLinkFact]
    [SupportedOSPlatform("windows")]
    public async Task The_link_is_self_updating()
    {
        using var sut = GameLink.Open();

        using CancellationTokenSource cts = new(TimeSpan.FromSeconds(3));

        GameLinkTestObserver actual = new(cts.Token);

        sut.Subscribe(actual);

        try
        {
            var success = await actual.Handle;
            Assert.True(success, "GameLink should push updates to subscribers.");
        }
        catch (TaskCanceledException)
        {
            Assert.True(
                actual.Last.UiTick > actual.First.UiTick,
                "This test only works if you are in a map, not in a loading screen etc."
            );
        }

        Assert.True(actual.Last.UiTick > actual.First.UiTick, "GameLink should be self-updating");
    }

    [MumbleLinkFact]
    [SupportedOSPlatform("windows")]
    public void The_link_provides_context()
    {
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

    [MumbleLinkFact]
    [SupportedOSPlatform("windows")]
    public void The_link_provides_identity()
    {
        using var sut = GameLink.Open();

        var actual = sut.GetSnapshot().GetIdentity();

        Assert.NotNull(actual);
        Assert.NotEmpty(actual.Name);
        Assert.True(Enum.IsDefined(typeof(UiSize), actual.UiSize));
    }
}
