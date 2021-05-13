using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Builds
{
    [PublicAPI]
    [DataTransferObject]
    public sealed record Build
    {
        public int Id { get; init; }
    }
}
