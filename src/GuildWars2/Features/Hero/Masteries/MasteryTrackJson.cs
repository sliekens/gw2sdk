using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Masteries;

internal static class MasteryTrackJson
{
    public static MasteryTrack GetMasteryTrack(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember requirement = "requirement";
        RequiredMember order = "order";
        RequiredMember background = "background";
        RequiredMember region = "region";
        RequiredMember levels = "levels";

        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (name.Match(member))
            {
                name = member;
            }
            else if (requirement.Match(member))
            {
                requirement = member;
            }
            else if (order.Match(member))
            {
                order = member;
            }
            else if (background.Match(member))
            {
                background = member;
            }
            else if (region.Match(member))
            {
                region = member;
            }
            else if (levels.Match(member))
            {
                levels = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        string backgroundString = background.Map(static (in value) => value.GetStringRequired());
#pragma warning disable CS0618 // Type or member is obsolete
        return new MasteryTrack
        {
            Id = id.Map(static (in value) => value.GetInt32()),
            Name = name.Map(static (in value) => value.GetStringRequired()),
            Requirement = requirement.Map(static (in value) => value.GetStringRequired()),
            Order = order.Map(static (in value) => value.GetInt32()),
            BackgroundHref = backgroundString,
            BackgroundUrl = new Uri(backgroundString),
            Region = region.Map(static (in value) => value.GetEnum<MasteryRegionName>()),
            Masteries = levels.Map(static (in values) =>
                values.GetList(static (in value) => value.GetMastery())
            )
        };
#pragma warning restore CS0618 // Type or member is obsolete
    }
}
