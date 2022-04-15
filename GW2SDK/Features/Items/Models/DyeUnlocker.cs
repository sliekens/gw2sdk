using JetBrains.Annotations;

namespace GW2SDK.Items.Models;

[PublicAPI]
public sealed record DyeUnlocker : Unlocker
{
    public int ColorId { get; init; }
}
