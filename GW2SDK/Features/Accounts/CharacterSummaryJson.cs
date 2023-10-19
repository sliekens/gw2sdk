using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Accounts;

[PublicAPI]
public static class CharacterSummaryJson
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
            if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(race.Name))
            {
                race = member;
            }
            else if (member.NameEquals(gender.Name))
            {
                gender = member;
            }
            else if (member.NameEquals(profession.Name))
            {
                profession = member;
            }
            else if (member.NameEquals(level.Name))
            {
                level = member;
            }
            else if (member.NameEquals(guild.Name))
            {
                guild = member;
            }
            else if (member.NameEquals(age.Name))
            {
                age = member;
            }
            else if (member.NameEquals(lastModified.Name))
            {
                lastModified = member;
            }
            else if (member.NameEquals(created.Name))
            {
                created = member;
            }
            else if (member.NameEquals(deaths.Name))
            {
                deaths = member;
            }
            else if (member.NameEquals(title.Name))
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
            Gender = gender.Map(value => value.GetEnum<Gender>(missingMemberBehavior)),
            Level = level.Map(value => value.GetInt32()),
            GuildId = guild.Map(value => value.GetString()) ?? "",
            Profession = profession.Map(value => value.GetEnum<ProfessionName>(missingMemberBehavior)),
            Age = age.Map(value => TimeSpan.FromSeconds(value.GetDouble())),
            LastModified = lastModified.Map(value => value.GetDateTimeOffset()),
            Created = created.Map(value => value.GetDateTimeOffset()),
            Deaths = deaths.Map(value => value.GetInt32()),
            TitleId = title.Map(value => value.GetInt32())
        };
    }
}
