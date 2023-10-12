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
        RequiredMember<int> tab = new("tab");
        RequiredMember<bool> isActive = new("is_active");
        RequiredMember<Build> build = new("build");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(tab.Name))
            {
                tab.Value = member.Value;
            }
            else if (member.NameEquals(build.Name))
            {
                build.Value = member.Value;
            }
            else if (member.NameEquals(isActive.Name))
            {
                isActive.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new BuildTab
        {
            Tab = tab.GetValue(),
            IsActive = isActive.GetValue(),
            Build = build.Select(value => value.GetBuild(missingMemberBehavior))
        };
    }
}
