using System;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Mumble;

namespace GuildWars2.Tests.Features.Mumble;

public class GameLinkTestObserver : IObserver<GameTick>
{
    private readonly TaskCompletionSource<bool> tcs;

    public GameLinkTestObserver(CancellationToken ct)
    {
        tcs = new TaskCompletionSource<bool>();
        ct.Register(tcs.SetCanceled);
    }

    public Task<bool> Handle => tcs.Task;

    public GameTick First { get; private set; }

    public GameTick Last { get; private set; }

    public void OnCompleted() => tcs.SetResult(true);

    public void OnError(Exception error) => tcs.SetException(error);

    public void OnNext(GameTick value)
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
