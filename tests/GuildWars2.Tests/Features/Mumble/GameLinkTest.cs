using System.Net;
using System.Runtime.Versioning;

using GuildWars2.Hero.Equipment.Mounts;
using GuildWars2.Mumble;

using TUnit.Core.Exceptions;

namespace GuildWars2.Tests.Features.Mumble;

[Explicit]
public class GameLinkTest
{
    [Before(Test)]
    public void EnsureWindows()
    {
        if (!GameLink.IsSupported())
        {
            throw new SkipTestException("Test requires Windows");
        }
    }

    [Test]
    [SupportedOSPlatform("windows")]
    public async Task Name_can_be_read_from_Mumble_link()
    {
        using GameLink sut = GameLink.Open();
        GameTick actual = sut.GetSnapshot();
        await Assert.That(actual.Name).IsEqualTo("Guild Wars 2");
    }

    [Test]
    [SupportedOSPlatform("windows")]
    public async Task The_link_is_self_updating()
    {
        await using GameLink sut = GameLink.Open();
        GameLinkTestObserver actual = new();
        sut.Subscribe(actual);
        await actual.WaitHandle.WaitAsync(TimeSpan.FromSeconds(3), TimeProvider.System, TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual.Last.UiTick != actual.First.UiTick).IsTrue().Because("GameLink should not re-publish the same tick twice");
    }

    [Test]
    [SupportedOSPlatform("windows")]
    public async Task The_link_provides_context()
    {
        using GameLink sut = GameLink.Open();
        GameTick gameTick = sut.GetSnapshot();
        await Assert.That(gameTick.Context.BuildId).IsGreaterThan((uint)100_000).Because("Game build should be over 100,000");
        IPEndPoint server = gameTick.Context.ServerAddress;
        await Assert.That(server.ToString()).IsNotEmpty();
        if (gameTick.Context.IsMounted)
        {
            await Assert.That(Enum.IsDefined(typeof(MountName), gameTick.Context.Mount)).IsTrue();
        }

        // Port is not specified
        await Assert.That(server.Port).IsEqualTo(0);
    }

    [Test]
    [SupportedOSPlatform("windows")]
    public async Task The_link_provides_identity()
    {
        using GameLink sut = GameLink.Open();
        Identity? actual = sut.GetSnapshot().GetIdentity();
        await Assert.That(actual).IsNotNull()
            .And.Member(a => a.Name, m => m.IsNotEmpty());
#if NET
        await Assert.That(Enum.IsDefined(actual!.UiSize)).IsTrue();
#else
        await TUnit.Assertions.Assert.That(Enum.IsDefined(typeof(UiSize), actual!.UiSize)).IsTrue();
#endif
    }
}
