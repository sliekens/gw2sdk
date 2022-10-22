using GW2SDK.Guilds.Permissions;
using Xunit;

namespace GW2SDK.Tests.Features.Guilds;

internal static class Invariants
{
    internal static void Has_id(this GuildPermissionSummary actual) => Assert.NotEmpty(actual.Id);

    internal static void Has_name(this GuildPermissionSummary actual) =>
        Assert.NotEmpty(actual.Name);

    internal static void Has_description(this GuildPermissionSummary actual) =>
        Assert.NotEmpty(actual.Description);
}
