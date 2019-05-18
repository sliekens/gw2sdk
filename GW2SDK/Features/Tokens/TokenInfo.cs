using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Tokens
{
    [DataTransferObject]
    public sealed class TokenInfo
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public Permission[] Permissions { get; set; }
    }
}
