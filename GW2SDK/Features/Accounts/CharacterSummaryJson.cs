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
        RequiredMember name = new("name");
        RequiredMember race = new("race");
        RequiredMember gender = new("gender");
        RequiredMember profession = new("profession");
        RequiredMember level = new("level");
        OptionalMember guild = new("guild");
        RequiredMember age = new("age");
        RequiredMember lastModified = new("last_modified");
        RequiredMember created = new("created");
        RequiredMember deaths = new("deaths");
        NullableMember title = new("title");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(race.Name))
            {
                race.Value = member.Value;
            }
            else if (member.NameEquals(gender.Name))
            {
                gender.Value = member.Value;
            }
            else if (member.NameEquals(profession.Name))
            {
                profession.Value = member.Value;
            }
            else if (member.NameEquals(level.Name))
            {
                level.Value = member.Value;
            }
            else if (member.NameEquals(guild.Name))
            {
                guild.Value = member.Value;
            }
            else if (member.NameEquals(age.Name))
            {
                age.Value = member.Value;
            }
            else if (member.NameEquals(lastModified.Name))
            {
                lastModified.Value = member.Value;
            }
            else if (member.NameEquals(created.Name))
            {
                created.Value = member.Value;
            }
            else if (member.NameEquals(deaths.Name))
            {
                deaths.Value = member.Value;
            }
            else if (member.NameEquals(title.Name))
            {
                title.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new CharacterSummary
        {
            Name = name.Select(value => value.GetStringRequired()),
            Race = race.Select(value => value.GetEnum<RaceName>(missingMemberBehavior)),
            Gender = gender.Select(value => value.GetEnum<Gender>(missingMemberBehavior)),
            Level = level.Select(value => value.GetInt32()),
            GuildId = guild.Select(value => value.GetString()) ?? "",
            Profession = profession.Select(value => value.GetEnum<ProfessionName>(missingMemberBehavior)),
            Age = age.Select(value => TimeSpan.FromSeconds(value.GetDouble())),
            LastModified = lastModified.Select(value => value.GetDateTimeOffset()),
            Created = created.Select(value => value.GetDateTimeOffset()),
            Deaths = deaths.Select(value => value.GetInt32()),
            TitleId = title.Select(value => value.GetInt32())
        };
    }
}
