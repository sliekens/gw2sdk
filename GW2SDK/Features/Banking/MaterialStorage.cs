using System.Collections.ObjectModel;

namespace GuildWars2.Banking;

/// <summary>The current account's material storage, sorted by in-game order.</summary>
[PublicAPI]
public sealed class MaterialStorage : ReadOnlyCollection<MaterialSlot>
{
    public MaterialStorage(IList<MaterialSlot> list)
        : base(list)
    {
    }
}
