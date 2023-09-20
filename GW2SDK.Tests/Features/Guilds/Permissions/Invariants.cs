using GuildWars2.Guilds.Permissions;

namespace GuildWars2.Tests.Features.Guilds.Permissions;

internal static class Invariants
{
    internal static void Has_id(this GuildPermissionSummary actual) =>
        Assert.True(Enum.IsDefined(typeof(GuildPermission), actual.Id));

    internal static void Has_name(this GuildPermissionSummary actual) =>
        Assert.NotEmpty(actual.Name);

    internal static void Has_description(this GuildPermissionSummary actual) =>
        Assert.NotEmpty(actual.Description);
}
