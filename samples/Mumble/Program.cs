using System;
using System.Globalization;
using System.Text;
using System.Threading;
using GW2SDK;
using GW2SDK.Json;
using GW2SDK.Mumble;

namespace Mumble;

internal class Program : IObserver<Snapshot>
{
    static Program()
    {
        Console.OutputEncoding = Encoding.UTF8;
        CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en");
        CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en");
    }

    public void OnCompleted() => Console.WriteLine("Goodbye.");

    public void OnError(Exception error) => Console.Error.WriteLine(error.ToString());

    public void OnNext(Snapshot snapshot) => ThreadPool.QueueUserWorkItem(CallBack, snapshot);

    private static void Main(string[] args)
    {
        var cts = new CancellationTokenSource();

        Console.CancelKeyPress += (_, _) =>
        {
            cts.Cancel();
        };

        new Program().RealMain(cts.Token);
    }

    private void RealMain(CancellationToken cancellationToken)
    {
        if (!OperatingSystem.IsWindows())
        {
            Console.WriteLine("This sample is only supported on Windows!");
            return;
        }

        using var mumble = GameLink.Open();

        using var subscription = mumble.Subscribe(this);

        WaitHandle.WaitAll(new[] { cancellationToken.WaitHandle });
    }

    private static void CallBack(object state)
    {
        var snapshot = (Snapshot)state;
        var pos = snapshot.AvatarPosition;

        if (!snapshot.TryGetIdentity(out var identity, MissingMemberBehavior.Error))
        {
            return;
        }

        if (!snapshot.TryGetContext(out var context))
        {
            return;
        }

        Console.WriteLine(
            "Update {0}: {1}, the {2} {3} is {4} on {5} in Map: {6}, Position: {{ Right = {7}, Up = {8}, Front = {9} }}",
            snapshot.UiTick,
            identity.Name,
            identity.Race,
            identity.Profession,
            context.UiState.HasFlag(UiState.IsInCombat) ? "fighting" : "traveling",
            context.IsMounted ? context.GetMount() : "foot",
            identity.MapId,
            pos[0],
            pos[1],
            pos[2]
            );
    }
}
