using GW2SDK.Annotations;

namespace GW2SDK.Tokens
{
    [PublicAPI]
    [Inheritable]
    [DataTransferObject]
    public record TokenInfo
    {
        public string Id { get; init; } = "";

        public string Name { get; init; } = "";

        public Permission[] Permissions { get; init; } = new Permission[0];
    }
}
