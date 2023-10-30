using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Banking;

internal static class MaterialStorageJson
{
    public static MaterialStorage GetMaterialStorage(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    ) =>
        new () { Materials = json.GetList(value => value.GetMaterialSlot(missingMemberBehavior)) };
}
