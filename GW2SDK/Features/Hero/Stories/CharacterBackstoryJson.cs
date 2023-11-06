using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Stories;

internal static class CharacterBackstoryJson
{
    public static CharacterBackstory GetCharacterBackstory(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember backstory = "backstory";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == backstory.Name)
            {
                backstory = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new CharacterBackstory
        {
            Backstory = backstory
                .Map(values => values.GetList(entry => entry.GetStringRequired()))
                .ToList()
        };
    }
}
