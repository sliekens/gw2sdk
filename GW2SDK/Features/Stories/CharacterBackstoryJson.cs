using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Stories;

[PublicAPI]
public static class CharacterBackstoryJson
{
    public static CharacterBackstory GetCharacterBackstory(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<string> backstory = new("backstory");

        foreach (var member in json.EnumerateObject())  
        {
            if (member.NameEquals(backstory.Name))
            {
                backstory.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new CharacterBackstory
        {
            Backstory = backstory.SelectMany(entry => entry.GetStringRequired()).ToList()
        };
    }
}
