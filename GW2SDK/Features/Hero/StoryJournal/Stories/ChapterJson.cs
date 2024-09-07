using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.StoryJournal.Stories;

internal static class ChapterJson
{
    public static Chapter GetChapter(this JsonElement json)
    {
        RequiredMember name = "name";

        foreach (var member in json.EnumerateObject())
        {
            if (name.Match(member))
            {
                name = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Chapter { Name = name.Map(static value => value.GetStringRequired()) };
    }
}
