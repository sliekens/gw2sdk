using System.Drawing;
using GuildWars2;
using GuildWars2.Hero.Equipment.Dyes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Pastel;

var appBuilder = Host.CreateApplicationBuilder(args);

// Set up the HTTP client factory
appBuilder.Services.AddHttpClient<Gw2Client>();

// Configure logging
appBuilder.Logging.AddSimpleConsole(options =>
    {
        options.ColorBehavior = LoggerColorBehavior.Enabled;
        options.SingleLine = true;
    }
);

var app = appBuilder.Build();

// Obtain a Gw2Client from the service provider, which uses the HTTP client factory
var gw2 = app.Services.GetRequiredService<Gw2Client>();

// Some demo code to print dye colors, using Pastel to colorize the console output
foreach (var dye in await gw2.Hero.Equipment.Dyes.GetColors().ValueOnly())
{
    PrintColor(dye.Name, dye.Cloth.Rgb, dye.Leather.Rgb, dye.Metal.Rgb);
}

// Helper method to print a row of colors
void PrintColor(string name, Color cloth, Color leather, Color metal)
{
    app.Services.GetRequiredService<ILogger<DyeColor>>()
        .LogInformation(
            "{name,-20} {Cloth,-25} {Leather,-25} {Metal,-25}",
            name,
            "  C  ".Pastel(Invert(cloth)).PastelBg(cloth),
            "  L  ".Pastel(Invert(leather)).PastelBg(leather),
            "  M  ".Pastel(Invert(metal)).PastelBg(metal)
        );

    static Color Invert(Color color)
    {
        return Color.FromArgb(color.ToArgb() ^ 0xffffff);
    }
}
