using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.StoryJournal.Stories;

internal static class ChapterJson
{
    public static Chapter GetChapter(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember name = "name";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == name.Name)
            {
                name = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Chapter { Name = name.Map(value => value.GetStringRequired()) };
    }
}
