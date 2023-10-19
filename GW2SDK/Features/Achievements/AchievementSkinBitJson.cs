using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Achievements;

[PublicAPI]
public static class AchievementSkinBitJson
{
    public static AchievementSkinBit GetAchievementSkinBit(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = new("id");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Skin"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
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

        return new AchievementSkinBit { Id = id.Select(value => value.GetInt32()) };
    }
}
