﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Builds;

internal static class SelectedPetsJson
{
    public static SelectedPets GetSelectedPets(this in JsonElement json)
    {
        RequiredMember terrestrial = "terrestrial";
        RequiredMember aquatic = "aquatic";

        foreach (var member in json.EnumerateObject())
        {
            if (terrestrial.Match(member))
            {
                terrestrial = member;
            }
            else if (aquatic.Match(member))
            {
                aquatic = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        var (terrestrial1, terrestrial2) = terrestrial.Map(static (in JsonElement values) => values.GetPetIds());
        var (aquatic1, aquatic2) = aquatic.Map(static (in JsonElement values) => values.GetPetIds());
        return new SelectedPets
        {
            Terrestrial1 = terrestrial1,
            Terrestrial2 = terrestrial2,
            Aquatic1 = terrestrial1,
            Aquatic2 = terrestrial2
        };
    }
}
