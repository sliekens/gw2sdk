using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Masteries;

internal static class MasteryPointsTotalJson
{
    public static MasteryPointsTotal GetMasteryPointsTotal(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember region = "region";
        RequiredMember spent = "spent";
        RequiredMember earned = "earned";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == region.Name)
            {
                region = member;
            }
            else if (member.Name == spent.Name)
            {
                spent = member;
            }
            else if (member.Name == earned.Name)
            {
                earned = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MasteryPointsTotal
        {
            Region = region.Map(
                value =>
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
                    return value.GetEnum<MasteryRegionName>(missingMemberBehavior);
                }
            ),
            Spent = spent.Map(value => value.GetInt32()),
            Earned = earned.Map(value => value.GetInt32())
        };
    }
}
