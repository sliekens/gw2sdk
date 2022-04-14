using System;
using System.Collections.Generic;

using GW2SDK.Annotations;

using JetBrains.Annotations;

namespace GW2SDK.Skins.Models;

[PublicAPI]
[Inheritable]
[DataTransferObject]
public record Skin
{
    public int Id { get; init; }

    public string Name { get; init; } = "";

    public string Description { get; init; } = "";

    public IReadOnlyCollection<SkinFlag> Flags { get; init; } = Array.Empty<SkinFlag>();

    public IReadOnlyCollection<SkinRestriction> Restrictions { get; init; } = Array.Empty<SkinRestriction>();

    public Rarity Rarity { get; init; }

    public string? Icon { get; init; }
}