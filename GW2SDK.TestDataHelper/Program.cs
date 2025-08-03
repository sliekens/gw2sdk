using System.IO.Compression;
using System.Text;

using GuildWars2;
using GuildWars2.TestDataHelper;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Polly;

using Spectre.Console;

string outDir = args[0];

Directory.CreateDirectory(outDir);

HostApplicationBuilder appBuilder = Host.CreateApplicationBuilder(args);

appBuilder.Logging.ClearProviders();

IHttpClientBuilder httpClientBuilder = appBuilder.Services.AddHttpClient<Gw2Client>(static httpClient =>
        {
            httpClient.Timeout = TimeSpan.FromSeconds(600);
        }
    )
    .ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler
    {
        // Creating a new connection shouldn't take more than 10 seconds
        ConnectTimeout = TimeSpan.FromSeconds(10),
        PooledConnectionLifetime = TimeSpan.FromMinutes(15),
        MaxConnectionsPerServer = 1000
    }
    );

httpClientBuilder.AddTypedClient<JsonAchievementService>()
    .AddTypedClient<JsonItemService>()
    .AddTypedClient<JsonRecipeService>()
    .AddTypedClient<JsonSkinService>()
    .AddTypedClient<JsonDecorationsService>();

httpClientBuilder.AddResilienceHandler(
    "Gw2Resiliency",
    resiliencePipelineBuilder =>
    {
        resiliencePipelineBuilder.AddTimeout(Gw2Resiliency.TotalTimeoutStrategy)
            .AddRetry(Gw2Resiliency.RetryStrategy)
            .AddCircuitBreaker(Gw2Resiliency.CircuitBreakerStrategy)
            .AddHedging(Gw2Resiliency.HedgingStrategy)
            .AddTimeout(Gw2Resiliency.AttemptTimeoutStrategy);
    }
);

IHost app = appBuilder.Build();

try
{
    await AnsiConsole.Progress()
        .StartAsync(async ctx =>
            {
                ProgressTask achievements = ctx.AddTask("Downloading achievements.");
                StreamWriter achievementsFile = CreateTextCompressed(Path.Combine(outDir, "achievements.jsonl.gz"));
                await using (achievementsFile.ConfigureAwait(false))
                {
                    JsonAchievementService service = app.Services.GetRequiredService<JsonAchievementService>();
                    ISet<string> documents =
                        await service.GetAllJsonAchievements(new ProgressAdapter(achievements)).ConfigureAwait(false);
                    foreach (string document in documents)
                    {
                        await achievementsFile.WriteLineAsync(document).ConfigureAwait(false);
                    }
                }

                ProgressTask items = ctx.AddTask("Downloading items.");
                StreamWriter itemsFile = CreateTextCompressed(Path.Combine(outDir, "items.jsonl.gz"));
                await using (itemsFile.ConfigureAwait(false))
                {
                    JsonItemService service = app.Services.GetRequiredService<JsonItemService>();
                    ISet<string> documents = await service.GetAllJsonItems(new ProgressAdapter(items)).ConfigureAwait(false);
                    foreach (string document in documents)
                    {
                        await itemsFile.WriteLineAsync(document).ConfigureAwait(false);
                    }
                }

                ProgressTask recipes = ctx.AddTask("Downloading recipes.");
                StreamWriter recipesFile = CreateTextCompressed(Path.Combine(outDir, "recipes.jsonl.gz"));
                await using (recipesFile.ConfigureAwait(false))
                {
                    JsonRecipeService service = app.Services.GetRequiredService<JsonRecipeService>();
                    ISet<string> documents = await service.GetAllJsonRecipes(new ProgressAdapter(recipes)).ConfigureAwait(false);
                    foreach (string document in documents)
                    {
                        await recipesFile.WriteLineAsync(document).ConfigureAwait(false);
                    }
                }

                ProgressTask skins = ctx.AddTask("Downloading skins.");
                StreamWriter skinsFile = CreateTextCompressed(Path.Combine(outDir, "skins.jsonl.gz"));
                await using (skinsFile.ConfigureAwait(false))
                {
                    JsonSkinService service = app.Services.GetRequiredService<JsonSkinService>();
                    ISet<string> documents = await service.GetAllJsonSkins(new ProgressAdapter(skins)).ConfigureAwait(false);
                    foreach (string document in documents)
                    {
                        await recipesFile.WriteLineAsync(document).ConfigureAwait(false);
                    }
                }

                ProgressTask decorations = ctx.AddTask("Downloading decorations.");
                StreamWriter decorationsFile = CreateTextCompressed(Path.Combine(outDir, "decorations.jsonl.gz"));
                await using (decorationsFile.ConfigureAwait(false))
                {
                    JsonDecorationsService service = app.Services.GetRequiredService<JsonDecorationsService>();
                    ISet<string> documents =
                        await service.GetAllJsonDecorations(new ProgressAdapter(decorations)).ConfigureAwait(false);
                    foreach (string document in documents)
                    {
                        await decorationsFile.WriteLineAsync(document).ConfigureAwait(false);
                    }
                }
            }
        ).ConfigureAwait(false);
}
catch (Exception crash)
{
    AnsiConsole.WriteException(crash);
    Environment.Exit(1);
    throw;
}

static StreamWriter CreateTextCompressed(string path)
{
    return new StreamWriter(
        new GZipStream(File.OpenWrite(path), CompressionMode.Compress),
        Encoding.UTF8
    );
}
