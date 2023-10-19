using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.BuildStorage;

[PublicAPI]
public static class BuildTabJson
{
    public static BuildTab GetBuildTab(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember tab = new("tab");
        RequiredMember isActive = new("is_active");
        RequiredMember build = new("build");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(tab.Name))
            {
                tab = member;
            }
            else if (member.NameEquals(build.Name))
            {
                build = member;
            }
            else if (member.NameEquals(isActive.Name))
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
            Tab = tab.Select(value => value.GetInt32()),
            IsActive = isActive.Select(value => value.GetBoolean()),
            Build = build.Select(value => value.GetBuild(missingMemberBehavior))
        };
    }
}
