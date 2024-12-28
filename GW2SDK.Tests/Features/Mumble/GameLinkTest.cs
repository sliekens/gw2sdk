﻿using System.Runtime.Versioning;
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
    public void The_link_is_self_updating()
    {
        Assert.SkipUnless(GameLink.IsSupported(), "Test requires Windows");
        using var sut = GameLink.Open();

        GameLinkTestObserver actual = new();

        sut.Subscribe(actual);

        if (actual.WaitHandle.WaitOne(30000))
        {
            Assert.True(
                actual.Last.UiTick != actual.First.UiTick,
                "GameLink should not re-publish the same tick twice"
            );
        }
        else
        {
            Assert.Fail("GameLink should push updates to subscribers. This test only works if you are in a map, not in a loading screen etc.");
        }
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
        Assert.True(Enum.IsDefined(typeof(UiSize), actual.UiSize));
    }
}
