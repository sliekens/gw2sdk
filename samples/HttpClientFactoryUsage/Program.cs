using System;
using System.Drawing;
using GuildWars2;
using GuildWars2.Colors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pastel;

var host = Host.CreateApplicationBuilder(args);

// Use AddHttpClient to register the Gw2Client with the HttpClientFactory
host.Services.AddHttpClient<Gw2Client>(
    httpClient =>
    {
        // Here you can further configure the HttpClient
        // e.g. you can set a different base address and a different timeout
        httpClient.BaseAddress = BaseAddress.DefaultUri;
        httpClient.Timeout = TimeSpan.FromSeconds(100);
    }
);

var app = host.Build();

var gw2 = app.Services.GetRequiredService<Gw2Client>();

var colors = await gw2.Dyes.GetColors();

foreach (var dye in colors.Value)
{
    PrintRow(dye.Name, dye.Cloth.Rgb, dye.Leather.Rgb, dye.Metal.Rgb);
}

void PrintRow(string name, Color cloth, Color leather, Color metal)
{
    app.Services.GetRequiredService<ILogger<Dye>>()
        .LogInformation(
            $"{name,-30} {"     ".Pastel(cloth).PastelBg(cloth),-25} {"     ".Pastel(leather).PastelBg(leather),-25} {"     ".Pastel(metal).PastelBg(metal),-25}"
        );
}
