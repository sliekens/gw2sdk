using GuildWars2;
using GuildWars2.Mumble;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

if (!GameLink.IsSupported())
{
    throw new PlatformNotSupportedException(
        "MumbleLink only works on Windows due to the use of named memory-mapped files."
    );
}

// Setup dependency injection and logging
var host = Host.CreateApplicationBuilder(args);
host.Services.AddHttpClient<Gw2Client>();
host.Logging.AddSimpleConsole(
    options =>
    {
        options.SingleLine = true;
        options.TimestampFormat = "HH:mm:ss.fff ";
        options.UseUtcTimestamp = true;
    }
);

var app = host.Build();
var appLifetime = app.Services.GetRequiredService<IHostApplicationLifetime>();
var logger = app.Services.GetRequiredService<ILogger<GameLink>>();

// The game link provides the IDs of the current map and specialization,
// which you can use to look up the map and specialization names (among other things) from the API
var gw2Client = app.Services.GetRequiredService<Gw2Client>();

// Dictionary of map ID => MapSummary
var maps = await gw2Client.Exploration
    .GetMapSummaries(cancellationToken: appLifetime.ApplicationStopping)
    .AsDictionary(static map => map.Id)
    .ValueOnly();

// Dictionary of specialization ID => Specialization
var specializations = await gw2Client.Hero.Builds.GetSpecializations()
    .AsDictionary(static specialization => specialization.Id)
    .ValueOnly();

// Initialize the shared memory link
var refreshInterval = TimeSpan.FromSeconds(1);
var gameLink = GameLink.Open(refreshInterval);

// Subscribe to the GameLink observable
// I recommend using Rx (System.Reactive) instead of implementing IObserver<T> yourself
gameLink.Subscribe(OnNext, OnError, OnCompleted, appLifetime.ApplicationStopping);

void OnNext(GameTick gameTick)
{
    // OnNext(GameTick) is executed whenever the game client updates the shared memory
    var identity = gameTick.GetIdentity();
    if (identity is null)
    {
        // Identity might be null due to invalid JSON
        // (happens very rarely due to concurrent reads/writes)
        return;
    }

    var currentSpecialization = "no specialization";
    if (specializations.TryGetValue(identity.SpecializationId, out var specialization))
    {
        currentSpecialization = specialization.Name;
    }

    var currentMap = "an unknown map";
    if (maps.TryGetValue(identity.MapId, out var map))
    {
        currentMap = map.Name;
    }

    var title = $"the {identity.Race} {identity.Profession}";
    if (!gameTick.Context.UiState.HasFlag(UiState.GameHasFocus))
    {
        logger.LogInformation(
            "[{UiTick}] {Name}, {Title} ({Specialization}) is afk",
            gameTick.UiTick,
            identity.Name,
            title,
            currentSpecialization
        );
    }
    else if (gameTick.Context.UiState.HasFlag(UiState.TextboxHasFocus))
    {
        logger.LogInformation(
            "[{UiTick}] {Name}, {Title} ({Specialization}) is typing",
            gameTick.UiTick,
            identity.Name,
            title,
            currentSpecialization
        );
    }
    else if (gameTick.Context.UiState.HasFlag(UiState.IsMapOpen))
    {
        logger.LogInformation(
            "[{UiTick}] {Name}, {Title} ({Specialization}) is looking at the map",
            gameTick.UiTick,
            identity.Name,
            title,
            currentSpecialization
        );
    }
    else if (gameTick.Context.UiState.HasFlag(UiState.IsInCombat))
    {
        logger.LogInformation(
            "[{UiTick}] {Name}, {Title} ({Specialization}) is in combat",
            gameTick.UiTick,
            identity.Name,
            title,
            currentSpecialization
        );
    }
    else
    {
        var transport = gameTick.Context.IsMounted ? gameTick.Context.Mount.ToString() : "foot";
        logger.LogInformation(
            "[{UiTick}] {Name}, {Title} ({Specialization}) is on {Transport} in {Map} ({Type}), Position: {{ Latitude = {X}, Longitude = {Z}, Elevation = {Y} }}",
            gameTick.UiTick,
            identity.Name,
            title,
            currentSpecialization,
            transport,
            currentMap,
            gameTick.Context.MapType.ToString(),
            gameTick.AvatarPosition.X,
            gameTick.AvatarPosition.Z,
            gameTick.AvatarPosition.Y
        );
    }
}

void OnError(Exception err)
{
    // OnError(Exception) is executed when there is an internal error while reading from the shared memory
    // or when your OnNext callback throws an exception, it will also end up here
    logger.LogError(err, "Something went wrong.");
    app.StopAsync();
}

void OnCompleted()
{
    // OnCompleted callback runs when you unsubscribe or when the GameLink is being disposed
    logger.LogInformation("Stopping on tick {UiTick}", gameLink.GetSnapshot().UiTick);
}

await app.RunAsync();
