namespace GW2SDK.Features.Common
{
    public sealed class ListMetaData : IListMetaData
    {
        public int ResultTotal { get; internal set; }

        public int ResultCount { get; internal set; }
    }
}
