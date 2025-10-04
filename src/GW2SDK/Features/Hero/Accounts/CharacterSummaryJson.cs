using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Accounts;

internal static class CharacterSummaryJson
{
    public static CharacterSummary GetCharacterSummary(this in JsonElement json)
    {
        RequiredMember name = "name";
        RequiredMember race = "race";
        RequiredMember gender = "gender";
        RequiredMember profession = "profession";
        RequiredMember level = "level";
        OptionalMember guild = "guild";
        RequiredMember age = "age";
        RequiredMember lastModified = "last_modified";
        RequiredMember created = "created";
        RequiredMember deaths = "deaths";
        NullableMember title = "title";

        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (name.Match(member))
            {
                name = member;
            }
            else if (race.Match(member))
            {
                race = member;
            }
            else if (gender.Match(member))
            {
                gender = member;
            }
            else if (profession.Match(member))
            {
                profession = member;
            }
            else if (level.Match(member))
            {
                level = member;
            }
            else if (guild.Match(member))
            {
                guild = member;
            }
            else if (age.Match(member))
            {
                age = member;
            }
            else if (lastModified.Match(member))
            {
                lastModified = member;
            }
            else if (created.Match(member))
            {
                created = member;
            }
            else if (deaths.Match(member))
            {
                deaths = member;
            }
            else if (title.Match(member))
            {
                title = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new CharacterSummary
        {
            Name = name.Map(static (in value) => value.GetStringRequired()),
            Race = race.Map(static (in value) => value.GetEnum<RaceName>()),
            BodyType = gender.Map(static (in value) => value.GetEnum<BodyType>()),
            Level = level.Map(static (in value) => value.GetInt32()),
            GuildId = guild.Map(static (in value) => value.GetString()) ?? "",
            Profession = profession.Map(static (in value) => value.GetEnum<ProfessionName>()),
            Age = age.Map(static (in value) => TimeSpan.FromSeconds(value.GetDouble())),
            LastModified = lastModified.Map(static (in value) => value.GetDateTimeOffset()),
            Created = created.Map(static (in value) => value.GetDateTimeOffset()),
            Deaths = deaths.Map(static (in value) => value.GetInt32()),
            TitleId = title.Map(static (in value) => value.GetInt32())
        };
    }
}
