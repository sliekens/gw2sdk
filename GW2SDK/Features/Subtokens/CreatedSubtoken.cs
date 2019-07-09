using GW2SDK.Annotations;

namespace GW2SDK.Subtokens
{
    [PublicAPI]
    [DataTransferObject]
    public sealed class CreatedSubtoken
    {
        public string Subtoken { get; set; }
    }
}
