using System;
using System.Collections.Generic;
using GuildWars2;
using GuildWars2.Exploration.Maps;
using GuildWars2.Mumble;
using GuildWars2.Specializations;

public class GameReporter : IObserver<Snapshot>
{
    public Dictionary<int, Map> Maps { get; } = new();

    public Dictionary<int, Specialization> Specializations { get; } = new();

    public void OnNext(Snapshot snapshot)
    {
        var pos = snapshot.AvatarPosition;

        if (!snapshot.TryGetIdentity(out var identity, MissingMemberBehavior.Error))
        {
            return;
        }

        if (!snapshot.TryGetContext(out var context))
        {
            return;
        }

        var specialization = "no specialization";
        if (Specializations.TryGetValue(identity.SpecializationId, out var found))
        {
            specialization = found.Name;
        }

        var map = Maps[identity.MapId];
        var activity = "traveling";
        if (!context.UiState.HasFlag(UiState.GameHasFocus))
        {
            activity = "afk-ing";
        }
        else if (context.UiState.HasFlag(UiState.TextboxHasFocus))
        {
            activity = "typing";
        }
        else if (context.UiState.HasFlag(UiState.IsMapOpen))
        {
            activity = "looking at the map";
        }
        else if (context.UiState.HasFlag(UiState.IsInCombat))
        {
            activity = "in combat";
        }

        Console.WriteLine(
            "[{0}] {1}, the {2} {3} ({4}) is {5} on {6} in {7}, Position: {{ Right = {8}, Up = {9}, Front = {10} }}",
            snapshot.UiTick,
            identity.Name,
            identity.Race,
            identity.Profession,
            specialization,
            activity,
            context.IsMounted ? context.GetMount() : "foot",
            map.Name,
            pos[0],
            pos[1],
            pos[2]
        );
    }

    public void OnError(Exception error)
    {
    }

    public void OnCompleted()
    {
    }
}
