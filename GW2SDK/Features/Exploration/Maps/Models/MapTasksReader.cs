using System;
using System.Collections.Generic;
using System.Text.Json;
using GW2SDK.Exploration.Tasks;
using JetBrains.Annotations;

namespace GW2SDK.Exploration.Maps;

[PublicAPI]
public static class MapTasksReader
{
    public static Dictionary<int, MapTask> GetMapTasks(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        Dictionary<int, MapTask> tasks = new();
        foreach (var member in json.EnumerateObject())
        {
            if (int.TryParse(member.Name, out var id))
            {
                tasks[id] = member.Value.GetMapTask(missingMemberBehavior);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return tasks;
    }
}
