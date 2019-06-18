using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Subtokens
{
    [PublicAPI]
    [DataTransferObject]
    public sealed class CreatedSubtoken
    {
        public string Subtoken { get; set; }
    }
}
