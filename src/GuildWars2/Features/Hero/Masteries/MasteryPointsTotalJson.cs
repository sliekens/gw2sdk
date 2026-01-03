using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Masteries;

internal static class MasteryPointsTotalJson
{
    public static MasteryPointsTotal GetMasteryPointsTotal(this in JsonElement json)
    {
        RequiredMember region = "region";
        RequiredMember spent = "spent";
        RequiredMember earned = "earned";

        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (region.Match(member))
            {
                region = member;
            }
            else if (spent.Match(member))
            {
                spent = member;
            }
            else if (earned.Match(member))
            {
                earned = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new MasteryPointsTotal
        {
            Region = region.Map(static (in value) =>
                {
                    // For some reason the API now returns story journal names instead of the region names as it was originally designed
                    // As a workaround, map the story journal names to the region names
                    if (value.ValueEquals("Central Tyria"))
                    {
                        return MasteryRegionName.Tyria;
                    }

                    if (value.ValueEquals("Heart of Thorns"))
                    {
                        return MasteryRegionName.Maguuma;
                    }

                    if (value.ValueEquals("Path of Fire"))
                    {
                        return MasteryRegionName.Desert;
                    }

                    if (value.ValueEquals("Icebrood Saga"))
                    {
                        return MasteryRegionName.Tundra;
                    }

                    if (value.ValueEquals("End of Dragons"))
                    {
                        return MasteryRegionName.Jade;
                    }

                    if (value.ValueEquals("Secrets of the Obscure"))
                    {
                        return MasteryRegionName.Sky;
                    }

                    // No matches? Could be a new region, or maybe they fixed the API to use the correct region names
                    return value.GetEnum<MasteryRegionName>();
                }
            ),
            Spent = spent.Map(static (in value) => value.GetInt32()),
            Earned = earned.Map(static (in value) => value.GetInt32())
        };
    }
}
