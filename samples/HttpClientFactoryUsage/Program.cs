﻿using System.Drawing;
using GuildWars2;
using GuildWars2.Hero.Dyes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pastel;

var appBuilder = Host.CreateApplicationBuilder(args);

// Set up the HTTP client factory
appBuilder.Services.AddHttpClient<Gw2Client>();

var app = appBuilder.Build();

// Obtain a Gw2Client from the service provider, which uses the HTTP client factory
var gw2 = app.Services.GetRequiredService<Gw2Client>();

// Some demo code to print dye colors, using Pastel to colorize the console output
foreach (var dye in (await gw2.Hero.Dyes.GetColors()).Value)
{
    PrintColor(dye.Name, dye.Cloth.Rgb, dye.Leather.Rgb, dye.Metal.Rgb);
}

// Helper method to print a row of colors
void PrintColor(string name, Color cloth, Color leather, Color metal)
{
    app.Services.GetRequiredService<ILogger<Dye>>()
        .LogInformation(
            $"{name,-30} {"     ".Pastel(cloth).PastelBg(cloth),-25} {"     ".Pastel(leather).PastelBg(leather),-25} {"     ".Pastel(metal).PastelBg(metal),-25}"
        );
}
