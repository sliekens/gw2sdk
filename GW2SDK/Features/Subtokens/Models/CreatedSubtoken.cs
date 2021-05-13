using GW2SDK.Annotations;

namespace GW2SDK.Subtokens
{
    [PublicAPI]
    [DataTransferObject]
    public sealed record CreatedSubtoken
    {
        public string Subtoken { get; init; } = "";
    }
}
