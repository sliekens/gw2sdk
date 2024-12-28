using GuildWars2.Mumble;

namespace GuildWars2.Tests.Features.Mumble;

public class GameLinkTestObserver : IObserver<GameTick>
{
    private readonly AutoResetEvent are = new(false);

    public Exception? Error { get; private set; }

    public GameTick First { get; private set; }

    public GameTick Last { get; private set; }

    public WaitHandle WaitHandle => are;

    public void OnCompleted() => are.Set();

    public void OnError(Exception? error)
    {
        Error = error;
        are.Set();
    }

    public void OnNext(GameTick value)
    {
        if (First.UiTick == 0)
        {
            First = value;
        }
        else if (value.UiTick > First.UiTick)
        {
            Last = value;
            are.Set();
        }
    }
}
