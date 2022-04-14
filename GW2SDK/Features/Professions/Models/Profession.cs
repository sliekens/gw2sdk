using System;
using System.Collections.Generic;

using GW2SDK.Annotations;

using JetBrains.Annotations;

namespace GW2SDK.Professions.Models;

[PublicAPI]
[DataTransferObject]
public sealed record Profession
{
    public ProfessionName Id { get; init; }

    /// <summary>The localized profession name.</summary>
    public string Name { get; init; } = "";

    public int Code { get; init; }

    public string Icon { get; init; } = "";

    public string IconBig { get; init; } = "";

    public IReadOnlyCollection<int> Specializations { get; init; } = Array.Empty<int>();

    public IDictionary<string, WeaponProficiency> Weapons { get; init; } = new Dictionary<string, WeaponProficiency>();

    public IReadOnlyCollection<ProfessionFlag> Flags { get; init; } = Array.Empty<ProfessionFlag>();

    public IReadOnlyCollection<SkillReference> Skills { get; init; } = Array.Empty<SkillReference>();

    public IReadOnlyCollection<Training> Training { get; init; } = Array.Empty<Training>();

    public IDictionary<int, int> SkillsByPalette { get; init; } = new Dictionary<int, int>();
}