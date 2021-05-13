using GW2SDK.Annotations;

namespace GW2SDK.Accounts.Banks
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record SelectedStat
    {
        public int Id { get; init; }

        public SelectedModification Attributes { get; init; } = new();
    }
}
