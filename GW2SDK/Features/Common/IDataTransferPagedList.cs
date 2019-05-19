namespace GW2SDK.Features.Common
{
    public interface IDataTransferPagedList<out T> : IDataTransferList<T>
    {
        new IPagedListMetaData MetaData { get; }
    }
}
