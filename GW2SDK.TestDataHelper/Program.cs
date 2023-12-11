using System.IO.Compression;
using System.Text;
using GuildWars2;
using GuildWars2.TestDataHelper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using Spectre.Console;

var outDir = args[0];

Directory.CreateDirectory(outDir);

var appBuilder = Host.CreateApplicationBuilder(args);

appBuilder.Logging.ClearProviders();

var httpClientBuilder = appBuilder.Services.AddHttpClient<Gw2Client>(
        static httpClient =>
        {
            httpClient.Timeout = TimeSpan.FromSeconds(600);
        }
    )
    .ConfigurePrimaryHttpMessageHandler(
        () => new SocketsHttpHandler
        {
            // Creating a new connection shouldn't take more than 10 seconds
            ConnectTimeout = TimeSpan.FromSeconds(10),
            PooledConnectionLifetime = TimeSpan.FromMinutes(5),
            MaxConnectionsPerServer = 1000
        }
    );

httpClientBuilder.AddTypedClient<JsonAchievementService>()
    .AddTypedClient<JsonItemService>()
    .AddTypedClient<JsonRecipeService>()
    .AddTypedClient<JsonSkinService>();

httpClientBuilder.AddResilienceHandler(
    "api.guildwars2.com",
    resiliencePipelineBuilder =>
    {
        resiliencePipelineBuilder.AddRetry(Gw2Resiliency.RetryStrategy);
        resiliencePipelineBuilder.AddHedging(Gw2Resiliency.HedgingStrategy);
    }
);

var app = appBuilder.Build();

try
{
    await AnsiConsole.Progress()
        .StartAsync(
            async ctx =>
            {
                var achievements = ctx.AddTask("Downloading achievements.");
                await using (var file =
                    CreateTextCompressed(Path.Combine(outDir, "achievements.json.gz")))
                {
                    var service = app.Services.GetRequiredService<JsonAchievementService>();
                    var documents =
                        await service.GetAllJsonAchievements(new ProgressAdapter(achievements));
                    foreach (var document in documents)
                    {
                        await file.WriteLineAsync(document);
                    }
                }

                var items = ctx.AddTask("Downloading items.");
                await using (var file = CreateTextCompressed(Path.Combine(outDir, "items.json.gz")))
                {
                    var service = app.Services.GetRequiredService<JsonItemService>();
                    var documents = await service.GetAllJsonItems(new ProgressAdapter(items));
                    foreach (var document in documents)
                    {
                        await file.WriteLineAsync(document);
                    }
                }

                var recipes = ctx.AddTask("Downloading recipes.");
                await using (var file =
                    CreateTextCompressed(Path.Combine(outDir, "recipes.json.gz")))
                {
                    var service = app.Services.GetRequiredService<JsonRecipeService>();
                    var documents = await service.GetAllJsonRecipes(new ProgressAdapter(recipes));
                    foreach (var document in documents)
                    {
                        await file.WriteLineAsync(document);
                    }
                }

                var skins = ctx.AddTask("Downloading skins.");
                await using (var file = CreateTextCompressed(Path.Combine(outDir, "skins.json.gz")))
                {
                    var service = app.Services.GetRequiredService<JsonSkinService>();
                    var documents = await service.GetAllJsonSkins(new ProgressAdapter(skins));
                    foreach (var document in documents)
                    {
                        await file.WriteLineAsync(document);
                    }
                }
            }
        );
}
catch (Exception crash)
{
    AnsiConsole.WriteException(crash);
}

static StreamWriter CreateTextCompressed(string path)
{
    return new StreamWriter(
        new GZipStream(File.OpenWrite(path), CompressionMode.Compress),
        Encoding.UTF8
    );
}
