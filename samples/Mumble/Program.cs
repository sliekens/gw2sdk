using System;
using System.Globalization;
using System.Text;
using GuildWars2;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Mumble;

Console.OutputEncoding = Encoding.UTF8;
CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en");
CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en");

if (!GameLink.IsSupported())
{
    Console.WriteLine("This sample is only supported on Windows!");
    return;
}

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHttpClient<Gw2Client>();
builder.Services.AddHostedService<GameReporter>();
builder.Logging.AddSimpleConsole(
    options =>
    {
        options.SingleLine = true;
        options.TimestampFormat = "HH:mm:ss.fff ";
        options.UseUtcTimestamp = true;
    }
);

var app = builder.Build();

app.Run();
