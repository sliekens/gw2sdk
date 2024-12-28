using GuildWars2.Mumble;

namespace GuildWars2.Tests.Features.Mumble;

public class GameLinkTestObserver : IObserver<GameTick>
{
    private readonly TaskCompletionSource<bool> tcs = new();

    public Exception? Error { get; private set; }

    public GameTick First { get; private set; }

    public GameTick Last { get; private set; }

    public Task WaitHandle => tcs.Task;

    public void OnCompleted()
    {
        tcs.TrySetResult(true);
    }

    public void OnError(Exception error)
    {
        Error = error;
        tcs.TrySetException(error);
    }

    public void OnNext(GameTick value)
    {
        if (First.UiTick == 0)
        {
            First = value;
            Last = value;
        }
        else if (value.UiTick != First.UiTick)
        {
            Last = value;
            tcs.TrySetResult(true);
        }
    }
}
