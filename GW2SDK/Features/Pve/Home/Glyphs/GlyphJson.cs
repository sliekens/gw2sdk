using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Pve.Home.Decorations;

internal static class GlyphJson
{
    public static Glyph GetGlyph(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember itemId = "item_id";
        RequiredMember slot = "slot";

        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (itemId.Match(member))
            {
                itemId = member;
            }
            else if (slot.Match(member))
            {
                slot = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Glyph
        {
            Id = id.Map(static (in JsonElement value) => value.GetStringRequired()),
            ItemId = itemId.Map(static (in JsonElement value) => value.GetInt32()),
            Slot = slot.Map(static (in JsonElement value) => value.GetEnum<CollectionBox>())
        };
    }
}
