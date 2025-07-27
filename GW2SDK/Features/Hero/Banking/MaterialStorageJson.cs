using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Banking;

internal static class MaterialStorageJson
{
    public static MaterialStorage GetMaterialStorage(this in JsonElement json)
    {
        return new MaterialStorage
        {
            Materials = json.GetList(static (in JsonElement value) => value.GetMaterialSlot())
        };
    }
}
