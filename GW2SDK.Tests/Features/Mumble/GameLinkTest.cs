using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Mumble;
using Xunit;

#pragma warning disable CA1416
namespace GuildWars2.Tests.Features.Mumble;

public class GameLinkTestObserver : IObserver<Snapshot>
{
    private readonly TaskCompletionSource<bool> tcs;

    public GameLinkTestObserver(CancellationToken ct)
    {
        tcs = new TaskCompletionSource<bool>();
        ct.Register(tcs.SetCanceled);
    }

    public Task<bool> Handle => tcs.Task;

    public Snapshot First { get; private set; }

    public Snapshot Last { get; private set; }

    public void OnCompleted() => tcs.SetResult(true);

    public void OnError(Exception error) => tcs.SetException(error);

    public void OnNext(Snapshot value)
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
        if (First.UiTick == 0)
        {
            First = value;
        }
        else
        {
            Last = value;
            tcs.TrySetResult(true);
        }
    }
}

public class GameLinkTest
{
    [MumbleLinkFact]
    public void Name_can_be_read_from_Mumble_link()
    {
        using var sut = GameLink.Open();

        var actual = sut.GetSnapshot();

        Assert.Equal("Guild Wars 2", actual.Name);
    }

    [MumbleLinkFact]
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
    public void The_link_provides_context()
    {
        using var sut = GameLink.Open();

        var snapshot = sut.GetSnapshot();
        Assert.True(snapshot.TryGetContext(out var actual));
        Assert.True(actual.BuildId > 100_000, "Game build should be over 100,000");

        var server = actual.GetServerAddress();
        Assert.NotEmpty(server.ToString());

        // Port is not specified
        Assert.Equal(0, server.Port);
    }

    [MumbleLinkFact]
    public void The_link_provides_identity()
    {
        using var sut = GameLink.Open();

        var snapshot = sut.GetSnapshot();
        Assert.True(snapshot.TryGetIdentity(out var actual, MissingMemberBehavior.Error));
        Assert.NotNull(actual);
    }
}

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
