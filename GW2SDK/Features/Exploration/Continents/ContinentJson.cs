﻿using System.Drawing;
using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Exploration.Continents;

[PublicAPI]
public static class ContinentJson
{
    public static Continent GetContinent(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<string> name = new("name");
        RequiredMember<Size> continentDimensions = new("continent_dims");
        RequiredMember<int> minZoom = new("min_zoom");
        RequiredMember<int> maxZoom = new("max_zoom");
        RequiredMember<int> floors = new("floors");
        RequiredMember<int> id = new("id");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(continentDimensions.Name))
            {
                continentDimensions.Value = member.Value;
            }
            else if (member.NameEquals(minZoom.Name))
            {
                minZoom.Value = member.Value;
            }
            else if (member.NameEquals(maxZoom.Name))
            {
                maxZoom.Value = member.Value;
            }
            else if (member.NameEquals(floors.Name))
            {
                floors.Value = member.Value;
            }
            else if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Continent
        {
            Id = id.GetValue(),
            Name = name.GetValue(),
            ContinentDimensions =
                continentDimensions.Select(value => value.GetDimensions(missingMemberBehavior)),
            MinZoom = minZoom.GetValue(),
            MaxZoom = maxZoom.GetValue(),
            Floors = floors.SelectMany(value => value.GetInt32())
        };
    }
}
