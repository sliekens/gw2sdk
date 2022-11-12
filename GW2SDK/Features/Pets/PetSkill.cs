using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Pets;

[PublicAPI]
[DataTransferObject]
public sealed record PetSkill
{
    public required int Id { get; init; }
}
