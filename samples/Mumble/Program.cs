using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2;
using GuildWars2.Exploration.Maps;
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
Console.CancelKeyPress += (_, args) =>
{
    cts.Cancel();
    args.Cancel = true; // don't terminate the app
};

using var httpClient = new HttpClient();
var gw2 = new Gw2Client(httpClient);
HashSet<Map> maps = await gw2.Maps.GetMaps(cancellationToken: cts.Token);
HashSet<Specialization> specializations =
    await gw2.Specializations.GetSpecializations(cancellationToken: cts.Token);

var gameObserver = new GameReporter();
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

while (!cts.IsCancellationRequested)
{
    await Task.Delay(100);
}
