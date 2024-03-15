using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Accounts;

internal static class CharacterSummaryJson
{
    public static CharacterSummary GetCharacterSummary(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
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

        foreach (var member in json.EnumerateObject())
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
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new CharacterSummary
        {
            Name = name.Map(value => value.GetStringRequired()),
            Race = race.Map(value => value.GetEnum<RaceName>(missingMemberBehavior)),
            BodyType = gender.Map(value => value.GetEnum<BodyType>(missingMemberBehavior)),
            Level = level.Map(value => value.GetInt32()),
            GuildId = guild.Map(value => value.GetString()) ?? "",
            Profession =
                profession.Map(value => value.GetEnum<ProfessionName>(missingMemberBehavior)),
            Age = age.Map(value => TimeSpan.FromSeconds(value.GetDouble())),
            LastModified = lastModified.Map(value => value.GetDateTimeOffset()),
            Created = created.Map(value => value.GetDateTimeOffset()),
            Deaths = deaths.Map(value => value.GetInt32()),
            TitleId = title.Map(value => value.GetInt32())
        };
    }
}
