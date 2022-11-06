using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Pets;

[PublicAPI]
[DataTransferObject]
public sealed record PetSkill
{
    public int Id { get; init; }
}
