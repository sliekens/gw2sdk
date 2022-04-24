using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Accounts.Home.Cats;

[PublicAPI]
[DataTransferObject]
public sealed record Cat
{
    public int Id { get; init; }

    public string Hint { get; init; } = "";
}
