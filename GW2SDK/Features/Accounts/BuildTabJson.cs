using System;
using System.Text.Json;
using GW2SDK.BuildStorage;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Accounts;

[PublicAPI]
public static class BuildTabJson
{
    public static BuildTab GetBuildTab(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> tab = new("tab");
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
            else if (member.NameEquals("is_active"))
            {
                // Ignore this because you should only use ActiveBuildTab
                // => player.BuildTabs.Single(tab => tab.Tab == player.ActiveBuildTab);

                // Otherwise you have to update two objects when the active tab changes and you can't do that atomically
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new BuildTab
        {
            Tab = tab.GetValue(),
            Build = build.Select(value => value.GetBuild(missingMemberBehavior))
        };
    }
}
