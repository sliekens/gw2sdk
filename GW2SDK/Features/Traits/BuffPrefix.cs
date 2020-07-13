using GW2SDK.Annotations;

namespace GW2SDK.Traits
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class BuffPrefix
    {
        public string Text { get; set; } = "";

        public string Icon { get; set; } = "";

        public string? Status { get; set; }

        public string? Description { get; set; }
    }
}
