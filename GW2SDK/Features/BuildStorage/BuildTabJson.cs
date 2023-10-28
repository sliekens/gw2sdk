using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.BuildStorage;

internal static class BuildTabJson
{
    public static BuildTab GetBuildTab(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember tab = "tab";
        RequiredMember isActive = "is_active";
        RequiredMember build = "build";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == tab.Name)
            {
                tab = member;
            }
            else if (member.Name == build.Name)
            {
                build = member;
            }
            else if (member.Name == isActive.Name)
            {
                isActive = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new BuildTab
        {
            Tab = tab.Map(value => value.GetInt32()),
            IsActive = isActive.Map(value => value.GetBoolean()),
            Build = build.Map(value => value.GetBuild(missingMemberBehavior))
        };
    }
}
