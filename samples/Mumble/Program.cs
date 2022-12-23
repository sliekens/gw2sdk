using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2;
using GuildWars2.Exploration.Maps;
using GuildWars2.Mumble;
using GuildWars2.Specializations;

Console.OutputEncoding = Encoding.UTF8;
CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en");
CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en");

if (!GameLink.IsSupported())
{
    Console.WriteLine("This sample is only supported on Windows!");
    return;
}

var cts = new CancellationTokenSource();
Console.CancelKeyPress += (sender, args) =>
{
    cts.Cancel();
    args.Cancel = true; // don't terminate the app
};

using var httpClient = new HttpClient();
var gw2 = new Gw2Client(httpClient);
HashSet<Map> maps = await gw2.Maps.GetMaps(cancellationToken: cts.Token);
HashSet<Specialization> specializations =
    await gw2.Specializations.GetSpecializations(cancellationToken: cts.Token);

var gameObserver = new GameObserver(cts.Token);
foreach (var map in maps)
{
    gameObserver.Maps[map.Id] = map;
}

foreach (var specialization in specializations)
{
    gameObserver.Specializations[specialization.Id] = specialization;
}

using var gameLink = GameLink.Open();
using var subscription = gameLink.Subscribe(gameObserver);

try
{
    await gameObserver.Observations.ConfigureAwait(false);
}
catch (OperationCanceledException)
{
    Console.WriteLine("Ctrl+C");
}
finally
{
    Console.WriteLine("Goodbye.");
}

public class GameObserver : IObserver<Snapshot>
{
    private readonly TaskCompletionSource tcs = new();

    public GameObserver(CancellationToken cancellationToken)
    {
        cancellationToken.Register(() => tcs.TrySetCanceled());
    }

    public Task Observations => tcs.Task;

    public Dictionary<int, Map> Maps { get; } = new();

    public Dictionary<int, Specialization> Specializations { get; } = new();

    public void OnCompleted() => tcs.TrySetResult();

    public void OnError(Exception error) => tcs.TrySetException(error);

    public void OnNext(Snapshot snapshot) => ThreadPool.QueueUserWorkItem(CallBack, snapshot);

    private void CallBack(object state)
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

        var specialization = "no specialization";
        if (Specializations.TryGetValue(identity.SpecializationId, out var found))
        {
            specialization = found.Name;
        }

        var map = Maps[identity.MapId];
        Console.WriteLine(
            "[{0}] {1}, the {2} {3} ({4}) is {5} on {6} in {7}, Position: {{ Right = {8}, Up = {9}, Front = {10} }}",
            snapshot.UiTick,
            identity.Name,
            identity.Race,
            identity.Profession,
            specialization,
            context.UiState.HasFlag(UiState.IsInCombat) ? "fighting" : "traveling",
            context.IsMounted ? context.GetMount() : "foot",
            map.Name,
            pos[0],
            pos[1],
            pos[2]
        );
    }
}
