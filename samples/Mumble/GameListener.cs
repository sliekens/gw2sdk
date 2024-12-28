using System.Runtime.Versioning;
using GuildWars2;
using GuildWars2.Mumble;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pastel;

namespace Mumble;

[SupportedOSPlatform("windows")]
public sealed class GameListener(
    ILogger<GameListener> logger,
    GameLink gameLink,
    ReferenceData referenceData
) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Task.Yield lets the application start first
        // Otherwise "Application Started" is logged after we already cleared the console
        await Task.Yield();

        Console.Clear();

        // Subscribe to the GameLink observable
        // I recommend using Rx (System.Reactive) instead of implementing IObserver<T> yourself
        gameLink.Subscribe(OnNext, OnError, OnCompleted, stoppingToken);
    }

    private void OnNext(GameTick gameTick)
    {
        // OnNext(GameTick) is executed whenever the game client updates the shared memory
        var identity = gameTick.GetIdentity();
        if (identity is null)
        {
            // Identity might be null due to invalid JSON
            // (happens very rarely due to concurrent reads/writes)
            return;
        }

        var currentSpecialization = "none";
        if (referenceData.Specializations.TryGetValue(
                identity.SpecializationId,
                out var specialization
            ))
        {
            currentSpecialization = specialization.Name;
        }

        var currentMap = "unknown";
        if (referenceData.Maps.TryGetValue(identity.MapId, out var map))
        {
            currentMap = map.Name;
        }

        var currentWorld = "unknown";
        if (referenceData.Worlds.TryGetValue((int)identity.WorldId, out var world))
        {
            currentWorld = world.Name;
        }

        var currentTeamColor = "none";
        if (referenceData.Colors.TryGetValue(identity.TeamColorId, out var teamColor))
        {
            currentTeamColor = "     ".PastelBg(teamColor.Cloth.Rgb);
        }
        else if (identity.TeamColorId != 0)
        {
            currentTeamColor = "unknown";
        }

        Console.SetCursorPosition(0, 0);

        Print("Build", gameTick.Context.BuildId.ToString("N0"));
        Print("Process", gameTick.Context.ProcessId.ToString("N0"));
        Print(
            "World",
            $"{currentWorld} (Language: {world?.Language.CultureInfo.DisplayName} Region: {world?.Region}, Population: {world?.Population}, ID: {identity.WorldId})"
        );
        Print("Server IP", gameTick.Context.ServerAddress.Address);
        Print("Shard ID", gameTick.Context.ShardId.ToString("X8"));
        Print("Name", identity.Name);
        Print("Race", identity.Race);
        Print("Profession", identity.Profession);
        Print("Specialization", $"{currentSpecialization} (ID: {identity.SpecializationId})");
        Print("Map", $"{currentMap} ({gameTick.Context.MapType}, ID: {identity.MapId})");
        Print("Instance", gameTick.Context.Instance.ToString("X8"));
        Print("Current mount", gameTick.Context.Mount);
        Print("UI Size", identity.UiSize);
        Print(
            "Compass dimensions",
            $"Width = {gameTick.Context.CompassWidth:N0} Height = {gameTick.Context.CompassHeight:N0}"
        );
        Print(
            "Compass can rotate",
            gameTick.Context.UiState.HasFlag(UiState.DoesCompassHaveRotationEnabled)
        );
        Print(
            "Compass docked top-right",
            gameTick.Context.UiState.HasFlag(UiState.IsCompassTopRight)
        );
        Print("Compass/world map scale", gameTick.Context.MapScale.ToString("N"));
        Print("World map is open", gameTick.Context.UiState.HasFlag(UiState.IsMapOpen));
        Print(
            "Player is in PvP game type",
            gameTick.Context.UiState.HasFlag(UiState.IsInCompetitiveGameMode)
        );
        Print("Team color", currentTeamColor);
        Print("Player is in combat", gameTick.Context.UiState.HasFlag(UiState.IsInCombat));
        Print("Player is squad leader", identity.Commander);
        Print("Textbox has focus", gameTick.Context.UiState.HasFlag(UiState.TextboxHasFocus));
        Print("Game has focus", gameTick.Context.UiState.HasFlag(UiState.GameHasFocus));
        Print("Field of View", identity.FieldOfView);
        Print(
            "Map Center",
            $"X = {gameTick.Context.MapCenterX,10:N} Y = {gameTick.Context.MapCenterY,10:N}"
        );
        Print(
            "Player Coordinates",
            $"X = {gameTick.Context.PlayerX,10:N} Y = {gameTick.Context.PlayerY,10:N}"
        );
        Print(
            "Avatar Coordinates",
            $"X = {gameTick.AvatarPosition.X,10:N} Y = {gameTick.AvatarPosition.Y,10:N} Z = {gameTick.AvatarPosition.Z,10:N}"
        );
        Print(
            "Avatar Orientation",
            $"X = {gameTick.AvatarFront.X,10:N} Y = {gameTick.AvatarFront.Y,10:N} Z = {gameTick.AvatarFront.Z,10:N}"
        );
        Print(
            "Avatar Top",
            $"X = {gameTick.AvatarTop.X,10:N} Y = {gameTick.AvatarTop.Y,10:N} Z = {gameTick.AvatarTop.Z,10:N}"
        );
        Print(
            "Camera Coordinates",
            $"X = {gameTick.CameraPosition.X,10:N} Y = {gameTick.CameraPosition.Y,10:N} Z = {gameTick.CameraPosition.Z,10:N}"
        );
        Print(
            "Camera Orientation",
            $"X = {gameTick.CameraFront.X,10:N} Y = {gameTick.CameraFront.Y,10:N} Z = {gameTick.CameraFront.Z,10:N}"
        );
        Print(
            "Camera Top",
            $"X = {gameTick.CameraTop.X,10:N} Y = {gameTick.CameraTop.Y,10:N} Z = {gameTick.CameraTop.Z,10:N}"
        );
        Print("Tick", gameTick.UiTick.ToString("N0"));
    }

    private void OnError(Exception err)
    {
        // OnError(Exception) is executed when there is an internal error while reading from the shared memory
        // or when your OnNext callback throws an exception, it will also end up here
        logger.LogError(err, "Something went wrong.");
    }

    private void OnCompleted()
    {
        // OnCompleted() is executed when you unsubscribe or when the GameLink is disposed
        // You have one last chance to get a snapshot of the game state
        // This is a good place to to save the final state
        // or to clean up resources used by your application
        // WARNING: OnCompleted is not called when the GameLink stops due to an error
        logger.LogInformation("Stopping on tick {UiTick}", gameLink.GetSnapshot().UiTick);
    }

    private static void Print(string name, bool value)
    {
        Print(name, value ? "✔️" : "❌");
    }

    // Helper method to print static text instead of scrolling text
    private static void Print<T>(string name, T value)
    {
        var formatted = $"{name,-30} : {value}";
        Console.WriteLine(formatted.PadRight(Console.WindowWidth - 1));
    }
}
