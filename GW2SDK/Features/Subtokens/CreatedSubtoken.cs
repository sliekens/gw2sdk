using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Subtokens
{
    [DataTransferObject]
    public sealed class CreatedSubtoken
    {
        public string Subtoken { get; set; }
    }
}
