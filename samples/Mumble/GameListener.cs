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
) : IHostedService
{
	private IDisposable? subscription;

	public Task StartAsync(CancellationToken cancellationToken)
	{
		cancellationToken.ThrowIfCancellationRequested();

		// Subscribe to the GameLink observable
		// I recommend using Rx (System.Reactive) instead of implementing IObserver<T> yourself
		subscription = gameLink.Subscribe(OnNext, OnError, OnCompleted);
		return Task.CompletedTask;
	}

    public Task StopAsync(CancellationToken cancellationToken)
	{
		// Dispose the subscription to stop receiving realtime game information
		subscription?.Dispose();
		return Task.CompletedTask;
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

		// The idea is to write to the console in-place instead of creating scrolling text
		// but clearing the console each time creates flickering
		if (gameTick.UiTick % 100 != 0)
		{
			Console.SetCursorPosition(0, 0);
		}
		else
		{
			Console.Clear();
		}

		PrintHeader("Technical Information");
		Print("Build ID", gameTick.Context.BuildId.ToString("N0"));
		Print("Process ID", gameTick.Context.ProcessId.ToString("N0"));
		Print("World ID", $"{identity.WorldId} ({currentWorld})");
		Print("World population", world?.Population);
		Print("World region", world?.Region);
		Print("World language", world?.Language.CultureInfo.DisplayName);
		Print("Shard ID", gameTick.Context.ShardId.ToString("X8"));
		Print("Instance", gameTick.Context.Instance.ToString("X8"));
		Print("Server IP", gameTick.Context.ServerAddress.Address);
		Print("Tick", gameTick.UiTick.ToString("N0"));
		Console.WriteLine();

		PrintHeader("User Interface");
		Print("UI size", identity.UiSize);
		Print("Game has focus", gameTick.Context.UiState.HasFlag(UiState.GameHasFocus));
		Print("Textbox has focus", gameTick.Context.UiState.HasFlag(UiState.TextboxHasFocus));
		Console.WriteLine();

		PrintHeader("Character Information");
		Print("Name", identity.Name);
		Print("Race", identity.Race);
		Print("Profession", identity.Profession);
		Print("Specialization", $"{currentSpecialization} (ID: {identity.SpecializationId})");
		Print("Squad leader", identity.Commander);
		Print("In combat", gameTick.Context.UiState.HasFlag(UiState.IsInCombat));
		Print("Current mount", gameTick.Context.Mount);
		Console.WriteLine();

		PrintHeader("Competitive Games");
		Print(
			"Competitive game type",
			gameTick.Context.UiState.HasFlag(UiState.IsInCompetitiveGameMode)
		);
		Print("Team color", currentTeamColor);
		Console.WriteLine();

		PrintHeader("World Map Coordinates");
		Print(
			"Compass location",
			gameTick.Context.UiState.HasFlag(UiState.IsCompassTopRight)
				? "top-right"
				: "bottom-right"
		);
		Print(
			"Compass dimensions",
			$"Width = {gameTick.Context.CompassWidth:N0} Height = {gameTick.Context.CompassHeight:N0}"
		);
		Print(
			"Compass orientation",
			gameTick.Context.UiState.HasFlag(UiState.DoesCompassHaveRotationEnabled)
				? "rotate"
				: "static"
		);
		Print("World map is open", gameTick.Context.UiState.HasFlag(UiState.IsMapOpen));
		Print("Compass/world map scale", gameTick.Context.MapScale.ToString("N"));
		Print(
			"Map center",
			$"X = {gameTick.Context.MapCenterX,10:N} Y = {gameTick.Context.MapCenterY,10:N}"
		);
		Print(
			"Player position",
			$"X = {gameTick.Context.PlayerX,10:N} Y = {gameTick.Context.PlayerY,10:N}"
		);
		Console.WriteLine();

		PrintHeader("Positional Information");
		Print("Map ID", $"{identity.MapId} ({currentMap}, {gameTick.Context.MapType})");
		Print(
			"Avatar position",
			$"X = {gameTick.AvatarPosition.X,10:N} Y = {gameTick.AvatarPosition.Y,10:N} Z = {gameTick.AvatarPosition.Z,10:N}"
		);
		Print(
			"Avatar orientation",
			$"X = {gameTick.AvatarFront.X,10:N} Y = {gameTick.AvatarFront.Y,10:N} Z = {gameTick.AvatarFront.Z,10:N}"
		);
		Print(
			"Avatar top",
			$"X = {gameTick.AvatarTop.X,10:N} Y = {gameTick.AvatarTop.Y,10:N} Z = {gameTick.AvatarTop.Z,10:N}"
		);
		Console.WriteLine();

		PrintHeader("Camera Information");
		Print("Field of fiew", identity.FieldOfView);
		Print(
			"Camera coordinates",
			$"X = {gameTick.CameraPosition.X,10:N} Y = {gameTick.CameraPosition.Y,10:N} Z = {gameTick.CameraPosition.Z,10:N}"
		);
		Print(
			"Camera orientation",
			$"X = {gameTick.CameraFront.X,10:N} Y = {gameTick.CameraFront.Y,10:N} Z = {gameTick.CameraFront.Z,10:N}"
		);
		Print(
			"Camera top",
			$"X = {gameTick.CameraTop.X,10:N} Y = {gameTick.CameraTop.Y,10:N} Z = {gameTick.CameraTop.Z,10:N}"
		);
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

	private static void PrintHeader(string header)
	{
		var formatted = $"\u2b51\u2b51\u2b51 {header} \u2b51\u2b51\u2b51"
			.Pastel("#FFD700")
			.PastelBg("#00008B");
		Console.WriteLine(formatted);
	}

	private static void Print(string name, bool value)
	{
		Print(name, value ? "✔️" : "❌");
	}

	private static void Print<T>(string name, T value)
	{
		var formatted = $"{name,-30} : {value}";
		Console.WriteLine(formatted.PadRight(Console.WindowWidth - 1));
	}
}
